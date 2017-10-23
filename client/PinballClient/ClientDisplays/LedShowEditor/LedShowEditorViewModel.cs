using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace PinballClient.ClientDisplays.LedShowEditor
{
    [Export(typeof(ILedShowEditor))]
    class LedShowEditorViewModel : Screen, ILedShowEditor
    {
        [ImportingConstructor]
        public LedShowEditorViewModel()
        {
            DisplayName = "LedShowEditor";
        }
    }

 /*   public ILeds LedsVm { get; set; }
    public IPlayfield PlayfieldVm { get; set; }

    public IEnumerable<LedShape> AllShapes
    {
        get { return Enum.GetValues(typeof(LedShape)).Cast<LedShape>(); }
    }

    public ObservableCollection<ColorItem> ColorList
    {
        get { return _colorList; }
    }

    public bool NewEventMode
    {
        get { return _newEventMode; }
        set
        {
            if (value.Equals(_newEventMode)) return;
            _newEventMode = value;
            NotifyOfPropertyChange(() => NewEventMode);
            NotifyOfPropertyChange(() => CanAddEvent);
            NotifyOfPropertyChange(() => EventManipulationText);
        }
    }

    public string EventManipulationText
    {
        get { return NewEventMode ? "Adding Event" : "Editing Event"; }
    }

    [ImportingConstructor]
    public PropertiesViewModel(IEventAggregator eventAggregator, ILeds ledsViewModel, IPlayfield playfieldViewModel)
    {
        _eventAggregator = eventAggregator;
        _eventAggregator.Subscribe(this);
        LedsVm = ledsViewModel;
        PlayfieldVm = playfieldViewModel;

        DisplayName = "Properties";

        _colorList = new BindableCollection<ColorItem>()
            {
                new ColorItem(Colors.White, "White"),
                new ColorItem(Colors.Transparent, "Transparent")
            };
    }


    public void NewEvent()
    {
        // Update prevEvent values to be selected event as we want new one to 'follow on'
        _prevEventStartFrame = LedsVm.SelectedShow.SelectedEvent.StartFrame;
        _prevEventEndFrame = LedsVm.SelectedShow.SelectedEvent.EndFrame;
        _prevEventStartColor = LedsVm.SelectedShow.SelectedEvent.StartColor;
        _prevEventEndColor = LedsVm.SelectedShow.SelectedEvent.EndColor;

        NewEventMode = true;
        var duration = _prevEventEndFrame - _prevEventStartFrame;

        LedsVm.SelectedShow.SelectedEvent = new EventViewModel(
            _prevEventEndFrame, _prevEventEndFrame + duration, _prevEventEndColor, _prevEventEndColor);
    }

    public bool CanAddEvent
    {
        get { return _newEventMode; }
    }

    public void AddEvent()
    {
        LedsVm.AddEvent(LedsVm.SelectedShow.SelectedEvent);

        // Ready properties panel for next event
        NewEvent();
    }

    public void BrowsePlayfieldImage()
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Image Files (*.bmp,*.jpg,*.png)|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|" +
            "BMP (*.bmp)|*.bmp|JPG (*.jpg,*.jpeg)|*.jpg;*.jpeg|PNG (*.png)|*.png|TIF (*.tif,*.tiff)|*.tif;*.tiff",
            CheckFileExists = true
        };

        if (openFileDialog.ShowDialog() == true)
        {
            PlayfieldVm.UpdateImage(openFileDialog.FileName);
        }
    }

    public void ProcessChange()
    {
        if (LedsVm.SelectedLed != null)
        {
            NotifyOfPropertyChange(() => LedsVm.SelectedLed.Name);
        }
    }

    public void Handle(SingleColorLedColorModifiedEvent message)
    {
        UpdateColorList(message.NewColor);
    }

    public void Handle(SelectLedEvent message)
    {
        UpdateColorList(message.Led.SingleColor);
        NewEventMode = false; // Editing event

    }

    private void UpdateColorList(Color solidColor)
    {
        ColorList.Clear();
        ColorList.Add(new ColorItem(solidColor, "Solid Color"));
        ColorList.Add(new ColorItem(Colors.Transparent, "Transparent"));
    }

    private IEventAggregator _eventAggregator;
    private string _selectedSingleLedEventOption;
    private BindableCollection<ColorItem> _colorList;
    private bool _newEventMode;

    private uint _prevEventStartFrame;
    private uint _prevEventEndFrame;
    private Color _prevEventStartColor;
    private Color _prevEventEndColor;*/
}
