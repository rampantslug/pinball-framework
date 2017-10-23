using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Action = System.Action;

namespace PinballClient.ClientDisplays.Dialogs
{
    public partial class DeviceSelectorDialog : CustomDialog
    {
        private string _selectedDevice;

        private MetroWindow _parentWindow;

        public ObservableCollection<string> Devices { get; private set; }

        public string SelectedDevice
        {
            get
            {
                return _selectedDevice;

            }
            set
            {
                _selectedDevice = value;

                var newEventArgs = new RoutedEventArgs(DeviceSelectedEvent);
                RaiseEvent(newEventArgs);
            }
        }

        public DeviceSelectorDialog(List<string> devices, MetroWindow window)
        {
            InitializeComponent();

            _parentWindow = window;

            Devices = new ObservableCollection<string>(devices);
            
            Title = "Please select associated device.";
            this.DataContext = this;
        }


        public void Cancel()
        {
            //DialogManager.HideMetroDialogAsync(_parentWindow, this);
        }

        public static readonly RoutedEvent DeviceSelectedEvent = EventManager.RegisterRoutedEvent(
        "DeviceSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DeviceSelectorDialog));

        internal Task<string> WaitForButtonPressAsync()
        {
            var tcs = new TaskCompletionSource<string>();

            RoutedEventHandler negativeHandler = null;
            KeyEventHandler negativeKeyHandler = null;

            RoutedEventHandler affirmativeHandler = null;
            KeyEventHandler affirmativeKeyHandler = null;

            KeyEventHandler escapeKeyHandler = null;

            Action cleanUpHandlers = () =>
            {

                this.KeyDown -= escapeKeyHandler;

                PART_NegativeButton.Click -= negativeHandler;
                this.DeviceSelected -= affirmativeHandler;

                PART_NegativeButton.KeyDown -= negativeKeyHandler;
            };

            escapeKeyHandler = (sender, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(null);
                }
            };

            negativeKeyHandler = (sender, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(null);
                }
            };

            affirmativeKeyHandler = (sender, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(SelectedDevice);
                }
            };

            negativeHandler = (sender, e) =>
            {
                cleanUpHandlers();

                tcs.TrySetResult(null);

                e.Handled = true;
            };

            affirmativeHandler = (sender, e) =>
            {
                cleanUpHandlers();

                tcs.TrySetResult(SelectedDevice);

                e.Handled = true;
            };

            PART_NegativeButton.KeyDown += negativeKeyHandler;
            this.DeviceSelected += affirmativeHandler;

            this.KeyDown += escapeKeyHandler;

            PART_NegativeButton.Click += negativeHandler;

            return tcs.Task;
        }

        public event RoutedEventHandler DeviceSelected
        {
            add { AddHandler(DeviceSelectedEvent, value); }
            remove { RemoveHandler(DeviceSelectedEvent, value); }
        }

        public static readonly DependencyProperty NegativeButtonTextProperty = DependencyProperty.Register("NegativeButtonText", typeof(string), typeof(InputDialog), new PropertyMetadata("Cancel"));

        public string NegativeButtonText
        {
            get { return (string)GetValue(NegativeButtonTextProperty); }
            set { SetValue(NegativeButtonTextProperty, value); }
        }

    }
}