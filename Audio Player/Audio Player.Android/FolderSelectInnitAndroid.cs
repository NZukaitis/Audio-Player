using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace Audio_Player.Droid //TODO: figure out passing the arraylist and saving paths to disk
{
    [Activity(Label = "FolderSelectAndroid")]
    public class FolderSelectInnitAndroid : Activity
    {

        private ArrayList selectedFolders = new ArrayList();
        private String filePath = "/sdcard";
        private Color buttonDefault = new Color(Resource.Color.launcher_background);
        private Color buttonHighlight = new Color(Resource.Color.black);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.folder_select_android);

            LinearLayout linear = FindViewById<LinearLayout>(Resource.Id.dir_linear);

            Button scroll_button = new Button(this);
            scroll_button.Text = "Select";
            scroll_button.SetHeight(250);
            scroll_button.SetBackgroundColor(buttonDefault);

            scroll_button.Click += (o, e) =>
            {
                System.Console.WriteLine("button");
            };

            String[] cwd = Directory.GetDirectories(filePath);

            for (int i = 0; i < cwd.Length; i++)
            {
                Folder folder = new Folder(this);
                folder.setText(cwd[i]);
                folder.Click += (o, e) =>
                {
                    //if there are no subfolders in the current directory 
                    if (Directory.GetDirectories(folder.Text).Length == 0)
                    {
                        if (folder.isSelected())
                        {
                            folder.changeColor();
                            selectedFolders.Remove(folder.Text);

                            if(selectedFolders.Count == 0)
                            {
                                System.Console.WriteLine("empty");
                                scroll_button.SetBackgroundColor(buttonDefault);
                            }

                            linear.ForceLayout();
                        }

                        else
                        {
                            folder.changeColor();
                            selectedFolders.Add(folder.Text);

                            scroll_button.SetBackgroundColor(buttonHighlight);

                            linear.ForceLayout();
                        }
                    }

                    else
                    {
                        Intent refresh = new Intent(this, typeof(FolderSelectRecurse));

                        refresh.PutExtra("path", folder.Text);
                        //refresh.PutExtra("selectedFolders", (IParcelable)selectedFolders);
                        //StartActivity(refresh);
                    }

                };

                folder.LongClick += (o, e) =>
                {
                    if (folder.isSelected())
                    {
                        folder.changeColor();
                        selectedFolders.Remove(folder.Text);

                        if (selectedFolders.Count == 0)
                        {
                            System.Console.WriteLine("empty");
                            scroll_button.SetBackgroundColor(buttonDefault);
                        }

                    }

                    else
                    {
                        folder.changeColor();
                        selectedFolders.Add(folder.Text);

                        scroll_button.SetBackgroundColor(buttonHighlight);
                    }
                };

                linear.AddView(folder);
            }

            linear.AddView(scroll_button);
        }

        private void Scroll_button_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }

}