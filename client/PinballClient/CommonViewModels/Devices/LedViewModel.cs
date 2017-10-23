using System;
using System.Windows.Media;
using BusinessObjects.Devices;
using BusinessObjects.Shapes;
using Caliburn.Micro;
using Hardware.DeviceAddress;
using Common.Commands;
using PinballClient.ClientComms;
using PinballClient.Events;

namespace PinballClient.CommonViewModels.Devices
{
    public class LedViewModel : DeviceViewModel
    {
        public override string DeviceType => "Led";

        public override bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                if (_isSelected)
                {
                    EventAggregator.PublishOnUIThread(new HighlightLedEvent() { LedVm = this });
                }
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        public bool IsSingleColor
        {
            get { return ((Led)Device).IsSingleColor; }
            set
            {
                ((Led)Device).IsSingleColor = value;
                NotifyOfPropertyChange(() => IsSingleColor);
            }
        }

        public Color SingleColor
        {
            get { return ((Led)Device).SingleColor; }
            set
            {
                ((Led)Device).SingleColor = value;
                NotifyOfPropertyChange(() => SingleColor);
            }
        }

        public LedShape Shape
        {
            get { return ((Led)Device).Shape; }
            set
            {
                ((Led)Device).Shape = value;
                UpdateGeometry(value);
                NotifyOfPropertyChange(() => Shape);
            }
        }

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ledDevice"></param>
        public LedViewModel(Led ledDevice, IClientCommsController clientCommsController, IEventAggregator eventAggregator)
            : base(clientCommsController, eventAggregator)
        {
            Device = ledDevice;

            // New device hasn't had its address set yet
            if (string.IsNullOrEmpty(Device.Address))
            {
                Address = new PlbAddress();
            }
        }

        #endregion

        #region Device Command Methods

        public void ActivateLed()
        {
            var led = Device as Led;
            ClientCommsController.SendCommandDeviceMessage(led, LedCommand.MidIntesityOn);
        }

        public void DeactivateLed()
        {
            var led = Device as Led;
            ClientCommsController.SendCommandDeviceMessage(led, LedCommand.FullOff);
        }

        #endregion

        public override void ConfigureDevice()
        {
            EventAggregator.PublishOnUIThread(new ShowLedConfigEvent() { LedVm = this });
        }

        public void UpdateDeviceInfo(Led ledDevice, DateTime timestamp)
        {
            base.UpdateDeviceInfo(ledDevice, timestamp);
        }

