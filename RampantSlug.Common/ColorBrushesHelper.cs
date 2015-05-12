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

        public static string ConvertBrushToString(SolidColorBrush brush)
        {
            var hexString = brush.ToString();

            switch (hexString)
            {
                case "#FF008000":
                    {
                        return "Green";
                    }
                case "#FFFF0000":
                    {
                        return "Red";
                    }
                case "#FF0000FF":
                    {
                        return "Blue";
                    }
                case "#FFFFFF00":
                    {
                        return "Yellow";
                    }
                case "#FFFFC0CB":
                    {
                        return "Pink";
                    }
                case "#FFFFFFFF":
                    {
                        return "White";
                    }
                case "#FFA52A2A":
                    {
                        return "Brown";
                    }
                case "#FF000000":
                    {
                        return "Black";
                    }
                case "#FFFFA500":
                    {
                        return "Orange";
                    }
            }
            return "White";
        }
    }
}
