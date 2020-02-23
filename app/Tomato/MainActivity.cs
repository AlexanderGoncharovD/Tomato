using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Views;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace Tomato
{
    [Activity(MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextView lbl;
        private TomatoTimer.TomatoTimer Timer;
        private bool _isVibration = false;
        private Button _startButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            SetStatusBarColor();
            
            lbl = FindViewById<TextView>(Resource.Id.lbl);
            _startButton = FindViewById<Button>(Resource.Id.start_button);
            var pause = FindViewById<Button>(Resource.Id.pause_button);
            var stop = FindViewById<Button>(Resource.Id.stop_button);

            _startButton.Click += Start_Click;
            pause.Click += Pause_Click;
            stop.Click += Stop_Click;

            var settings = FindViewById<ImageButton>(Resource.Id.settings_button);
            settings.Click += SettingsButton_Click;

            Timer = new TomatoTimer.TomatoTimer();
            Timer.TimerTick += OnTimerTick;
            Timer.TimerLeft += OnTimerLeft;

        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #region Private Methods

        private void Stop_Click(object sender, EventArgs e)
        {
            if (_isVibration)
            {
                _isVibration = false;
                Xamarin.Essentials.Vibration.Cancel();
            }
            Timer.Stop();
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            Timer.Pause();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Timer.Start(15);
        }

        /// <summary>
        ///     Обновление тика таймера
        /// </summary>
        private void OnTimerTick(object sender, EventArgs e)
        {
            lbl.Text = $"{Timer.Minutes}:{Timer.Seconds}";
            _startButton.Enabled = !Timer.IsTimerStarted;
        }

        /// <summary>
        ///     Обработичк завершения таймера
        /// </summary>
        private void OnTimerLeft(object sender, EventArgs e)
        {
            Vibration(TimeSpan.FromSeconds(1));
        }

        /// <summary>
        ///     Устанавливает цвет статус бара
        /// </summary>
        private void SetStatusBarColor()
        {
            var color = ColorConverters.FromHex("#1B3147").ToPlatformColor();

            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            Window.SetStatusBarColor(new Android.Graphics.Color(color));
        }

        /// <summary>
        ///     Обработчик кнопки настроект
        /// </summary>
        private void SettingsButton_Click(object secnder, EventArgs e)
        {
            Toast.MakeText(this, "Action selected: Settings",
                ToastLength.Short).Show();
        }

        /// <summary>
        ///     Вибрация продолжительностью <paramref name="time"/>
        /// </summary>
        /// <param name="time">
        ///     Время продолжительности вибрации
        /// </param>
        private async void Vibration(TimeSpan time)
        {
            _isVibration = true;
            while (_isVibration)
            {
                Xamarin.Essentials.Vibration.Vibrate(time);
                await Task.Delay(1500);
            }
        }

        #endregion

    }
}