namespace MelodyFusion.DLL.Models
{
    public class MonthlySubscriptionCount
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int Count { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
