using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BusinessObjects;
using BusinessObjects.Devices;
using Caliburn.Micro;
using Common;
using Configuration;
using Logging;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using PinballClient.ClientComms;
using PinballClient.ClientDisplays;
using PinballClient.ClientDisplays.DeviceInformation;
using PinballClient.ClientDisplays.DeviceTree;
using PinballClient.ClientDisplays.GameStatus;
using PinballClient.ClientDisplays.LedShowEditor;
using PinballClient.ClientDisplays.LedShowTimeline;
using PinballClient.ClientDisplays.LogMessages;
using PinballClient.ClientDisplays.MediaTree;
using PinballClient.ClientDisplays.ModeTree;
using PinballClient.ClientDisplays.Playfield;
using PinballClient.ClientDisplays.PlayfieldProperties;
using PinballClient.ClientDisplays.ShowsList;
using PinballClient.ClientDisplays.SwitchMatrix;
using PinballClient.CommonViewModels.Devices;
using PinballClient.Events;
using Image = System.Windows.Controls.Image;

namespace PinballClient
{

    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>.Collection.AllActive, 
        IShell, 
        IDataErrorInfo,

        IHandle<DisplayLoadedEvent>,
        IHandle<UpdateConfigEvent>,
        IHandle<ShowSwitchConfigEvent>,
        IHandle<ShowMediaEvent>,

