using System;
using Telerik.DesignTemplates.WP.SampleData;
using Telerik.Windows.Controls.DataForm;

namespace Telerik.DesignTemplates.WP.Models
{
    public class MultiPurposeFormDataModel
    {
        [GenericListEditor(typeof(OptionsInfoProvider))]
        public string Option1 { get; set; }

        [GenericListEditor(typeof(OptionsInfoProvider))]
        public string Option2 { get; set; }

        public DateTime? Date1 { get; set; }

        public DateTime? Date2 { get; set; }

        public int NumberField { get; set; }

        [GenericListEditor(typeof(OptionsInfoProvider))]
        public string Option3 { get; set; }
    }
}
