using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Audio_Player.Droid  
{
    [Activity(Label = "FolderSelectRecurse")]
    public class FolderSelectRecurse : Activity
    {

        String filePath;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.folder_select_android);

            LinearLayout linear = FindViewById<LinearLayout>(Resource.Id.dir_linear);

            filePath = Intent.GetStringExtra("path");

            String[] cwd = Directory.GetDirectories(filePath);

            for (int i = 0; i < cwd.Length; i++)
            {
                TextView folder = new TextView(this)
                {
                    Text = cwd[i],
                    TextSize = 24

                };

                linear.AddView(folder);
            }

            Button scroll_button = new Button(this)
            {
                Text = "Select",

            };
            scroll_button.SetHeight(250);

            linear.AddView(scroll_button);

            scroll_button.LongClick += (o, e) =>
            {
                System.Console.WriteLine("long");
            };
        }
    }
}