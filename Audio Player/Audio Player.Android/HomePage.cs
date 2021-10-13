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
    [Activity(Label = "HomePage")]
    public class HomePage : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.home_page);
            TextView text = new TextView(this);
            text.Text = File.ReadAllText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "/music_dir.txt");
            LinearLayout linear = FindViewById<LinearLayout>(Resource.Id.homeLinear);
            linear.AddView(text);
        }

    }
}