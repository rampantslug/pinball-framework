using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Micro;

namespace PinballClient.ClientDisplays.LedShowTimeline
{
    [Export(typeof (ILedShowTimeline))]
    public class LedShowTimelineViewModel : Screen, ILedShowTimeline//, IHandle<MaxFramesUpdatedEvent>, IHandle<ShowSelectedEvent>
    {
       // public ILeds LedsVm { get; set; }

       /* public LedInShowViewModel SelectedLed
        {
            get
            {
                if (LedsVm.SelectedShow != null)
                {
                    var matchingLedInShow = LedsVm.SelectedShow.Leds.FirstOrDefault(led => led.LinkedLed == LedsVm.SelectedLed);
                    return matchingLedInShow;
                }

                return null;
            }
            set
            {
                // Update linked led...
                if (value != null) // Somehow value can be null?!?
                {
                    LedsVm.SelectedLed = value.LinkedLed;
                    NotifyOfPropertyChange(() => SelectedLed);
                }
            }
        }*/

        [ImportingConstructor]
        public LedShowTimelineViewModel(IEventAggregator eventAggregator, IGameState gameState)
        {
            _eventAggregator = eventAggregator;
           // LedsVm = ledsViewModel;

            _eventAggregator.Subscribe(this);

            DisplayName = "LedShow Timeline";
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            _eventAggregator.PublishOnUIThread(new DisplayLoadedEvent());
            // Need to break MVVM to dynamically generate part of the view
            // _timelineView = view as TimelineView;
        }

        #region Playback Control

        public void FirstFrame()
        {
           // LedsVm.IsPlaying = false;
            //LedsVm.CurrentFrame = 0;
        }

        public void Pause()
        {
            //LedsVm.IsPlaying = false;
        }

        public void PlayPause()
        {
            //LedsVm.IsPlaying = !LedsVm.IsPlaying;
        }

        public void StepBack()
        {
            //LedsVm.IsPlaying = false;
            //LedsVm.CurrentFrame--;
        }

        public void StepForward()
        {
            //LedsVm.IsPlaying = false;
           // LedsVm.CurrentFrame++;
        }

        public void LastFrame()
        {
           // LedsVm.IsPlaying = false;

           // if (LedsVm.SelectedShow != null)
           // {
           //     LedsVm.CurrentFrame = LedsVm.SelectedShow.Frames - 1;
           // }
        }

        public void LastFrameOfLastEvent()
        {
           // LedsVm.IsPlaying = false;

          //  if (LedsVm.SelectedShow != null && SelectedLed != null)
          //  {
          //      var lastEvent = SelectedLed.GetLastEvent();
          //      if (lastEvent != null)
          //      {
          //          LedsVm.CurrentFrame = lastEvent.EndFrame;
          //          return;
          //      }
          //  }

            // If cant get event then jump to end of show 
          //  LastFrame();            
        }

        #endregion

        public void ExecuteLedRowCommand(Key key)
        {
            if (key == Key.D1)
            {
        //        LedsVm.AddEvent(LedsVm.CurrentFrame, LedsVm.CurrentFrame + 2);
            }
            else if (key == Key.D2)
            {
       //         LedsVm.AddEvent(LedsVm.CurrentFrame, LedsVm.CurrentFrame + 4);
            }
            else if (key == Key.D3)
            {
       //         LedsVm.AddEvent(LedsVm.CurrentFrame, LedsVm.CurrentFrame + 8);
            }
            else if (key == Key.D4)
            {
      //          LedsVm.AddEvent(LedsVm.CurrentFrame, LedsVm.CurrentFrame + 16);
            }
            else if (key == Key.D5)
            {
     //           LedsVm.AddEvent(LedsVm.CurrentFrame, LedsVm.CurrentFrame + 32);
            }
        }

        /*    public void Handle(MaxFramesUpdatedEvent message)
            {
                if (LedsVm.SelectedShow != null)
                {            
                    var showLength = LedsVm.SelectedShow.Frames;
                    if (_currentDisplayedMaxFrames != showLength)
                    {
                        _currentDisplayedMaxFrames = showLength;
                        GenerateTickMarks();
                    }
                }           
            }

            public void Handle(ShowSelectedEvent message)
            {
                if (LedsVm.SelectedShow != null)
                {
                    var showLength = LedsVm.SelectedShow.Frames;
                    if (_currentDisplayedMaxFrames != showLength)
                    {
                        _currentDisplayedMaxFrames = showLength;
                        GenerateTickMarks();
                    }
                }  
            }*/


