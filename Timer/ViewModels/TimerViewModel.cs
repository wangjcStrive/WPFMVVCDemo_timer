using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Timer.Common;

namespace Timer.ViewModels
{
    class TimerViewModel: ViewModelBase
    {
        #region Fields
        private const string _backGroundPath = "/Images/BackGround.jpeg";
        private const string _startIconPath = "/Images/Start.png";
        private const string _pauseIconPath = "/Images/Pause.png";
        private const string _resetIconPath = "/Images/Reset.png";
        
        private string _timeCount1;
        private string _timeCount2;
        private bool _isTime1Paused;
        private bool _isTime2Paused;
        private bool _isTimerStarted;
        int _timeCountNum;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        #endregion

        #region Properties
        public string BackGroundPath
        {
            get { return _backGroundPath; }
        }

        public string StartIconPath
        {
            get { return _startIconPath; }
        }

        public string Pause1IconPath
        {
            get { return _pauseIconPath; }
        }
        public string Pause2IconPath
        {
            get { return _pauseIconPath; }
        }

        public string ResetIconPath
        {
            get { return _resetIconPath; }
        }

        public string TimeCount1
        {
            get { return _timeCount1; }
            set { _timeCount1 = value; NotifyPropertyChanged("TimeCount1"); }
        }
        public string TimeCount2
        {
            get { return _timeCount2; }
            set { _timeCount2 = value; NotifyPropertyChanged("TimeCount2"); }
        }
        #endregion

        #region ICommand & Relay Command
        public ICommand StartTimerTrigger { get; private set; }
        public ICommand Pause1Trigger { get; private set; }
        public ICommand Pause2Trigger { get; private set; }
        public ICommand ResetTimerTrigger { get; private set; }
        
        private void InitRelayCommands()
        {
            StartTimerTrigger  = new RelayCommand(o => OnStartTimerTrigger(), o => (!_isTimerStarted));
            Pause1Trigger = new RelayCommand (o => OnPause1Trigger(), o => _isTimerStarted );
            Pause2Trigger = new RelayCommand(o => OnPause2Trigger(),  o => _isTimerStarted);
            ResetTimerTrigger  = new RelayCommand(o => OnResetTimerTrigger(), o => _isTimerStarted);
        }
        #endregion

        public TimerViewModel()
        {
            _timeCountNum            = 0;
            TimeCount1               = _timeCountNum.ToString();
            TimeCount2               = _timeCountNum.ToString();
            dispatcherTimer.Tick    += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _isTime1Paused           = false;
            _isTime2Paused           = false;
            _isTimerStarted          = false;
            InitRelayCommands();
        }




        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!_isTimerStarted)
                return;
            _timeCountNum++;
            if((!_isTime1Paused) && (!_isTime2Paused))
            {
                TimeCount1 = _timeCountNum.ToString();
                TimeCount2 = _timeCountNum.ToString();
                return;
            }
            if ((!_isTime1Paused) && (_isTime2Paused))
            {
                TimeCount1 = _timeCountNum.ToString();
                return;
            }
            if ((_isTime1Paused) && (!_isTime2Paused))
            {
                TimeCount2 = _timeCountNum.ToString();
                return;
            }
        }

        private void OnStartTimerTrigger()
        {
            _isTimerStarted = true;
            dispatcherTimer.Start();
        }

        private void OnPause1Trigger()
        {
            _isTime1Paused = true;
        }
        private void OnPause2Trigger()
        {
            _isTime2Paused = true;
        }

        private void OnResetTimerTrigger()
        {
            _timeCountNum = 0;
            TimeCount1 = _timeCountNum.ToString();
            TimeCount2 = _timeCountNum.ToString();
            _isTime1Paused = false;
            _isTime2Paused = false;
            _isTimerStarted = false;
        }
    }
}