        // Device State Changes
        IHandle<UpdateSwitchEvent>,
        IHandle<UpdateCoilEvent>,
        IHandle<UpdateStepperMotorEvent>,
        IHandle<UpdateServoEvent>,
        IHandle<UpdateLedEvent>
    {

        // Client display Modules
        public IDeviceInformation DeviceInformation { get; private set; }
        public IDeviceTree DeviceTree { get; private set; }
        public IGameStatus GameStatus { get; private set; }
        public ILedShowEditor LedShowEditor { get; private set; }
        public ILedShowTimeline LedShowTimeline { get; private set; }
        public ILogMessages LogMessages { get; private set; }
        public IMediaTree MediaTree { get; private set; }
        public IModeTree ModeTree { get; private set; }
        public IPlayfield Playfield { get; private set; }
        public IPlayfieldProperties PlayfieldProperties { get; private set; }
        public IShowsList ShowsList { get; private set; }
        public ISwitchMatrix SwitchMatrix { get; private set; }

        public BindableCollection<IScreen> LeftTabs
        {
            get
            {
                return _leftTabs;
            }
            set
            {
                _leftTabs = value;
                NotifyOfPropertyChange(() => LeftTabs);
            }
        }

        public BindableCollection<IScreen> MidTabs
        {
            get
            {
                return _midTabs;
            }
            set
            {
                _midTabs = value;
                NotifyOfPropertyChange(() => MidTabs);
            }
        }

        public BindableCollection<IScreen> RightTabs
        {
            get
            {
                return _rightTabs;
            }
            set
            {
                _rightTabs = value;
                NotifyOfPropertyChange(() => RightTabs);
            }
        }

        public BindableCollection<IScreen> BottomTabs
        {
            get
            {
                return _bottomTabs;
            }
            set
            {
                _bottomTabs = value;
                NotifyOfPropertyChange(() => BottomTabs);
            }
        }

        public bool SettingsFlyoutIsOpen
        {
            get
            {
                return _settingsFlyoutIsOpen;
            }
            set
            {
                _settingsFlyoutIsOpen = value;
                NotifyOfPropertyChange(() => SettingsFlyoutIsOpen);
            }
        }

        public bool IsConfigLoaded
        {
            get { return _isConfigLoaded; }
            set
            {
                if (value == _isConfigLoaded) return;
                _isConfigLoaded = value;
                NotifyOfPropertyChange(() => IsConfigLoaded);
            }
        }

        public bool IsEditingConfigName
        {
            get { return _isEditingConfigName; }
            set
            {
                if (value == _isEditingConfigName) return;
                _isEditingConfigName = value;
                NotifyOfPropertyChange(() => IsEditingConfigName);
            }
        }

        #region Client Settings - Saved in App.config

        public bool UseServer
        {
            get { return _useServer; }
            set
            {
                if (value == _useServer) return;
                _useServer = value;
                NotifyOfPropertyChange(() => UseServer);
            }
        }

        public string ServerIpAddress
        {
            get
            {
                return _serverIpAddress;
            }
            set
            {
                _serverIpAddress = value;
                NotifyOfPropertyChange(() => ServerIpAddress);
            }
        }

        public bool ConnectedToServer
        {
            get
            {
                return _connectedToServer;
            }
            set
            {
                _connectedToServer = value;
                NotifyOfPropertyChange(() => ConnectedToServer);
            }
        }

        public string LocalConfigLocation
        {
            get { return _localConfigLocation; }
            set
            {
                if (value == _localConfigLocation) return;
                // Check validity before setting?
                if (string.IsNullOrEmpty(value) || !File.Exists(value))
                {
                    _localConfigLocation = "Click to set file location.";
                }
                else
                {
                    _localConfigLocation = value;
                }
                
                NotifyOfPropertyChange(() => LocalConfigLocation);
            }
        }

        #endregion

        #region Server Settings - Saved in json config

        public string ConfigName
        {
            get
            {
                return _configName;
            }
            set
            {
                _configName = value;
                NotifyOfPropertyChange(() => ConfigName);
            }
        }

        public bool ServerIsUsingHardware
        {
            get
            {
                return _serverIsUsingHardware;
            }
            set
            {
                _serverIsUsingHardware = value;
                NotifyOfPropertyChange(() => ServerIsUsingHardware);
            }
        }

        #endregion

        public string MediaBaseFileLocation { get; set; }

        public string Error => string.Empty;


        #region Constructor

        /// <summary>
        /// Constructor for the ShellViewModel. Main container for Client application elements.
        /// Imports required UI elements via DI.
        /// </summary>
        [ImportingConstructor]
        public ShellViewModel(
            IEventAggregator eventAggregator,
            IClientCommsController clientCommsController,
            IClientToLocalCommsController clientToLocalCommsController,
            IClientToServerCommsController clientToServerCommsController,
            IGameState gameState,
            
            // Displays
            IDeviceInformation deviceInformation,             
            IDeviceTree deviceTree,
            IGameStatus gameStatus,
            ILedShowEditor ledShowEditor,
            ILedShowTimeline ledShowTimeline,
            ILogMessages logMessages,
            IMediaTree mediaTree,
            IModeTree modeTree,
            IPlayfield playfield,
            IPlayfieldProperties playfieldProperties,
            IShowsList showsList,
            ISwitchMatrix switchMatrix
            ) 
        {
            _eventAggregator = eventAggregator;
            _clientCommsController = clientCommsController;
            _clientToLocalCommsController = clientToLocalCommsController;
            _clientToServerCommsController = clientToServerCommsController;
            _gameState = gameState;

            // Displays
            LogMessages = logMessages;
            DeviceInformation = deviceInformation;
            
            DeviceTree = deviceTree;
            GameStatus = gameStatus;
            LedShowEditor = ledShowEditor;
            LedShowTimeline = ledShowTimeline;
            MediaTree = mediaTree;
            ModeTree = modeTree;
            Playfield = playfield;
            PlayfieldProperties = playfieldProperties;
            ShowsList = showsList;
            SwitchMatrix = switchMatrix;

            LeftTabs = new BindableCollection<IScreen>();
            MidTabs = new BindableCollection<IScreen>();
            RightTabs = new BindableCollection<IScreen>();
            BottomTabs = new BindableCollection<IScreen>();

            // ReSharper disable once VirtualMemberCallInConstructor
            DisplayName = "RS Pinball Client";
            ConfigName = "Not connected to server";
            ConnectedToServer = false;

            GetClientSettingsFromAppConfig();

            if (UseServer)
            {
                RsLog.Info("Attempting to contact server");
                // Set ClientCommsController to use server controller

                _clientCommsController.CurrentController = _clientToServerCommsController as IClientCommsController;
                _clientToServerCommsController.Start(ServerIpAddress);

                // If connection fails then revert to standalone mode
            }           
        }

        #endregion


        private void GetClientSettingsFromAppConfig()
        {
            UseServer = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("UseServer"));
            RsLog.Info("Retrieved key|value from App.Config: UseServer | " + UseServer);

            ServerIpAddress = ConfigurationManager.AppSettings.Get("DefaultServer");
            RsLog.Info("Retrieved key|value from App.Config: DefaultServer | " + ServerIpAddress);

            LocalConfigLocation = ConfigurationManager.AppSettings.Get("LocalConfigLocation");
            RsLog.Info("Retrieved key|value from App.Config: LocalConfigLocation | " + LocalConfigLocation);
        }