        /*
                private void GenerateTickMarks()
                {
                    if (_timelineView != null && LedsVm.SelectedShow != null)
                    {
                        _timelineView.TickCanvas.Children.Clear();
                        var showLength = LedsVm.SelectedShow.Frames;

                        for (int frame = 0; frame <= showLength; frame++)
                        {
                            int remainder = frame%16;
                            var xPos = frame*5;
                            var line = new Line
                            {
                                X1 = xPos,
                                X2 = xPos,
                                Y1 = 20,
                                Stroke = Brushes.Black
                            };

                            if (remainder == 0) // Major Tick mark
                            {                       
                                line.Y2 = 0;

                                // Also add text label                      
                                int firstChar = frame/32;
                                var textString = firstChar.ToString();

                                int secondCharRemainder = frame%32;

                                if (secondCharRemainder == 16)
                                {
                                    textString = textString + ".5";
                                }

                                var textBlock = new TextBlock()
                                {
                                   Text = textString
                                };
                                Canvas.SetLeft(textBlock, xPos + 3);
                                Canvas.SetTop(textBlock, 0);

                                _timelineView.TickCanvas.Children.Add(textBlock);
                            }
                            else if (remainder == 8) // Minor Tick mark
                            {
                                line.Y2 = 10;
                            }
                            else
                            {
                                int oddEven = frame%2;
                                if (oddEven == 0) // Even Tick mark
                                {
                                    line.Y2 = 16;
                                }
                                else
                                {
                                    line.Y2 = 18;
                                }
                            }
                            _timelineView.TickCanvas.Children.Add(line);                    
                        }
                        _timelineView.TickCanvas.UpdateLayout();
                    }
                }*/


        #region Handle Mouse movement / Dragging of Event

        public void MouseEnter(object source)
        {
            var eventContainer = source as Grid;
            if (eventContainer != null)
            {
            }
        }

        public void MouseLeave(object source)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        //private Point StartingPoint { get; set; }
        //private bool _isDraggingStartFrame;

        //private uint _scaleFactor = 5;

   //     private EventViewModel _activeEvent = null;
   //     private LedInShowViewModel _selectedLed;
       // private bool _hoverEdgeActive;

        public void PreviewMouseLeftButtonDown(object source)
        {
            var listView = source as ListView;
            if (listView != null)
            {
               // var position = Mouse.GetPosition(listView);

                // If over an event then need to select it...
//                var mouseOverEvent = GetEventUnderCursor(position);
//                if (mouseOverEvent != null)
//                {
//                    // Set old event to not selected
//                    if (LedsVm.SelectedShow.SelectedEvent != null)
//                    {
//                        LedsVm.SelectedShow.SelectedEvent.IsSelected = false;
//                    }
//                    mouseOverEvent.IsSelected = true;
//                    LedsVm.SelectedShow.SelectedEvent = mouseOverEvent;
//                }
            }



//            if (listView != null && SelectedLed != null)
//            {
//                var position = Mouse.GetPosition(listView);
//
//                // Get the LedInShow row that mouse is over...
//                var ledIndex = LedsVm.SelectedShow.Leds.IndexOf(SelectedLed);
//                var rowHeight = 25; // NOTE: If this is changed in xaml then this needs to be updated. Possibly bind xaml to this value.
//
//                var selectedRowY1 = ledIndex * rowHeight;
//                var selectedRowY2 = ledIndex * rowHeight + rowHeight;
//
//                if (position.Y >= selectedRowY1 && position.Y <= selectedRowY2)
//                {
//                    var variance = 5;
//                    foreach (var eventViewModel in SelectedLed.Events)
//                    {
//                        var startIndex = eventViewModel.StartFrame * _scaleFactor;
//                        var endIndex = eventViewModel.EndFrame * _scaleFactor;
//
//                        if (position.X > startIndex - variance && position.X < startIndex + variance)
//                        {
//                            StartingPoint = Mouse.GetPosition(listView);
//                            _activeEvent = eventViewModel;
//                            break;
//                        }
//                        else if (position.X > endIndex - variance && position.X < endIndex + variance)
//                        {
//                            StartingPoint = Mouse.GetPosition(listView);
//                            _activeEvent = eventViewModel;
//                            break;
//                        }
//                    }
//                    if (!_hoverEdgeActive)
//                    {
//                        LedsVm.CurrentFrame = (uint)position.X / _scaleFactor;
//                    }
//                }
//            }
        }

//        private EventViewModel GetEventUnderCursor(Point cursorPosition)
//        {
//            var rowHeight = 25; // NOTE: If this is changed in xaml then this needs to be updated. Possibly bind xaml to this value.
//            int activeRow = (int)cursorPosition.Y / rowHeight;
//            uint frameNo = (uint)cursorPosition.X / 5;
//
//            if (activeRow >= LedsVm.SelectedShow.Leds.Count)
//            {
//                return null;
//            }
//
//            // Get matching Led
//            var matchingLed = LedsVm.SelectedShow.Leds[activeRow];
//
//            foreach (var eventViewModel in matchingLed.Events)
//            {
//                if (eventViewModel.StartFrame <= frameNo && eventViewModel.EndFrame >= frameNo)
//                {
//                    return eventViewModel;
//                }
//            }
//            return null;
//        }


