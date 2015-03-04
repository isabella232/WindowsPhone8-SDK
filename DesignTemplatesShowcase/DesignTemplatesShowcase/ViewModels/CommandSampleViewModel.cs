using System;
using System.Windows.Input;
using Telerik.DesignTemplates.WP.Commands;

namespace Telerik.DesignTemplates.WP.ViewModels
{
    public class CommandSampleViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSampleViewModel"/> class.
        /// </summary>
        public CommandSampleViewModel()
        {
            this.RateThisAppCommand = new RateThisAppCommand();
            this.SendAnEmailCommand = new SendAnEmailCommand();
        }

        /// <summary>
        /// Gets the rate this app command.
        /// </summary>
        public ICommand RateThisAppCommand { get; private set; }

        /// <summary>
        /// Gets the send an email command.
        /// </summary>
        public ICommand SendAnEmailCommand { get; private set; }
    }
}