using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RampantSlug.Common
{
    public static class ColorBrushesHelper
    {

        public static ObservableCollection<string> GetColorStrings()
        {
            return new ObservableCollection<string>() { "Green", "Red", "Blue", "Yellow", "Pink", "White", "Brown", "Black", "Orange" };
        }

        public static SolidColorBrush ConvertStringToBrush(string colorName)
        {
            switch (colorName)
            {
                case "Green":
                    {
                        return Brushes.Green;
                    }
                case "Red":
                    {
                        return Brushes.Red;
                    }
                case "Blue":
                    {
                        return Brushes.Blue;
                    }
                case "Yellow":
                    {
                        return Brushes.Yellow;
                    }
                case "Pink":
                    {
                        return Brushes.Pink;
                    }
                case "White":
                    {
                        return Brushes.White;
                    }
                case "Brown":
                    {
                        return Brushes.Brown;
                    }
                case "Black":
                    {
                        return Brushes.Black;
                    }
                case "Orange":
                    {
                        return Brushes.Orange;
                    }
            }
            return Brushes.Transparent;
        }
    }
}
