using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ViewModel.Base
{
    /// <summary>
    /// A base view model that fires Property Changed events as needed
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #region command helpers
        /// <summary>
        /// if the flag is false the action will not run
        /// </summary>
        /// <param name="updatingFlag">the boolean flag defining if the command is already running</param>
        /// <param name="action">the action to run if the command is not already running</param>
        /// <returns></returns>
        //protected async Task RunCommandAsync(Expression<Func<bool>> updatingFlag, Func<Task> action)
        //{
        //    //check if the flag is true
        //    if (updatingFlag.GetPropertyValue())
        //        return;
        //    updatingFlag.SetPropertyValue(true);
        //    try
        //    {
        //        await action();
        //    }
        //    finally
        //    {
        //        //set the property flag back to false not it's finished
        //        updatingFlag.SetPropertyValue(false);
        //    }

        //}
        #endregion
    }
}
