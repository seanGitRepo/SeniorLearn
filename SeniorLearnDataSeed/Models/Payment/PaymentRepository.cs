using Microsoft.AspNetCore.Mvc.Rendering;
using SeniorLearnDataSeed.Data.Core;
using System.ComponentModel.DataAnnotations;

namespace SeniorLearnDataSeed.Models.Payment
{
    public class PaymentRepository
    {

        public PaymentRepository(Data.Core.Payment p)
        {
            PaymentId = p.PaymentId;
            SelectedPaymentType = p.PaymentType;
            SelectedPaymentStatus = p.PaymentStatus;
            AmountPaid = p.AmountPaid;
            ApplicationUserId = p.ApplicationUserId;
            CardNumber = p.CardNumber;
            CardExpiry = p.CardExpiry;
            CVV = p.CVV;
            userRegistrationDate = p.userRegistrationDate;
        }

        public PaymentRepository()
        {
            PaymentTypes = new List<SelectListItem>();
        }

        [Key]
        public int PaymentId { get; set; }
        public List<SelectListItem> PaymentStatuses { get; set; }
        public PaymentType SelectedPaymentType { get; set; } // Property for selected payment type
        public List<SelectListItem> PaymentTypes { get; set; } // List of payment types as select list items
        public PaymentStatus SelectedPaymentStatus { get; set; }
        public double AmountPaid { get; set; }
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be 16 digits")]
        public double? CardNumber { get; set; }
        [RegularExpression(@"^(0[1-9]|1[0-2])\/[0-9]{2}$", ErrorMessage = "Expiry must be in the format MM/YY")]
        public string? CardExpiry { get; set; }
        [Required(ErrorMessage = "CVV is required.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVV must be 3 digits")]
        public int? CVV { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? User { get; set; }
        public DateTime userRegistrationDate { get; set; }

    }    

   



    
}
