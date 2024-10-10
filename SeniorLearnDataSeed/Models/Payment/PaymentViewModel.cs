using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeniorLearnDataSeed.Data.Core;

namespace SeniorLearnDataSeed.Models
{
    public class PaymentViewModel
    {
        public PaymentType SelectedPaymentType { get; set; } // Property for selected payment type
        public List<SelectListItem> PaymentTypes { get; set; } // List of payment types as select list items

        // Include other properties as needed
        [Required]
        public double AmountPaid { get; set; }
    }
}