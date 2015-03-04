using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PictureGallery.Speech
{
    /// <summary>
    /// Contains information about the <see cref="SpeechRecognitionMetadata.AfterInput"/> event.
    /// </summary>
    public class AfterInputEventArgs : EventArgs
    {
        /// <summary>
        /// Gets a boolean value determining whether the input operation was successful.
        /// </summary>
        public bool IsSuccess
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets a string representing the received input
        /// from a speech command.
        /// </summary>
        public string Input
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the input target <see cref="UIElement"/>.
        /// </summary>
        public UIElement Target
        {
            get;
            internal set;
        }
    }
}
