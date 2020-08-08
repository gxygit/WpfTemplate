using System;
using System.Windows;

namespace ViewModel.Base
{
    /// <summary>
    /// a base attached property to replace the vanilla wpf attached property
    /// </summary>
    /// <typeparam name="Parent">the parent class to be the attached property</typeparam>
    /// <typeparam name="Property">the type of this attached property</typeparam>
    public abstract class BaseAttachedProperty<Parent, Property>
        where Parent : new()
    {
        #region public properties

        public static Parent Instance { get; private set; } = new Parent();

        #endregion

        #region attached property definitions

        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value",
            typeof(Property),
            typeof(BaseAttachedProperty<Parent, Property>),
            new UIPropertyMetadata(
                default(Property),
                new PropertyChangedCallback(OnValuePropertyChanged),
                new CoerceValueCallback(OnValuePropertyUpdated)
                ));

        /// <summary>
        /// the callback event when the <see cref="ValueProperty"/> is changed
        /// 值改变了才会调用
        /// </summary>
        /// <param name="d">the ui element that had it's property changed</param>
        /// <param name="e">the arguments for the event</param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //call the parent function
            (Instance as BaseAttachedProperty<Parent, Property>)?.OnValueChanged(d, e);
            //call event listeners
            (Instance as BaseAttachedProperty<Parent, Property>)?.ValueChanged(d, e);
        }
        /// <summary>
        /// the callback event when the <see cref="ValueProperty"/> is changed
        /// 每次设置值 即使值和本次设置之前的相同也会调用
        /// </summary>
        /// <param name="d">the ui element that had it's property changed</param>
        /// <param name="e">the arguments for the event</param>
        private static object OnValuePropertyUpdated(DependencyObject d, object value)
        {
            //call the parent function
            (Instance as BaseAttachedProperty<Parent, Property>)?.OnValueUpdated(d, value);
            //call event listeners
            (Instance as BaseAttachedProperty<Parent, Property>)?.ValueUpdated(d, value);

            return value;
        }
        /// <summary>
        /// get the attached property
        /// </summary>
        /// <param name="d">the element to get the property from</param>
        /// <returns></returns>
        public static Property GetValue(DependencyObject d) => (Property)d.GetValue(ValueProperty);

        /// <summary>
        /// set the attached property
        /// </summary>
        /// <param name="d">the element to set the property</param>
        /// <param name="value">the value to set the property</param>
        public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty, value);
        #endregion

        #region public events
        /// <summary>
        /// fired when the value changes
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };
        /// <summary>
        /// fired when the value changes,even whe the value is same 
        /// </summary>
        public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };
        #endregion

        #region event methods
        /// <summary>
        /// the method that is called when any attached property of this type is changed
        /// </summary>
        /// <param name="sender">the ui element</param>
        /// <param name="e">the argument of this event</param>
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }
        /// <summary>
        /// the method that is called when any attached property of this type is changed,even if the value is the same
        /// </summary>
        /// <param name="sender">the ui element</param>
        /// <param name="e">the argument of this event</param>
        public virtual void OnValueUpdated(DependencyObject sender, object value) { }
        #endregion
    }
}
