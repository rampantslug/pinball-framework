using System.Windows.Media;
using PinballClient.CommonViewModels;

namespace PinballClient.Events
{
    /// <summary>
    /// Event to notify UI that playfield image has been populated in the ShellViewModel
    /// </summary>
    public class UpdatePlayfieldImageEvent
    {
        /// <summary>
        /// Playfield Image in string format (required conversion for display)
        /// </summary>
        public string PlayfieldImage { get; set; }
    }
}
