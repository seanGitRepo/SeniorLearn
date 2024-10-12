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
            PaymentStatus = p.PaymentStatus;
            AmountPaid = p.AmountPaid;
            ApplicationUserId = p.ApplicationUserId;
            userRegistrationDate = p.userRegistrationDate;
        }

        public PaymentRepository()
        {
            PaymentTypes = new List<SelectListItem>();
        }

        [Key]
        public int PaymentId { get; set; }
        public PaymentType SelectedPaymentType { get; set; } // Property for selected payment type
        public List<SelectListItem> PaymentTypes { get; set; } // List of payment types as select list items
        public PaymentStatus PaymentStatus { get; set; }
        public double AmountPaid { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? User { get; set; }
        public DateTime userRegistrationDate { get; set; }

    }    

   



    
}
