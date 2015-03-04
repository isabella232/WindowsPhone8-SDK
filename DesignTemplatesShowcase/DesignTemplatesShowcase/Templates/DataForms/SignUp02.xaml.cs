using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.DataForm;
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP.Templates.DataForms
{
    public partial class SignUp02 : UserControl
    {
        const string PasswordFieldName = "Password";
        const string PasswordConfirmationFieldName = "PasswordConfirm";
        const string PasswordFieldsDifferMessage = "The Password and Confirmation Password must match.";

        public SignUp02()
        {
            InitializeComponent();
            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        void OnUnloaded(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ShowApplicationBar();
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.HideApplicationBar();
        }

        void OnDataFormValidatingDataField(object sender, ValidatingDataFieldEventArgs e)
        {
            if (e.AssociatedDataField.PropertyKey != PasswordConfirmationFieldName)
            {
                return;
            }
            PasswordField originalPassword = this.DataForm.FindFieldByPropertyName(PasswordFieldName) as PasswordField;
            if (originalPassword != null)
            {
                if ((string)originalPassword.Value != (string)e.AssociatedDataField.Value)
                {
                    e.IsInputValid = false;
                    e.ValidationMessage = PasswordFieldsDifferMessage;
                }
            }
        }
    }
}
