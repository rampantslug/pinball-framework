using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BusinessObjects.LedShows;
using Caliburn.Micro;
using PinballClient.Helpers;

namespace PinballClient.CommonViewModels
{
    public class EventViewModel : Screen
    {
        public uint StartFrame
        {
            get
            {
                return _startFrame;
            }
            set
            {
                _startFrame = value;
                NotifyOfPropertyChange(() => StartFrame);
                NotifyOfPropertyChange(() => EventLength);
            }
        }

        public uint EndFrame
        {
            get
            {
                return _endFrame;
            }
            set
            {
                _endFrame = value;
                NotifyOfPropertyChange(() => EndFrame);
                NotifyOfPropertyChange(() => EventLength);
            }
        }

        public uint EventLength
        {
            get
            {
                return _endFrame - _startFrame;
            }
            set
            {
                _endFrame = value + _startFrame;
                NotifyOfPropertyChange(() => EventLength);
                NotifyOfPropertyChange(() => EndFrame);
            }
        }

        public Color StartColor
        {
            get
            {
                return _startColor;
            }
            set
            {
                _startColor = value;
                NotifyOfPropertyChange(() => StartColor);
                UpdateBrush();
            }
        }

        public Color EndColor
        {
            get
            {
                return _endColor;
            }
            set
            {
                _endColor = value;
                NotifyOfPropertyChange(() => EndColor);
                UpdateBrush();
            }
        }

        public Brush EventBrush
        {
            get
            {
                return _eventBrush;
            }
            set
            {
                _eventBrush = value;
                NotifyOfPropertyChange(() => EventBrush);
            }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        public int ShiftAmount
        {
            get { return _shiftAmount; }
            set
            {
                _shiftAmount = value;
                ShiftEvent(_shiftAmount);
                _shiftAmount = 0;
                NotifyOfPropertyChange(() => ShiftAmount);

            }
        }

        public EventViewModel(uint startFrame, uint endFrame, Color startColor, Color endColor)
        {
            _startFrame = startFrame;
            _endFrame = endFrame;
            _startColor = startColor;
            _endColor = endColor;

            UpdateBrush();
        }

        public bool Contains(uint frame)
        {
            return frame >= StartFrame && frame <= EndFrame;
        }

        public Brush GetColor(uint frame)
        {
            // Single colour event
            var solidBrush = EventBrush as SolidColorBrush;
            if (solidBrush != null)
            {
                return EventBrush;
            }
            else // Gradient Brush
            {
                var linearGradBrush = EventBrush as LinearGradientBrush;
                if (linearGradBrush != null)
                {
                    // Normalise frame position to length event
                    var positionInEvent = frame - StartFrame;
                    var offset = (double)positionInEvent / EventLength;

                    var offsetColor = linearGradBrush.GradientStops.GetRelativeColor(offset);
                    return new SolidColorBrush(offsetColor);
                }
            }
            return EventBrush;
        }

        private void UpdateBrush()
        {
            if (StartColor == EndColor)
            {
                EventBrush = new SolidColorBrush(StartColor);
            }
            else
            {
                EventBrush = new LinearGradientBrush(StartColor, EndColor, 0);
            }
        }

        public bool ContainsFrame(int frameNo)
        {
            return frameNo >= StartFrame && frameNo < EndFrame;
        }

        public void ShiftEvent(int shiftAmount)
        {
            StartFrame = (uint)(StartFrame + shiftAmount);
            EndFrame = (uint)(EndFrame + shiftAmount);
        }

        public LedEvent ConvertToConfiguration()
        {
            var configuration = new LedEvent
            {
                StartFrame = StartFrame,
                EndFrame = EndFrame,
                StartColor = StartColor,
                EndColor = EndColor
            };
            return configuration;
        }


        private uint _startFrame;
        private uint _endFrame;
        private Brush _eventBrush;
        private bool _isSelected;
        private Color _startColor;
        private Color _endColor;
        private int _shiftAmount;

      
    }
}

