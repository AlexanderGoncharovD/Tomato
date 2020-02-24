using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Tomato
{
    public class CustomDialog : Dialog
    {
        public CustomDialog(Activity activity) : base(activity) { }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature((int)WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.number_picker_dialog);


            NumberPicker hours = FindViewById<NumberPicker>(Resource.Id.hours);
            var contentHours = CreateContent(0, 12, 1);
            hours.MinValue = 0;
            hours.MaxValue = contentHours.Length-1;
            hours.SetDisplayedValues(contentHours);

            NumberPicker minutes = FindViewById<NumberPicker>(Resource.Id.minutes);
            var contentMinutes = CreateContent(0, 59, 1);
            minutes.MinValue = 0;
            minutes.MaxValue = contentMinutes.Length - 1;
            minutes.SetDisplayedValues(contentMinutes);

            NumberPicker seconds = FindViewById<NumberPicker>(Resource.Id.seconds);
            var contentSeconds = CreateContent(0, 59, 15);
            seconds.MinValue = 0;
            seconds.MaxValue = contentSeconds.Length - 1;
            seconds.SetDisplayedValues(contentSeconds);
        }

        /// <summary>
        ///     Создать контент для NumberPicker'ов
        /// </summary>
        /// <param name="first">
        ///     Начальная точка
        /// </param>
        /// <param name="last">
        ///     Конечная точка
        /// </param>
        /// <param name="step">
        ///     Шаг
        /// </param>
        /// <returns>
        ///     Массив строкового контента
        /// </returns>
        private string[] CreateContent(int first, int last, int step)
        {
            var content = new List<string>();

            for(var i = first; i <= last; i += step)
            {
                content.Add(i < 10 ? $"0{i}" : $"{i}");
            }

            return content.ToArray();
        }
    }
}