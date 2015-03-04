using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PictureGallery.Speech;

namespace PictureGallery.Speech
{
    /// <summary>
    /// Contains information about the speech input capabilities of an element.
    /// This property is set on input controls that are expected to receive voice input.
    /// </summary>
    public class SpeechRecognitionMetadata : DependencyObject
    {
        /// <summary>
        /// Identifies the <see cref="InputOrder"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty InputOrderProperty =
            DependencyProperty.Register("InputOrder", typeof(int), typeof(SpeechRecognitionMetadata), new PropertyMetadata(0));

        /// <summary>
        /// Identifies the <see cref="InputIdentificationToken"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty InputIdentificationTokenProperty =
            DependencyProperty.Register("InputIdentificationToken", typeof(object), typeof(SpeechRecognitionMetadata), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="PreviousInputElementToken"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PreviousInputElementTokenProperty =
            DependencyProperty.Register("PreviousInputElementToken", typeof(object), typeof(SpeechRecognitionMetadata), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="InputIdentificationHint"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty InputIdentificationHintProperty =
            DependencyProperty.Register("InputIdentificationHint", typeof(string), typeof(SpeechRecognitionMetadata), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="InputHandler"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty InputHandlerProperty =
            DependencyProperty.Register("InputHandler", typeof(ISpeechInputHandler), typeof(SpeechRecognitionMetadata), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="InvalidInputAnnouncement"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty InvalidInputAnnouncementProperty =
            DependencyProperty.Register("InvalidInputAnnouncement", typeof(string), typeof(SpeechRecognitionMetadata), new PropertyMetadata("Input invalid! Please, try again."));

        /// <summary>
        /// Identifies the <see cref="NotRecognizedInputAnnouncement"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty NotRecognizedInputAnnouncementProperty =
            DependencyProperty.Register("NotRecognizedInputAnnouncement", typeof(string), typeof(SpeechRecognitionMetadata), new PropertyMetadata("Input not recognized! Please, try again."));

        /// <summary>
        /// Identifies the <see cref="RecognizableStrings"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RecognizableStringsProperty =
            DependencyProperty.Register("RecognizableStrings", typeof(List<RecognizableString>), typeof(SpeechRecognitionMetadata), new PropertyMetadata(null));

        /// <summary>
        /// Fired before the speech input is sent to the target element.
        /// </summary>
        public event EventHandler<BeforeInputEventArgs> BeforeInput;

        /// <summary>
        /// Fired after the speech input is sent to the target element.
        /// </summary>

        public SpeechRecognitionMetadata()
        {
            this.RecognizableStrings = new List<RecognizableString>();
        }

        /// <summary>
        /// Fired after the speech input procedure has been delegated to the focused element.
        /// </summary>
        public event EventHandler<AfterInputEventArgs> AfterInput;

        /// <summary>
        /// Gets or sets a list of strings representing the recognizable strings for the input target
        /// to which this <see cref="SpeechRecognitionMetadata"/> is assigned.
        /// Defining recognizable strings makes speech recognition faster.
        /// </summary>
        public List<RecognizableString> RecognizableStrings
        {
            get
            {
                return this.GetValue(RecognizableStringsProperty) as List<RecognizableString>;
            }
            set
            {
                this.SetValue(RecognizableStringsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets an implementation of the <see cref="ISpeechInputHandler"/> interface
        /// that can be used to convert speech inputs to suitable inputs for the target element.
        /// </summary>
        public ISpeechInputHandler InputHandler
        {
            get
            {
                return this.GetValue(InputHandlerProperty) as ISpeechInputHandler;
            }
            set
            {
                this.SetValue(InputHandlerProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the speech input order for the element to which this
        /// <see cref="SpeechRecognitionMetadata"/> instance is assigned.
        /// </summary>
        public int InputOrder
        {
            get
            {
                return (int)this.GetValue(InputOrderProperty);
            }
            set
            {
                this.SetValue(InputOrderProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets an object that is used as an identification token for the
        /// object this <see cref="SpeechRecognitionMetadata"/> instance is set to.
        /// </summary>
        public object InputIdentificationToken
        {
            get
            {
                return this.GetValue(InputIdentificationTokenProperty);
            }
            set
            {
                this.SetValue(InputIdentificationTokenProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the input identification token of the element in the page
        /// which was activated before the element associated with the current <see cref="SpeechRecognitionMetadata"/> instance.
        /// </summary>
        public object PreviousInputElementToken
        {
            get
            {
                return this.GetValue(PreviousInputElementTokenProperty);
            }
            set
            {
                this.SetValue(PreviousInputElementTokenProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets an object that is used as an identification hint for the
        /// object this <see cref="SpeechRecognitionMetadata"/> instance is set to. The identification
        /// hint is spoken out by the SpeechSynthesizer to notify the user which
        /// field is focused for input.
        /// </summary>
        public string InputIdentificationHint
        {
            get
            {
                return (string)this.GetValue(InputIdentificationHintProperty);
            }
            set
            {
                this.SetValue(InputIdentificationHintProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the announcement that is made when the speech input has not been validated.
        /// </summary>
        public string InvalidInputAnnouncement
        {
            get
            {
                return (string)this.GetValue(InvalidInputAnnouncementProperty);
            }
            set
            {
                this.SetValue(InvalidInputAnnouncementProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the announcement that is made when the speech input has not been recognized.
        /// </summary>
        public string NotRecognizedInputAnnouncement
        {
            get
            {
                return (string)this.GetValue(NotRecognizedInputAnnouncementProperty);
            }
            set
            {
                this.SetValue(NotRecognizedInputAnnouncementProperty, value);
            }
        }

        internal bool PerformInput(FrameworkElement target, string input)
        {
            bool result = true;
            this.OnBeforeInput(target, input);

            ISpeechInputHandler converter = this.InputHandler;

            if (converter != null)
            {
                if (converter.CanHandleInput(input))
                {
                    converter.HandleInput(target, input);
                }
                else
                {
                    converter.NotifyInputError(target);
                    result = false;
                }
            }
            else
            {
                if (target is TextBox)
                {
                    (target as TextBox).Text = input;
                }
            }

            this.OnAfterInput(target, input, result);

            return result;
        }

        private void OnBeforeInput(FrameworkElement target, string input)
        {
            EventHandler<BeforeInputEventArgs> handler = this.BeforeInput;
            if (handler != null)
            {
                handler(this, new BeforeInputEventArgs(){Input = input, Target = target});
            }
        }

        private void OnAfterInput(FrameworkElement target, string input, bool isSuccess)
        {
            EventHandler<AfterInputEventArgs> handler = this.AfterInput;
            if (handler != null)
            {
                handler(this, new AfterInputEventArgs() { Input = input, Target = target, IsSuccess = isSuccess });
            }
        }
    }
}
