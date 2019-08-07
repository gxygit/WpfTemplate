
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

        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        #region constructor
        public MainWindowViewModel(Window window)
        {
            mWindow = window;
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
        }
        #endregion
    }
}