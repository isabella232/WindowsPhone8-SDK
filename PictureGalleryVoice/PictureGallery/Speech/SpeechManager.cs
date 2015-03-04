using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using PictureGallery.Speech;
using Windows.Foundation;
using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;

namespace PictureGallery.Speech
{
    /// <summary>
    /// Manages the voice input and delegates it to the focused input element.
    /// Uses the <see cref="SpeechRecognitionMetadata"/> class and the <see cref="ISpeechInputHandler"/> interface.
    /// </summary>
    public class SpeechManager : DependencyObject
    {
        /// <summary>
        /// Identifies the SpeechMetadata dependency property.
        /// </summary>
        public static readonly DependencyProperty SpeechMetadataProperty =
            DependencyProperty.RegisterAttached("SpeechMetadata", typeof(SpeechRecognitionMetadata), typeof(SpeechManager), new PropertyMetadata(null));

        private static List<WeakReference<FrameworkElement>> inputElements;
        private static SpeechRecognizer speechRecognizer;
        private static SpeechSynthesizer speechSynthesizer;

        private SpeechManager()
        {
        }

        static SpeechManager()
        {
            inputElements = new List<WeakReference<FrameworkElement>>();
            InitRecognizer();
            InitSynthesizer();
        }

        /// <summary>
        /// Gets the <see cref="SpeechRecognitionMetadata"/> instance associated with the provided source object.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <returns></returns>
        public static SpeechRecognitionMetadata GetSpeechMetadata(DependencyObject source)
        {
            return (SpeechRecognitionMetadata)source.GetValue(SpeechMetadataProperty);
        }

        /// <summary>
        /// Sets a given <see cref="SpeechRecognitionMetadata"/> instance to a given target object.
        /// </summary>
        /// <param name="target">The target object on which to set the <see cref="SpeechRecognitionMetadata"/> instance.</param>
        /// <param name="value">The <see cref="SpeechRecognitionMetadata"/> instance to set.</param>
        public static void SetSpeechMetadata(DependencyObject target, SpeechRecognitionMetadata value)
        {
            target.SetValue(SpeechMetadataProperty, value);
        }
        
        /// <summary>
        /// Starts a listening procedure.
        /// </summary>
        public static void StartListening()
        {
            if (speechRecognizer == null)
            {
                InitRecognizer();
            }

            if (speechSynthesizer == null)
            {
                InitSynthesizer();
            }

            PhoneApplicationFrame currentFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            inputElements.Clear();
            FindInputElements(currentFrame);

            if (inputElements.Count > 0)
            {
                FrameworkElement firstInputElement = FindFirstInputElement();

                if (firstInputElement == null)
                {
                    return;
                }

                ListenForElement(firstInputElement, true);
            }
        }

        /// <summary>
        /// Stops the listening procedure.
        /// </summary>
        public static void Reset()
        {
            ////isListening = false;
            ////inputElements.Clear();
            ////if (speechRecognizer != null)
            ////{
            ////    speechRecognizer = null;
            ////}

            ////if (speechSynthesizer != null)
            ////{
            ////    speechSynthesizer = null;
            ////}
        }

        private static async Task AnnounceListenedElement(UIElement target)
        {
            SpeechRecognitionMetadata targetMetadata = GetSpeechMetadata(target);
            string announcement = targetMetadata.InputIdentificationHint;
            await speechSynthesizer.SpeakTextAsync(announcement);
        }

        private static void InitRecognizer()
        {
            speechRecognizer = new SpeechRecognizer();
            speechRecognizer.Settings.EndSilenceTimeout = TimeSpan.FromMilliseconds(150);
        }

        private static void InitSynthesizer()
        {
            speechSynthesizer = new SpeechSynthesizer();
        }

