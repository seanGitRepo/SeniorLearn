using SeniorLearnDataSeed.Data.Core;
using System.ComponentModel.DataAnnotations;

namespace SeniorLearnDataSeed.Models.Payment
{
    public class PaymentDetails
    {

        [Key]
        public int PaymentId { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public double AmountPaid { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? User { get; set; }
        public DateTime userRegistrationDate { get; set; }

    }    

    public enum PaymentType
    {
        Cash,
        EFT,
        CreditCard,
        Cheque,

    }

    public enum PaymentStatus
    {
        ChequeCleared,
        Pending,
        Declined,
        Success,
    }



    
}
