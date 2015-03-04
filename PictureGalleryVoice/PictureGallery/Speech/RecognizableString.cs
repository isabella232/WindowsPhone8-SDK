using System;
using System.Linq;

namespace PictureGallery.Speech
{
    /// <summary>
    /// Represents a string that can be recognized by using voice input. An instance of this class
    /// can be associated with a <see cref="SpeechRecognitionMetadata"/> instance.
    /// </summary>
    public class RecognizableString
    {
        /// <summary>
        /// Gets or sets the identification token that determines the input
        /// element that will be activated after successfull input of the current <see cref="RecognizableString"/>.
        /// </summary>
        public object NextInputElementToken
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value of the recognizable string.
        /// </summary>
        public string Value
        {
            get;
            set;
        }
    }
}
