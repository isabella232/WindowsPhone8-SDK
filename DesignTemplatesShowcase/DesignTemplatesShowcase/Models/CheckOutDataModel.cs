using System;
using Telerik.DesignTemplates.WP.SampleData;
using Telerik.Windows.Controls.DataForm;

namespace Telerik.DesignTemplates.WP.Models
{
    public class CheckOutDataModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CardNumber { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string SecurityCode { get; set; }

        [GenericListEditor(typeof(CountriesInfoProvider))]
        public string Country { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }
    }
}
