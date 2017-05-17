using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceInformation
{
    public partial class GallerySelectorDialog : CustomDialog
    {
        private ObservableCollection<ImageSource> _galleryImages;

        private MetroWindow _parentWindow;

        public string SelectedRefinedType { get; set; }

        public ObservableCollection<ImageSource> GalleryImages
        {
            get
            {
                return _galleryImages;

            }
            set { _galleryImages = value; }
        }


        public GallerySelectorDialog(string imagesLocation, MetroWindow window)
        {
            InitializeComponent();

            _parentWindow = window;
            
            GalleryImages = new ObservableCollection<ImageSource>();

            var pngFiles = Directory.GetFiles(imagesLocation, "*.png");
            foreach (var file in pngFiles)
            {
                if (File.Exists(file))
                {
                    GalleryImages.Add(new BitmapImage(new Uri(file)));
                }
            }
            
            Title = "Please select the closest representation of the device";
            this.DataContext = this;
        }


        public void Cancel()
        {
            //DialogManager.HideMetroDialogAsync(_parentWindow, this);
        }

        public void SelectImage(ImageSource imageSource)
        {
            var location = imageSource.ToString();
            var splitStrings = location.Split('/');
            if (splitStrings.Length > 0)
            {
                var file = splitStrings[splitStrings.Length - 1];
                var refinedType = file.Split('.');
                SelectedRefinedType = refinedType[0];
            }

            var newEventArgs = new RoutedEventArgs(ImageSelectedEvent);
            RaiseEvent(newEventArgs);

            //DialogManager.HideMetroDialogAsync(_parentWindow, this);
        }

        public static readonly RoutedEvent ImageSelectedEvent = EventManager.RegisterRoutedEvent(
        "ImageSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(GallerySelectorDialog));

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
                this.ImageSelected -= affirmativeHandler;

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

                    tcs.TrySetResult(SelectedRefinedType);
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

                tcs.TrySetResult(SelectedRefinedType);

                e.Handled = true;
            };

            PART_NegativeButton.KeyDown += negativeKeyHandler;
            this.ImageSelected += affirmativeHandler;

            this.KeyDown += escapeKeyHandler;

            PART_NegativeButton.Click += negativeHandler;

            return tcs.Task;
        }

        public event RoutedEventHandler ImageSelected
        {
            add { AddHandler(ImageSelectedEvent, value); }
            remove { RemoveHandler(ImageSelectedEvent, value); }
        }

        public static readonly DependencyProperty NegativeButtonTextProperty = DependencyProperty.Register("NegativeButtonText", typeof(string), typeof(InputDialog), new PropertyMetadata("Cancel"));


        public string NegativeButtonText
        {
            get { return (string)GetValue(NegativeButtonTextProperty); }
            set { SetValue(NegativeButtonTextProperty, value); }
        }

    }
}