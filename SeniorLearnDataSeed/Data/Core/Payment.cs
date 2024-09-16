using System.ComponentModel.DataAnnotations;

namespace SeniorLearnDataSeed.Data.Core
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public PaymentClassifications PaymentType { get; set; }
        public double AmountPaid { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }


    }

    public enum PaymentClassifications
    {
        Cash,
        Cheque,
        BankTransfer,
        DebitCard,
        EFT
    }
}