        // Build what shapes we can from code. Irregular shapes are taken from AllShapes.xaml
        private void UpdateGeometry(LedShape shape)
        {
            switch (shape)
            {
                case LedShape.Square:
                    {
                        PlayfieldGeometry = Geometry.Parse("F1 M -7.5,-9.5L 7.5,-9.5C 8.60456,-9.5 9.5,-8.60461 9.5,-7.5L 9.5,7.50006C 9.5,8.60461 8.60456,9.50006 7.5,9.50006L -7.5,9.50006C -8.60457,9.50006 -9.5,8.60461 -9.5,7.50006L -9.5,-7.5C -9.5,-8.60461 -8.60457,-9.5 -7.5,-9.5 Z ");
                        break;
                    }
                case LedShape.Rectangle:
                    {
                        PlayfieldGeometry = Geometry.Parse("F1 M -17,-9.50006L 17,-9.50006C 18.1046,-9.50006 19,-8.60461 19,-7.50006L 19,7.50006C 19,8.60455 18.1046,9.49994 17,9.49994L -17,9.49994C -18.1046,9.49994 -19,8.60455 -19,7.50006L -19,-7.50006C -19,-8.60461 -18.1046,-9.50006 -17,-9.50006 Z ");
                        break;
                    }
                case LedShape.CircleLarge:
                    {
                        PlayfieldGeometry = Geometry.Parse("F1 M 0,-21.5C 11.8741,-21.5 21.5,-11.8741 21.5,0C 21.5,11.8741 11.8741,21.5 0,21.5C -11.8741,21.5 -21.5,11.8741 -21.5,0C -21.5,-11.8741 -11.8741,-21.5 0,-21.5 Z ");
                        break;
                    }
                case LedShape.CircleMed:
                    {
                        PlayfieldGeometry = Geometry.Parse("F1 M 2.08674e-005,-17.5001C 9.66499,-17.5001 17.5,-9.66498 17.5,-6.10352e-005C 17.5,9.66498 9.665,17.4999 2.08674e-005,17.4999C -9.66496,17.4999 -17.5,9.66498 -17.5,-6.10352e-005C -17.5,-9.66498 -9.66494,-17.5001 2.08674e-005,-17.5001 Z ");
                        break;
                    }
                case LedShape.CircleSmall:
                    {
                        PlayfieldGeometry = Geometry.Parse("F1 M -3.34502e-006,-15.0001C 8.28422,-15.0001 15,-8.28424 15,-6.10352e-005C 15,8.28424 8.28427,14.9999 -3.34502e-006,14.9999C -8.28427,14.9999 -15,8.28424 -15,-6.10352e-005C -15,-8.28424 -8.28423,-15.0001 -3.34502e-006,-15.0001 Z ");
                        break;
                    }
                case LedShape.Triangle:
                    {
                        PlayfieldGeometry = Geometry.Parse("F1 M -0.0711765,-13.8299C -0.675348,-13.809 -1.27253,-13.2396 -1.86975,-12.6702L -15.3698,10.7673C -15.5364,11.4549 -15.7031,12.1424 -15.4948,12.6528C -15.2864,13.1632 -14.7031,13.4965 -14.1198,13.8298L 14.5677,13.8298C 14.9636,13.4549 15.3594,13.0798 15.4948,12.5903C 15.6302,12.1007 15.5053,11.4965 15.3802,10.8924L 1.7552,-12.7952C 1.14408,-13.3229 0.532995,-13.8507 -0.0711765,-13.8299 Z ");
                        break;
                    }
                case LedShape.ThinTriangleLarge:
                    {
                        PlayfieldGeometry = Geometry.Parse("F1 M 0.000930081,-24.8906C -0.920839,-24.8906 -1.82072,-24.0176 -2.33189,-23.3763C -2.84307,-22.7349 -2.9655,-22.3251 -3.08794,-21.9153L -12.4321,18.7056C -12.8989,20.7766 -13.3657,22.8475 -12.7748,23.8783C -12.1838,24.9091 -10.5352,24.8998 -8.88647,24.8906L 8.8415,24.8906C 10.5166,24.8998 12.1917,24.9091 12.7854,23.8646C 13.3791,22.8199 12.8913,20.7216 12.4036,18.6231L 3.15564,-21.7698C 3.04036,-22.2281 2.92512,-22.6864 2.39567,-23.3521C 1.86624,-24.0176 0.922671,-24.8906 0.000930081,-24.8906 Z ");
                        break;
                    }
                case LedShape.ThinTriangleSmall:
                    {
                        PlayfieldGeometry = Geometry.Parse("F1 M 0.000617974,-18.8644C -0.58162,-18.8644 -1.15003,-18.2028 -1.47291,-17.7168C -1.7958,-17.2307 -1.87314,-16.9201 -1.95047,-16.6096L -7.8527,14.1769C -8.14757,15.7464 -8.44244,17.316 -8.06917,18.0972C -7.69591,18.8785 -6.65452,18.8715 -5.61313,18.8644L 5.58478,18.8644C 6.64286,18.8714 7.70096,18.8785 8.07596,18.0868C 8.45095,17.2951 8.14286,15.7048 7.83477,14.1144L 1.9933,-16.4992C 1.92048,-16.8466 1.84769,-17.1939 1.51326,-17.6984C 1.17885,-18.2028 0.582837,-18.8644 0.000617974,-18.8644 Z ");
                        break;
                    }
                case LedShape.Arrow:
                    {
                        PlayfieldGeometry = Geometry.Parse("F1 M 0.272468,-27.5245C 1.47512,-27.5972 2.09884,-26.4898 2.09884,-26.4898L 11.4344,-5.81561C 11.7119,-5.20111 11.9893,-4.58673 11.8446,-4.08051C 11.6999,-3.57428 11.133,-3.17627 10.5661,-2.77826L 6.41625,0.135193C 6.41625,0.135193 6.48853,0.484741 6.48853,0.669922L 6.48831,25.5245C 6.48831,26.6291 5.59288,27.5245 4.48831,27.5245L -4.13669,27.5245C -5.24126,27.5245 -6.13669,26.6291 -6.13669,25.5245L -6.13647,0.669922C -6.12904,0.402771 -6.06418,0.135193 -6.06418,0.135193L -10.1796,-2.71399C -10.9058,-3.21674 -11.632,-3.71948 -11.8446,-4.27856C -12.0571,-4.83765 -11.7561,-5.45306 -11.455,-6.06848L -1.52612,-26.3648C -1.52612,-26.3648 -0.816542,-27.6806 0.272468,-27.5245 Z ");
                        break;
                    }
                default:
                    {
                        PlayfieldGeometry = Geometry.Parse("F1 M -3.34502e-006,-15.0001C 8.28422,-15.0001 15,-8.28424 15,-6.10352e-005C 15,8.28424 8.28427,14.9999 -3.34502e-006,14.9999C -8.28427,14.9999 -15,8.28424 -15,-6.10352e-005C -15,-8.28424 -8.28423,-15.0001 -3.34502e-006,-15.0001 Z ");
                        break;
                    }
            }
        }

    }
}
