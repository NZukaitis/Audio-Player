using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.IO;
using Android.Content;

namespace Audio_Player.Droid
{
    [Activity(Label = "Audio_Player", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            //if the directory(ies) for the songs have already been selected, aka if first time setup already happened
            if(File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "/music_dir.txt")){
                System.Console.WriteLine("File already exists");
                System.Console.WriteLine(string.Join("\n", Directory.GetFiles(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData))));

                Intent nextActivity = new Intent(this, typeof(FolderSelectInnitAndroid));
                StartActivity(nextActivity);
            }
            //if the directory(ies) have not yet been selected, aka first time setup
            else
            {
                File.Create(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "/music_dir.txt");
                Intent nextActivity = new Intent(this, typeof(FolderSelectInnitAndroid));
                StartActivity(nextActivity);
            }
            
           
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}