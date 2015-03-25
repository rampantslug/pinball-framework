using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.Model
{
    // Change this back to use EventAggregator


    // This is a simple customer class that  
    // implements the IPropertyChange interface. 
    public class TempData: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string someText = String.Empty;

        public string SomeText
        {
            get
            {
                return this.someText;
            }

            set
            {
                if (value != this.someText)
                {
                    this.someText = value;
                    NotifyPropertyChanged();
                }
            }
        }


        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // The constructor is private to enforce the factory pattern. 
        private TempData()
        {
            someText = "Customer";
        }

        // This is the public factory method. 
        public static TempData CreateNewTempData()
        {
            return new TempData();
        }
    }
}
