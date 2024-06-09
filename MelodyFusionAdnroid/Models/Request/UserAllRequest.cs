namespace MelodyFusionAdnroid.Models.Request
{
    public class UserAllRequest
    {
        public string? SearchRequest { get; set; } = string.Empty;
        public int PageFrom { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}