using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PictureGallery.Speech
{
    /// <summary>
    /// Contains information about an event that designates
    /// an input received from a voice command.
    /// </summary>
    public class BeforeInputEventArgs : EventArgs
    {
        /// <summary>
        /// Creates an instance of the <see cref="BeforeInputEventArgs"/> class.
        /// </summary>
        public BeforeInputEventArgs()
        {
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
