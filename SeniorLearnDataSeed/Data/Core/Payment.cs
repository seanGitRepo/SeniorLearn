using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace SeniorLearnDataSeed.Data.Core
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
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