        private static async void ListenForElement(FrameworkElement target, bool announce)
        {
            if (target is Control)
            {
                (target as Control).Focus();
            }

            if (announce)
            {
                await AnnounceListenedElement(target);
            }

            try
            {
                SpeechRecognitionMetadata metadata = GetSpeechMetadata(target);
                speechRecognizer.Grammars.Clear();
                if (metadata.RecognizableStrings.Count > 0)
                {
                    List<string> recognizableStrings = new List<string>();

                    foreach(RecognizableString str in metadata.RecognizableStrings)
                    {
                        recognizableStrings.Add(str.Value);
                    }

                    speechRecognizer.Grammars.AddGrammarFromList("default", recognizableStrings);
                }

                SpeechRecognitionResult result = await speechRecognizer.RecognizeAsync();
                
                if (result.TextConfidence >= SpeechRecognitionConfidence.Medium)
                {
                    string inputResult = result.Text;

                    if (PerformInputForElement(target, inputResult))
                    {
                        FrameworkElement nextInputElement = FindNextInputElement(target, inputResult);
                        if (nextInputElement != null)
                        {
                            ListenForElement(nextInputElement, target != nextInputElement);
                            return;
                        }
                        else
                        {
                            Reset();
                        }
                    }
                    else
                    {
                        await speechSynthesizer.SpeakTextAsync(metadata.InvalidInputAnnouncement);
                        ListenForElement(target, false);
                        return;
                    }
                }
                else
                {
                    await speechSynthesizer.SpeakTextAsync(metadata.InvalidInputAnnouncement);
                    ListenForElement(target, false);
                    return;
                }
            }
            catch(Exception)
            {
                Reset();
            }
        }

        private static bool PerformInputForElement(FrameworkElement target, string input)
        {
            SpeechRecognitionMetadata elementMetadata = GetSpeechMetadata(target);

            return elementMetadata.PerformInput(target, input);
        }

        private static void FindInputElements(FrameworkElement element)
        {
            if (GetSpeechMetadata(element) != null)
            {
                inputElements.Add(new WeakReference<FrameworkElement>(element));
            }
            else if (element is Panel)
            {
                Panel panel = element as Panel;
                foreach (FrameworkElement panelElement in panel.Children)
                {
                    FindInputElements(panelElement);
                }
            }
            else if (element is ContentControl && (element as ContentControl).Content is UIElement)
            {
                FindInputElements((element as ContentControl).Content as FrameworkElement);
            }
            else if (element is ContentPresenter && (element as ContentPresenter).Content is FrameworkElement)
            {
                FindInputElements((element as ContentPresenter).Content as FrameworkElement);
            }
            else if (element is UserControl && (element as UserControl).Content is FrameworkElement)
            {
                FindInputElements((element as UserControl).Content as FrameworkElement);
            }
            else if (element is ItemsControl)
            {
                ItemsControl ic = element as ItemsControl;

                foreach (object item in ic.Items)
                {
                    if (item is FrameworkElement)
                    {
                        FindInputElements(item as FrameworkElement);
                    }
                }
            }
        }

        private static FrameworkElement FindFirstInputElement()
        {
            if (inputElements.Count == 0)
            {
                return null;
            }

            foreach (WeakReference<FrameworkElement> elRef in inputElements)
            {
                FrameworkElement target = null;
                if (elRef.TryGetTarget(out target))
                {
                    if (GetSpeechMetadata(target).PreviousInputElementToken == null)
                    {
                        return target;
                    }
                }
            }

            return null;
        }

        private static FrameworkElement FindNextInputElement(FrameworkElement element, string input)
        {
            RecognizableString stringMetadata = GetRecognizableStringMetadata(GetSpeechMetadata(element), input);
            object nextInputElementToken = stringMetadata.NextInputElementToken;

            foreach (WeakReference<FrameworkElement> elRef in inputElements)
            {
                FrameworkElement target = null;
                if (elRef.TryGetTarget(out target))
                {
                    if (object.Equals(GetSpeechMetadata(target).InputIdentificationToken, nextInputElementToken))
                    {
                        return target;
                    }
                }
            }

            return null;
        }

        private static RecognizableString GetRecognizableStringMetadata(SpeechRecognitionMetadata metadata, string input)
        {
            foreach (RecognizableString rString in metadata.RecognizableStrings)
            {
                if (string.Compare(rString.Value, input, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    return rString;
                }
            }

            return null;
        }
    }
}
