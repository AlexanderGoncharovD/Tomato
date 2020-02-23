using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Tomato.Settings;

namespace Tomato
{
    /// <summary>
    ///     Страница внутренних настроек приложения
    /// </summary>
    [Activity(Label = "")]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.settings_page);
            AppSettings.SetDefaultColorStatusBar(this);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);

            var backButton = FindViewById<ImageButton>(Resource.Id.back_button);
            backButton.Click += BackButton_Click;
        }

        private void BackButton_Click(object secnder, EventArgs e)
        {
            Finish();
        }
    }
}