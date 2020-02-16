using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Views;

namespace Tomato
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextView lbl;
        private TomatoTimer.TomatoTimer Timer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "My Toolbar";
            

            lbl = FindViewById<TextView>(Resource.Id.lbl);
            var start = FindViewById<Button>(Resource.Id.start_button);
            var pause = FindViewById<Button>(Resource.Id.pause_button);
            var stop = FindViewById<Button>(Resource.Id.stop_button);

            start.Click += Start_Click;
            pause.Click += Pause_Click;
            stop.Click += Stop_Click;

            Timer = new TomatoTimer.TomatoTimer(90);
            Timer.TimerTick += OnTimerTick;
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Timer.Stop();
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            Timer.Pause();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Timer.Start();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            lbl.Text = $"{Timer.Minutes}:{Timer.Seconds}";
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
                ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }

    }
}