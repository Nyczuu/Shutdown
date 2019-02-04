using Shutdowner.Properties;
using System.Diagnostics;
using System.Windows;

namespace Shutdowner
{
    public partial class MainWindow : Window
    {
        private int _hours;
        private int _minutes;
        private int _hoursInitial;
        private int _minutesInitial;
        private int _hoursJumpSize;
        private int _minutesJumpSize;
        private Settings _config;

        public MainWindow()
        {
            _config = Settings.Default;
            InitializeComponent();
            InitializeDashboard();
            InitializeSettings();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ShutdownCancel();
        }

        #region Dashboard

        private void HoursAddButton_Click(object sender, RoutedEventArgs e)
        {
            _hours += _config.HoursJumpSize;
            ValidateAndApplyDashboardInputs();
        }

        private void HoursRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            _hours -= _config.HoursJumpSize;
            ValidateAndApplyDashboardInputs();
        }

        private void MinutesAddButton_Click(object sender, RoutedEventArgs e)
        {
            _minutes += _config.MinutesJumpSize;
            ValidateAndApplyDashboardInputs();
        }

        private void MinutesRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            _minutes -= _config.MinutesJumpSize;
            ValidateAndApplyDashboardInputs();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            ShutdownCancel();
            ShutdownExcute();
        }

        private void InitializeDashboard()
        {
            _hours = _config.HoursInitialValue;
            _minutes = _config.MinutesInitialValue;
            ValidateAndApplyDashboardInputs();
        }

        private void ValidateAndApplyDashboardInputs()
        {
            if (_hours <= 0 || _hours >= 24)
                _hours = 0;

            if (_minutes <= 0 || _minutes >= 60)
                _minutes = 0;

            HoursTextBox.Text = _hours.ToString();
            MinutesTextBox.Text = _minutes.ToString();
        }

        private void ShutdownCancel()
        {
            //RunProcess("shutdown", "/a");
        }

        private void ShutdownExcute()
        {
            RunProcess("shutdown.exe", $"-s -t {(_hours * 3600) + (_minutes * 60)}");

            if (CloseAfterConfirmCheckBox.IsChecked == true)
                Application.Current.Shutdown();
        }

        private void RunProcess(string command, string arguments)
        {
            Process.Start(command,arguments);
        }

        #endregion

        #region Settings

        private void HoursInitialAddButton_Click(object sender, RoutedEventArgs e)
        {
            _hoursInitial++;
            ValidateAndApplySettingsInputs();
        }

        private void HoursInitialRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            _hoursInitial--;
            ValidateAndApplySettingsInputs();
        }

        private void HoursJumpSizeAddButton_Click(object sender, RoutedEventArgs e)
        {
            _hoursJumpSize++;
            ValidateAndApplySettingsInputs();
        }

        private void HoursJumpSizeRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            _hoursJumpSize--;
            ValidateAndApplySettingsInputs();
        }

        private void MinutesInitialAddButton_Click(object sender, RoutedEventArgs e)
        {
            _minutesInitial++;
            ValidateAndApplySettingsInputs();
        }

        private void MinutesInitialRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            _minutesInitial--;
            ValidateAndApplySettingsInputs();
        }

        private void MinutesJumpSizeAddButton_Click(object sender, RoutedEventArgs e)
        {
            _minutesJumpSize++;
            ValidateAndApplySettingsInputs();
        }

        private void MinutesJumpSizeRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            _minutesJumpSize--;
            ValidateAndApplySettingsInputs();
        }

        private void SettingsSaveButton_Click(object sender, RoutedEventArgs e)
        {
            _config.HoursInitialValue = _hoursInitial;
            _config.HoursJumpSize = _hoursJumpSize;
            _config.MinutesInitialValue = _minutesInitial;
            _config.MinutesJumpSize = _minutesJumpSize;
            _config.Save();

            ResetToSavedSettings();
            ValidateAndApplySettingsInputs();
        }

        private void InitializeSettings()
        {
            ResetToSavedSettings();
            ValidateAndApplySettingsInputs();
        }

        private void ResetToSavedSettings()
        {
            _hoursInitial = _config.HoursInitialValue;
            _minutesInitial = _config.MinutesInitialValue;
            _hoursJumpSize = _config.HoursJumpSize;
            _minutesJumpSize = _config.MinutesJumpSize;
        }

        private void ValidateAndApplySettingsInputs()
        {
            if (_hoursInitial <= 0 || _hoursInitial >= 24)
                _hoursInitial = 0;

            if (_hoursJumpSize <= 0 || _hoursJumpSize >= 24)
                _hoursJumpSize = 0;

            if (_minutesInitial <= 0 || _minutesInitial >= 60)
                _minutesInitial = 0;

            if (_minutesJumpSize <= 0 || _minutesJumpSize >= 60)
                _minutesJumpSize = 0;

            HoursInitialTextBox.Text = _hoursInitial.ToString();
            HoursJumpSizeTextBox.Text = _hoursJumpSize.ToString();
            MinutesInitialTextBox.Text = _minutesInitial.ToString();
            MinutesJumpSizeTextBox.Text = _minutesJumpSize.ToString();
        }

        #endregion

    }
}