using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace PinballClient.ClientDisplays.Dialogs
{
    public partial class LedSelectorDialog : CustomDialog
    {
        private string _selectedLed;

        private MetroWindow _parentWindow;

        public ObservableCollection<string> Leds { get; private set; }

        public string SelectedLed
        {
            get
            {
                return _selectedLed;

            }
            set
            {
                _selectedLed = value;

                var newEventArgs = new RoutedEventArgs(LedSelectedEvent);
                RaiseEvent(newEventArgs);
            }
        }

        public LedSelectorDialog(List<string> leds, MetroWindow window)
        {
            InitializeComponent();

            _parentWindow = window;

            Leds = new ObservableCollection<string>(leds);

            Title = "Led to duplicate events to.";
            this.DataContext = this;
        }


        public void Cancel()
        {
            //DialogManager.HideMetroDialogAsync(_parentWindow, this);
        }

        public static readonly RoutedEvent LedSelectedEvent = EventManager.RegisterRoutedEvent(
        "LedSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(LedSelectorDialog));

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

                    tcs.TrySetResult(SelectedLed);
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

                tcs.TrySetResult(SelectedLed);

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
            add { AddHandler(LedSelectedEvent, value); }
            remove { RemoveHandler(LedSelectedEvent, value); }
        }

        public static readonly DependencyProperty NegativeButtonTextProperty = DependencyProperty.Register("NegativeButtonText", typeof(string), typeof(InputDialog), new PropertyMetadata("Cancel"));

        public string NegativeButtonText
        {
            get { return (string)GetValue(NegativeButtonTextProperty); }
            set { SetValue(NegativeButtonTextProperty, value); }
        }
    }
}