        public void Exit()
        {
            if (UseServer)
            {
                _clientToServerCommsController.Stop();
            }
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            LeftTabs.Add(DeviceTree);
            LeftTabs.Add(ModeTree);
            LeftTabs.Add(MediaTree);
            LeftTabs.Add(ShowsList);

            MidTabs.Add(DeviceInformation);
            MidTabs.Add(SwitchMatrix);
            MidTabs.Add(GameStatus);
            MidTabs.Add(LedShowTimeline);

            BottomTabs.Add(LogMessages);
            BottomTabs.Add(PlayfieldProperties);
            BottomTabs.Add(LedShowEditor);

            _eventAggregator.Subscribe(this);        
        }

        public void LoadConfig()
        {
            if (UseServer)
            {

                // TODO: Need to check status server bus. Cant assume that it is working...
                _clientToServerCommsController.RequestSettings();
            }
            else
            {
                if (string.IsNullOrEmpty(LocalConfigLocation) || !File.Exists(LocalConfigLocation))
                {
                    RsLog.Error("Unable to load config from location: " + LocalConfigLocation);
                    return; 
                }

                var configuration = new RsConfiguration(LocalConfigLocation);
                UpdateCommonViewModels(configuration.MachineConfiguration);
                IsConfigLoaded = true;

                _clientToLocalCommsController.Configuration = configuration;
                var devices = new Devices();
                devices.LoadSwitches(configuration.MachineConfiguration.Switches);
                devices.LoadCoils(configuration.MachineConfiguration.Coils);
                devices.LoadLeds(configuration.MachineConfiguration.Leds);
                devices.LoadServos(configuration.MachineConfiguration.Servos);
                devices.LoadStepperMotors(configuration.MachineConfiguration.StepperMotors);
                _clientToLocalCommsController.MachineState = new MachineState(devices);
            }
        }

       

        protected override void OnDeactivate(bool close)
        {
            Exit();
            base.OnDeactivate(close);
        }

        public void Settings()
        {
            SettingsFlyoutIsOpen = !SettingsFlyoutIsOpen;
        }

        public void SaveAsDefault()
        {
            ConfigurationManager.AppSettings.Set("DefaultServer", ServerIpAddress);
        }

        public void RestartServer()
        {
            _clientToServerCommsController.RestartServer();
            SettingsFlyoutIsOpen = false;
        }

        public void ConnectToServer()
        {
            // Stop current connection and reconnect to possibly different server
            _clientToServerCommsController.Stop();
            _clientToServerCommsController.Start(ServerIpAddress);

            _clientToServerCommsController.RequestSettings();
            SettingsFlyoutIsOpen = false;
        }

