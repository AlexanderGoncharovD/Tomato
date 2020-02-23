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
using Xamarin.Essentials;

namespace Tomato.Settings
{
    /// <summary>
    ///     Настройки приложения
    /// </summary>
    public static class AppSettings
    {
        #region Const

        private const string DEFAULT_STATUS_BAR_COLOR = "#1B3147";

        #endregion

        #region Public Methods

        /// <summary>
        ///     Установить цвет статус бару по умолчанию
        /// </summary>
        /// <param name="activity">
        ///     Окно, которому нужно установить цвет статус бара
        /// </param>
        public static void SetDefaultColorStatusBar(Activity activity) =>
            SetColorHexStatusBar(activity, DEFAULT_STATUS_BAR_COLOR);

        /// <summary>
        ///     Установить цвет статус бару из Hex цвета
        /// </summary>
        /// <param name="activity">
        ///     Окно, которому нужно установить цвет статус бара
        /// </param>
        /// <param name="uintColor">
        ///     Uint цвет
        /// </param>
        public static void SetColorUintStatusBar(Activity activity, uint uintColor)
        {
            var androidColor = ColorConverters.FromUInt(uintColor).ToPlatformColor();
            SetColorStatusBar(activity, androidColor);
        }

        /// <summary>
        ///     Установить цвет статус бару из Hex цвета
        /// </summary>
        /// <param name="activity">
        ///     Окно, которому нужно установить цвет статус бара
        /// </param>
        /// <param name="hexColor">
        ///     Hex цвет
        /// </param>
        public static void SetColorHexStatusBar(Activity activity, string hexColor)
        {
            var androidColor = ColorConverters.FromHex(hexColor).ToPlatformColor();
            SetColorStatusBar(activity, androidColor);
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Установить цвет статус бару
        /// </summary>
        /// <param name="activity">
        ///     Окно, которому нужно установить цвет статус бара
        /// </param>
        /// <param name="color">
        ///     Андроид цвет
        /// </param>
        private static void SetColorStatusBar(Activity activity, Android.Graphics.Color color)
        {
            activity.Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            activity.Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            activity.Window.SetStatusBarColor(color);
        }

        #endregion
    }
}