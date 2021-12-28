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
using System.Threading.Tasks;

namespace Audio_Player.Droid
{
    [Activity(Label = "FolderSelectAndroid")]
    public class FolderSelectRecurse : Activity
    {

        private ArrayList selectedFolders = new ArrayList();
        private String filePath;
        private Color buttonDefault = new Color(Resource.Color.launcher_background);
        private Color buttonHighlight = new Color(Resource.Color.black);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.folder_select_android);

            filePath = Intent.GetStringExtra("path");

            String[] alreadySelectedFolders = new String[0];
            if (File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "/music_dir.txt"))
            {
                alreadySelectedFolders = File.ReadAllLines(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "/music_dir.txt");

                for (int i = 0; i < alreadySelectedFolders.Length; i++)
                {
                    selectedFolders.Add(alreadySelectedFolders[i]);
                }
            }
            LinearLayout linear = FindViewById<LinearLayout>(Resource.Id.dir_linear);

            Button scroll_button = new Button(this);
            scroll_button.Text = "Select";
            scroll_button.SetHeight(250);
            scroll_button.SetBackgroundColor(buttonDefault);

            /**
             * Button Click
             */
            scroll_button.Click += async (o, e) =>
            {
                Intent toHomePage = new Intent(this, typeof(HomePage));

                await UpdateFileAsync();
                StartActivity(toHomePage);
            };

            Button back_button = new Button(this);
            back_button.Text = "<";
            back_button.SetBackgroundColor(buttonDefault);

            back_button.Click += async (o, e) =>
            {
                String[] previousPath = filePath.Split("/");
                previousPath = previousPath.Where<String>(val => val != previousPath[previousPath.Length - 1]).ToArray();
                Intent prevActivity;
                await UpdateFileAsync();
                if (previousPath[previousPath.Length -1].Equals("sdcard"))
                {
                    prevActivity = new Intent(this, typeof(FolderSelectInnit));
                    StartActivity(prevActivity);
                }
                else
                {
                    prevActivity = new Intent(this, typeof(FolderSelectRecurse));
                    prevActivity.PutExtra("path", String.Join("/", previousPath));
                    StartActivity(prevActivity);
                }
            };

            linear.AddView(back_button);

            String[] cwd = Directory.GetDirectories(filePath);

            for (int i = 0; i < cwd.Length; i++)
            {
                Folder folder = new Folder(this);
                folder.setText(cwd[i]);

                if (alreadySelectedFolders.Contains<String>(folder.Text))
                {
                    folder.changeColor();
                }

                /**
                 * Folder Click
                 */
                folder.Click += async (o, e) =>
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

                        await UpdateFileAsync();
                        StartActivity(refresh);
                    }

                };

                /**
                 * Folder Long Click
                 */
                folder.LongClick += async (o, e) =>
                {
                    if (folder.isSelected())
                    {
                        folder.changeColor();
                        selectedFolders.Remove(folder.Text);

                        if (selectedFolders.Count == 0)
                        {
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

        async Task UpdateFileAsync()
        {
            await File.WriteAllLinesAsync(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "/music_dir.txt",
                                                        selectedFolders.Cast<String>());
        }
    }

}