using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PinballClient.ClientDisplays.Dialogs;

namespace PinballClient.ClientDisplays.ModeTree
{
    public class ModeRequiredDeviceViewModel : Screen
    {
        #region Fields

        private string _associatedDevice;
        private ushort _associatedDeviceId;
        private IGameState _gameState;

        #endregion

        #region Properties

        public string ModeEventDeviceName { get; private set; }

        public string TypeOfDevice { get; private set; }

        public string AssociatedDevice
        {
            get
            {
                return _associatedDevice;
            }
            set
            {
                _associatedDevice = value;
                NotifyOfPropertyChange(()=> AssociatedDevice);
            }
        }

        public ushort AssociatedDeviceId
        {
            get
            {
                return _associatedDeviceId;
            }
            set
            {
                _associatedDeviceId = value;
                NotifyOfPropertyChange(() => AssociatedDeviceId);
            }
        }

        #endregion

        #region Constructor

        public ModeRequiredDeviceViewModel(IGameState gameState, string typeOfDevice, string modeEventDeviceName, ushort associatedDeviceId, string associatedDevice)
        {
            _gameState = gameState;
            ModeEventDeviceName = modeEventDeviceName;
            TypeOfDevice = typeOfDevice;
            _associatedDeviceId = associatedDeviceId;
            _associatedDevice = associatedDevice;
        }

        #endregion

        public async void SelectAssociatedDevice()
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);

            var devices = new List<string>();
            switch (TypeOfDevice)
            {
                case "Event":
                {
                    devices = _gameState.Switches.Select(sw => sw.Name).ToList();
                    break;
                }
                case "Switch":
                {
                    devices = _gameState.Switches.Select(sw => sw.Name).ToList();
                    break;
                }
                case "Coil":
                {
                    devices = _gameState.Coils.Select(coil => coil.Name).ToList();
                    break;
                }
                default:
                {
                    devices = _gameState.Switches.Select(sw => sw.Name).ToList();
                    break;
                }

            }

            var dialog = new DeviceSelectorDialog(devices, metroWindow);
            await metroWindow.ShowMetroDialogAsync(dialog);

            var result = await dialog.WaitForButtonPressAsync();
            if (!string.IsNullOrEmpty(result))
            {
                AssociatedDevice = result;
            }
            await metroWindow.HideMetroDialogAsync(dialog);
        }
    }
}

