
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace WpfTemplate
{
    /// <summary>
    /// 
    /// </summary>
    public class MainWindowViewModel:BaseViewModel
    {
        private Window mWindow;//window 对象

        public bool ButtonIsBusy { get; set; }
        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand BusyCommand { get; set; }


        public ICommand GetPasswordCommand { get; set; }



        #region constructor
        public MainWindowViewModel(Window window)
        {
            mWindow = window;
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            BusyCommand = new RelayCommand(() => ButtonIsBusy = !ButtonIsBusy);
            GetPasswordCommand = new RelayCommand(() => MessageBox.Show("输入的密码是 "+(mWindow as IHavePassword).SecurePassword.Unsecure()));
        }
        #endregion
    }
}