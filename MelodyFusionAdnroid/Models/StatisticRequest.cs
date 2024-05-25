namespace MelodyFusionAdnroid.Models
{
    public class StatisticRequest
    {
        public DateTime DateFrom { get; set; } 
        public DateTime DateTo { get; set; } = DateTime.UtcNow;
    }
}