        public void PreviewMouseLeftButtonUp(object source)
        {
           // _activeEvent = null;
        }

        public void MouseMove(object source)
        {


//            var listView = source as ListView;
//            if (listView != null && SelectedLed != null)
//            {
//                var position = Mouse.GetPosition(listView);
//
//                // Get the LedInShow row that mouse is over...
//                var ledIndex = LedsVm.SelectedShow.Leds.IndexOf(SelectedLed);
//                var rowHeight = 25; // NOTE: If this is changed in xaml then this needs to be updated. Possibly bind xaml to this value.
//
//                var selectedRowY1 = ledIndex * rowHeight;
//                var selectedRowY2 = ledIndex * rowHeight + rowHeight;
//
//                if (position.Y >= selectedRowY1 && position.Y <= selectedRowY2)
//                {
//                    var variance = 5;
//                    foreach (var eventViewModel in SelectedLed.Events)
//                    {
//                        var startIndex = eventViewModel.StartFrame * _scaleFactor;
//                        var endIndex = eventViewModel.EndFrame * _scaleFactor;
//
//                        if (position.X > startIndex - variance && position.X < startIndex + variance)
//                        {
//                            Mouse.OverrideCursor = Cursors.SizeWE;
//                            _isDraggingStartFrame = true;
//                            _activeEvent = eventViewModel;
//                            _hoverEdgeActive = true;
//                            break;
//                        }
//                        else if (position.X > endIndex - variance && position.X < endIndex + variance)
//                        {
//                            //Mouse.SetCursor(Cursors.SizeWE);
//                            Mouse.OverrideCursor = Cursors.SizeWE;
//                            _isDraggingStartFrame = false;
//                            _activeEvent = eventViewModel;
//                            _hoverEdgeActive = true;
//                            break;
//                        }
//                    }
//
//                    if (_activeEvent != null)
//                    {
//                        if (Mouse.LeftButton == MouseButtonState.Pressed)
//                        {
//                            var xDelta = position.X - StartingPoint.X;
//                            if (xDelta >= _scaleFactor || xDelta <= -_scaleFactor)
//                            {
//                                var amount = (int)xDelta / _scaleFactor;
//                                if (_isDraggingStartFrame)
//                                {
//                                    _activeEvent.StartFrame = (uint)(_activeEvent.StartFrame + amount);
//                                }
//                                else
//                                {
//                                    _activeEvent.EndFrame = (uint)(_activeEvent.EndFrame + amount);
//                                }
//
//                                // Reset the starting point
//                                StartingPoint = position;
//                            }
//                            _hoverEdgeActive = true;
//                        }
//                    }
//                    if (!_hoverEdgeActive)
//                    {
//                        Mouse.OverrideCursor = Cursors.Arrow;
//                        _activeEvent = null;
//                    }
//                }
//                _hoverEdgeActive = false;
//            }
        }

        #endregion


        private readonly IEventAggregator _eventAggregator;
  //      private TimelineView _timelineView;
       // private uint _currentDisplayedMaxFrames;

    }
}