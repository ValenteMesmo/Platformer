using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;
using Platformer.Desktop;

namespace App.Android
{
    [Activity(Label = "App.Android"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ResizeableActivity = true
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : AndroidGameActivity
    {
        private Game1 game = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            game = new Game1();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                //Vibrator vibrator = (Vibrator)GetSystemService(VibratorService);
                //var oneshot = VibrationEffect.CreateOneShot(1, 1);
                //game.Vibrate = f => vibrator.Vibrate(oneshot);
            }
            else
            {
                //TODO:   
            }

            SetViewFullScreen();
            game.Run();
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetViewFullScreen();
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            SetViewFullScreen();
        }

        private void SetViewFullScreen()
        {
            var view = game.GetService<View>();
            view.SystemUiVisibility = (StatusBarVisibility)
                (SystemUiFlags.LayoutStable
                | SystemUiFlags.LayoutHideNavigation
                | SystemUiFlags.LayoutFullscreen
                | SystemUiFlags.HideNavigation
                | SystemUiFlags.Immersive                
                | SystemUiFlags.Fullscreen
                | SystemUiFlags.ImmersiveSticky);

            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            Window.AddFlags(WindowManagerFlags.Fullscreen);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
                Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;

            SetContentView(view);
        }
    }
}