        public void BrowseLocalConfigLocation()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "JSON File (*.json)|*.json",
                CheckFileExists = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (string.IsNullOrEmpty(openFileDialog.FileName) || !File.Exists(openFileDialog.FileName))
                {
                    RsLog.Warn("Unable to find config in location: " + openFileDialog.FileName);
                    return; // Need to put error message about file not found?
                }
                else
                {
                    LocalConfigLocation = openFileDialog.FileName;
                    // Save out to App.Config
                    UpdateSetting("LocalConfigLocation", LocalConfigLocation);
                }
            }
        }

        private static void UpdateSetting(string key, string value)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }

        public void Handle(DisplayLoadedEvent message)
        {
            // The Shell View model raises its OnViewLoaded event before child view models
            // We therefore need some other method to determine WHEN we can update our data and notify displays
            // This allows us to wait until displays are loaded (and have subscribed to events) and THEN load the config

            _displaysLoaded++;
            if(_displaysLoaded >= _numberOfDisplaysToWaitFor)
            { 
                RsLog.Info("Client views loaded. Loading default config");
                LoadConfig();
            }
        }


        /// <summary>
        /// Request to update config of a device. Make DeviceInformation active if not already
        /// </summary>
        /// <param name="deviceMessage"></param>
        public void Handle(ShowSwitchConfigEvent deviceMessage)
        {            
            Playfield.Deactivate(false);
            DeviceInformation.Activate();
        }

        public void Handle(UpdateConfigEvent updateConfigEvent)
        {
            UpdateCommonViewModels(updateConfigEvent.MachineConfiguration);
        }


        /// <summary>
        /// Update ALL ViewModels based on config results
        /// </summary>
        /// <param name="config"></param>
        public void UpdateCommonViewModels(IMachineConfiguration config)
        {
            _gameState.PlayfieldImage = config.PlayfieldImage;
            ConfigName = config.ServerName;
            ServerIsUsingHardware = config.UseHardware;
            ConnectedToServer = true;

            _gameState.Modes = config.Modes;
            _eventAggregator.PublishOnUIThread(new UpdateModesEvent());

            MediaBaseFileLocation = config.MediaBaseFileLocation;
            _gameState.Images = config.Images;
            _gameState.Videos = config.Videos;
            _gameState.Sounds = config.Sounds;
            _eventAggregator.PublishOnUIThread(new UpdateMediaEvent());

            UpdateDeviceViewModels(config.Switches, config.Coils, config.StepperMotors, config.Servos, config.Leds);

            _eventAggregator.PublishOnUIThread(new LogEvent
            {
                Timestamp = DateTime.Now,
                EventType = LogEventType.Info,
                OriginatorType = OriginatorType.System,
                OriginatorName = "Machine Settings",
                Status = "Received",
                Information = "Received machine settings from server."
            });

            // Notify Client Displays that Common View Models are updated
            _eventAggregator.PublishOnUIThread(new CommonViewModelsLoadedEvent());

            if (_gameState.Switches.Count > 0)
            {
                _eventAggregator.PublishOnUIThread(new ShowSwitchConfigEvent() { SwitchVm = _gameState.Switches[0] });
            }

            // Notify Client Displays that Playfield Image is updated
            _eventAggregator.PublishOnUIThread(new UpdatePlayfieldImageEvent() { PlayfieldImage = config.PlayfieldImage });
        }

        public void UpdateDeviceViewModels(IList<Switch> switches, IList<Coil> coils, IList<StepperMotor> stepperMotors, IList<Servo> servos, IList<Led> leds)
        {
            _gameState.Switches.Clear();
            foreach (var sw in switches)
            {
                _gameState.Switches.Add(new SwitchViewModel(sw, _clientCommsController, _eventAggregator));
            }

            _gameState.Coils.Clear();
            foreach (var coil in coils)
            {
                _gameState.Coils.Add(new CoilViewModel(coil, _clientCommsController, _eventAggregator));
            }

            _gameState.StepperMotors.Clear();
            foreach (var stepperMotor in stepperMotors)
            {
                _gameState.StepperMotors.Add(new StepperMotorViewModel(stepperMotor, _clientCommsController, _eventAggregator));
            }

            _gameState.Servos.Clear();
            foreach (var servo in servos)
            {
                _gameState.Servos.Add(new ServoViewModel(servo, _clientCommsController, _eventAggregator));
            }

            _gameState.Leds.Clear();
            foreach (var led in leds)
            {
                _gameState.Leds.Add(new LedViewModel(led, _clientCommsController, _eventAggregator));
            }
        }

        #region Update Individual Device View Models based on state changes

        /// <summary>
        /// Update the local Switch ViewModel based on state change.
        /// </summary>
        /// <param name="deviceMessage">Message containing updated device.</param>
        public void Handle(UpdateSwitchEvent deviceMessage)
        {
            var switchViewModel = _gameState.Switches.FirstOrDefault(swVM => swVM.Number == deviceMessage.Device.Number);

            if (switchViewModel != null)
            {
                 switchViewModel.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);

                _eventAggregator.PublishOnUIThread(new LogEvent
                {
                    Timestamp = deviceMessage.Timestamp,
                    EventType = LogEventType.Info,
                    OriginatorType = OriginatorType.Switch,
                    OriginatorName = switchViewModel.Name,
                    Status = switchViewModel.State,
                    Information = "Switch Event for: " + switchViewModel.Name
                });
            }
        }

        /// <summary>
        /// Update the local Coil ViewModel based on state change.
        /// </summary>
        /// <param name="deviceMessage">Message containing updated device.</param>
        public void Handle(UpdateCoilEvent deviceMessage)
        {
            var coilViewModel = _gameState.Coils.FirstOrDefault(coilVM => coilVM.Number == deviceMessage.Device.Number);

            if (coilViewModel != null)
            {
                coilViewModel.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);

                _eventAggregator.PublishOnUIThread(new LogEvent
                {
                    Timestamp = deviceMessage.Timestamp,
                    EventType = LogEventType.Info,
                    OriginatorType = OriginatorType.Coil,
                    OriginatorName = coilViewModel.Name,
                    Status = coilViewModel.State,
                    Information = "Coil Event for: " + coilViewModel.Name
                });
            }
        }

        /// <summary>
        /// Update the local StepperMotor ViewModel based on state change.
        /// </summary>
        /// <param name="deviceMessage">Message containing updated device.</param>
        public void Handle(UpdateStepperMotorEvent deviceMessage)
        {
            var stepperMotorViewModel = _gameState.StepperMotors.FirstOrDefault(stepperMotorVM => stepperMotorVM.Number == deviceMessage.Device.Number);

            if (stepperMotorViewModel != null)
            {
                stepperMotorViewModel.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);

                _eventAggregator.PublishOnUIThread(new LogEvent
                {
                    Timestamp = deviceMessage.Timestamp,
                    EventType = LogEventType.Info,
                    OriginatorType = OriginatorType.StepperMotor,
                    OriginatorName = stepperMotorViewModel.Name,
                    Status = stepperMotorViewModel.State,
                    Information = "StepperMotor Event for: " + stepperMotorViewModel.Name
                });
            }
        }

        /// <summary>
        /// Update the local Servo ViewModel based on state change.
        /// </summary>
        /// <param name="deviceMessage">Message containing updated device.</param>
        public void Handle(UpdateServoEvent deviceMessage)
        {
            var servoViewModel = _gameState.Servos.FirstOrDefault(servoVM => servoVM.Number == deviceMessage.Device.Number);

            if (servoViewModel != null)
            {
                servoViewModel.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);

                _eventAggregator.PublishOnUIThread(new LogEvent
                {
                    Timestamp = deviceMessage.Timestamp,
                    EventType = LogEventType.Info,
                    OriginatorType = OriginatorType.Servo,
                    OriginatorName = servoViewModel.Name,
                    Status = servoViewModel.State,
                    Information = "Servo Event for: " + servoViewModel.Name
                });
            }
        }

        /// <summary>
        /// Update the local Led ViewModel based on state change.
        /// </summary>
        /// <param name="deviceMessage">Message containing updated device.</param>
        public void Handle(UpdateLedEvent deviceMessage)
        {
            var ledViewModel = _gameState.Leds.FirstOrDefault(ledVM => ledVM.Number == deviceMessage.Device.Number);

            if (ledViewModel != null)
            {
                ledViewModel.UpdateDeviceInfo(deviceMessage.Device, deviceMessage.Timestamp);

                _eventAggregator.PublishOnUIThread(new LogEvent
                {
                    Timestamp = deviceMessage.Timestamp,
                    EventType = LogEventType.Info,
                    OriginatorType = OriginatorType.Led,
                    OriginatorName = ledViewModel.Name,
                    Status = ledViewModel.State,
                    Information = "Led Event for: " + ledViewModel.Name
                });
            }
        }

        #endregion

        #region Media Playing stuff - Refactor in Release 0.7

        public void Handle(ShowMediaEvent showMediaEvent)
        {
            var fileLocation = MediaBaseFileLocation + @"\" + showMediaEvent.MediaType.ToString() + @"s\";
            ReceiveMediaFromServer(fileLocation, showMediaEvent.MediaName ,showMediaEvent.MediaType);
        }

        public async void ReceiveMediaFromServer(string fileLocation, string fileName, MediaType mediaType)
        {
            _clientCommsController.RequestFile(fileLocation + fileName);
            var result = await ReadDataFromStream(fileName);
            if (result)
            {
                // Display the file at this point...
                _eventAggregator.PublishOnUIThread(new LogEvent
                {
                    Timestamp = DateTime.Now,
                    EventType = LogEventType.Info,
                    OriginatorType = OriginatorType.System,
                    OriginatorName = "Client",
                    Status = "Received",
                    Information = string.Format("Received {0} file from server.", fileName)
                });

                MetroWindow window = null;

                if (mediaType == MediaType.Image)
                {
                    // Copy file to temp
                    File.Copy(@"C:\Dev\" + fileName, @"C:\Dev\ViewerCache", true);

                    //create new stream and create bitmap frame
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new FileStream(@"C:\Dev\ViewerCache", FileMode.Open, FileAccess.Read);
                    //load the image now so we can immediately dispose of the stream
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    //clean up the stream to avoid file access exceptions when attempting to delete images
                    bitmapImage.StreamSource.Dispose();

                    window = this.GetTestWindow();

                    var tempImage = new Image()
                    {
                        Stretch = Stretch.UniformToFill,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Source = bitmapImage
                    };
                    window.Content = tempImage;
                    window.GlowBrush = tempImage.FindResource("AccentColorBrush") as SolidColorBrush;
                   
                }
                else if (mediaType == MediaType.Video)
                {
                    window = this.GetTestWindow();

                    var grid = new Grid();

                    var waitText = new TextBlock()
                    {
                        Text = "Please wait...", 
                        FontSize = 28, 
                        FontWeight = FontWeights.Light, 
                        VerticalAlignment = VerticalAlignment.Center, 
                        HorizontalAlignment = HorizontalAlignment.Center
                    };


                    var tempPlayer = new MediaElement
                    {
                        Source = new Uri(@"C:\Dev\" + fileName),
                        Stretch = Stretch.UniformToFill,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        LoadedBehavior = MediaState.Play,
                    };

                    grid.Children.Add(waitText);
                    grid.Children.Add(tempPlayer);
                    window.Content = grid;
                    window.GlowBrush = tempPlayer.FindResource("AccentColorBrush") as SolidColorBrush;
                }
                else if (mediaType == MediaType.Sound)
                {
                    window = this.GetTestWindow();

                    var grid = new Grid();

                    var waitText = new TextBlock()
                    {
                        Text = "Playing sound...",
                        FontSize = 28,
                        FontWeight = FontWeights.Light,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };


                    var tempPlayer = new MediaElement
                    {
                        Source = new Uri(@"C:\Dev\" + fileName),
                        Stretch = Stretch.UniformToFill,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        LoadedBehavior = MediaState.Play,
                    };

                    grid.Children.Add(waitText);
                    grid.Children.Add(tempPlayer);
                    window.Content = grid;
                    window.GlowBrush = tempPlayer.FindResource("AccentColorBrush") as SolidColorBrush;
                }

                // Show the window...
                if(window != null)
                {
                    window.BorderThickness = new Thickness(1);
                    window.BorderBrush = null;
                    window.Show(); 
                }
            }
        }

        private MetroWindow testWindow;

        private MetroWindow GetTestWindow()
        {
            if (testWindow != null)
            {
                testWindow.Close();
            }
            testWindow = new MetroWindow() { WindowStartupLocation = WindowStartupLocation.CenterScreen, Title = "Media Viewer", Width = 500, Height = 300 };
            testWindow.Closed += (o, args) => testWindow = null;
            return testWindow;
        }


        async Task<bool> ReadDataFromStream(string fileName)
        {
            var filelistener = new TcpListener(IPAddress.Parse(GetIP()), 8085);
            filelistener.Start();
            var client = filelistener.AcceptTcpClient();
            var buffer = new byte[1500];
            var bytesread = 1;

            var writer = new StreamWriter(@"C:\Dev\" + fileName);

            while (bytesread > 0)
            {
                bytesread = client.GetStream().Read(buffer, 0, buffer.Length);
                if (bytesread == 0)
                    break;
                await writer.BaseStream.WriteAsync(buffer, 0, buffer.Length);
            }
            writer.Close();
            filelistener.Stop();
            return true;
        }



        public string GetIP()
        {
            string name = Dns.GetHostName();
            IPHostEntry entry = Dns.GetHostEntry(name);
            IPAddress[] addr = entry.AddressList;
            if (addr[1].ToString().Split('.').Length == 4)
            {
                return addr[1].ToString();
            }
            return addr[2].ToString();
        }

        public string this[string columnName]
        {
            get
            {
                if (string.IsNullOrEmpty(ServerIpAddress))
                {
                    return "No IP Address specified.";
                }

                if (columnName == "ServerIpAddress" && !ValidateIpAddress(ServerIpAddress))
                {
                    return "IP Address is not correct format. E.g. 127.0.0.1";
                }

                return null;
            }
        }

        private bool ValidateIpAddress(string ipAddress)
        {
            var parts = ipAddress.Split('.');
            if (parts.Count() != 4)
            {
                return false;
            }
            foreach (var part in parts)
            {
                int value;
                if (!int.TryParse(part, out value))
                {
                    return false;
                }
                if (value < 0 || value > 255)
                {
                    return false;
                }
            }
            return true;
        }



        #endregion

        private readonly IClientCommsController _clientCommsController;
        private readonly IClientToLocalCommsController _clientToLocalCommsController;
        private readonly IClientToServerCommsController _clientToServerCommsController;
        private readonly IEventAggregator _eventAggregator;
        private readonly IGameState _gameState;

        // Containers to break up the UI elements
        private BindableCollection<IScreen> _leftTabs;
        private BindableCollection<IScreen> _midTabs;
        private BindableCollection<IScreen> _rightTabs;
        private BindableCollection<IScreen> _bottomTabs;

        private bool _settingsFlyoutIsOpen;
        private bool _connectedToServer;
        private string _configName;
        private bool _serverIsUsingHardware;
        private string _serverIpAddress;
        //private string _playfieldImage;
        private bool _useServer;
        private string _localConfigLocation;
        private bool _isConfigLoaded;
        private int _displaysLoaded = 0;
        private int _numberOfDisplaysToWaitFor = 4;
        private bool _isEditingConfigName;
    }
}