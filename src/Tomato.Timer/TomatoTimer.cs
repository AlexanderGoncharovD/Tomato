﻿using System;
using System.Threading.Tasks;

namespace Tomato.TomatoTimer
{
    /// <summary>
    ///     Класс таймера
    /// </summary>
    public class TomatoTimer
    {
        #region Fields

        /// <summary>
        ///     Состояние таймера
        /// </summary>
        private bool _tiker = false;
        private int _time = 0;

        #endregion

        #region Properties

        /// <summary>
        ///     Количество оставшихся минут до окнчания таймера
        /// </summary>
        public string Minutes { get; private set; }

        /// <summary>
        ///     Количество оставшихся секунд до окнчания таймера
        /// </summary>
        public string Seconds { get; private set; }

        /// <summary>
        ///     Время, на которое заведён таймер
        /// </summary>
        private int Time { get => _time; set { _time = value;  SetTimeLeft(value);  } }

        /// <summary>
        ///     Запущен ли таймер
        /// </summary>
        public bool IsTimerStarted { get => _tiker; }

        #endregion

        #region Events

        /// <summary>
        ///     Событие тика таймера
        /// </summary>
        public event EventHandler TimerTick;

        /// <summary>
        ///     Событие окончания таймера
        /// </summary>
        public event EventHandler TimerLeft;

        #endregion

        #region .ctor

        /// <summary>
        ///     Конструктор таймера
        /// </summary>
        public TomatoTimer() { }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Запустить таймер
        /// </summary>
        /// <param name="seconds">
        ///     Количество секунд таймера
        /// </param>
        public void Start(int seconds)
        {
            if (!_tiker)
            {
                _time = seconds;
                Time = _time;
                _tiker = true;
                StartTimer();
            }
        }

        /// <summary>
        ///     Останавливает таймер
        /// </summary>
        public void Stop()
        {
            _tiker = false;
            Time = 0;
            TimerTick?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Поставить таймер на паузу
        /// </summary>
        public void Pause()
        {
            _tiker = !_tiker;

            if (_tiker)
            {
                StartTimer();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Вызывается событие тика таймера с задержкой в одну секунду
        /// </summary>
        private async void StartTimer()
        {
            while (_tiker)
            {
                TimerTick?.Invoke(this, EventArgs.Empty);
                if (Time <= 0)
                        TimeLeft();
                else
                    await Task.Delay(1000);
                Time--;
            }
        }

        /// <summary>
        ///     Устанавливает количество оставшихся минут и секунд до конца таймера
        /// </summary>
        /// <param name="time">
        ///     Время до конца таймера
        /// </param>
        private void SetTimeLeft(decimal time)
        {
            if (time == 0)
            {
                Minutes = "00";
                Seconds = "00";
                return;
            }
            var min = Math.Floor(time / 60);
            var sec = time - min * 60;

            Minutes = min < 10 ? $"0{min}" : $"{min}";
            Seconds = sec < 10 ? $"0{sec}" : $"{sec}";
        }

        /// <summary>
        ///     вызывает событие окночания таймера
        /// </summary>
        private void TimeLeft()
        {
            _tiker = false;
            _time = 0;
            Time = 0;
            TimerLeft?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
