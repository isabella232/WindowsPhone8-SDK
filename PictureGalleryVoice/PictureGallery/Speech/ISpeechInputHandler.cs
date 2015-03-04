using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PictureGallery.Speech
{
    /// <summary>
    /// An interface use by the <see cref="SpeechManager"/> to convert input strings
    /// to strings suitable for the targets of input.
    /// </summary>
    public interface ISpeechInputHandler
    {
        /// <summary>
        /// Evaluates whether the provided string input from a speech recognition procedure can be
        /// converted to a value suitable for the input target.
        /// </summary>
        /// <param name="input">The string input to convert.</param>
        /// <returns>True if the input is convertable; otherwise false.</returns>
        bool CanHandleInput(string input);

        /// <summary>
        /// Converts the provided input string to a suitable
        /// string that can be handled by the target of speech input.
        /// </summary>
        /// <param name="input">The input string to convert.</param>
        /// <param name="target">The target <see cref="FrameworkElement"/> that receives the input.</param>
        void HandleInput(FrameworkElement target, string input);

        /// <summary>
        /// This method will be called in case the <see cref="ISpeechInputHandler.CanHandleInput"/> returns
        /// false.
        /// </summary>
        /// <param name="target">The <see cref="FrameworkElement"/> that is target of input.</param>
        void NotifyInputError(FrameworkElement target);
        
    }
}
