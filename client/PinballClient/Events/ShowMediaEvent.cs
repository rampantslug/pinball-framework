using System;
using BusinessObjects;
using Common;

namespace PinballClient.Events
{
    /// <summary>
    /// Event to initiate showing of a Media file
    /// </summary>
    public class ShowMediaEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public string MediaName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MediaType MediaType { get; set; }
    }

}
