namespace MelodyFusionAdnroid.Models.Request
{
    public class ChangeRoleRequest
    {
        public string UserId { get; set; } = string.Empty;
        public int Role { get; set; }
    }
}
