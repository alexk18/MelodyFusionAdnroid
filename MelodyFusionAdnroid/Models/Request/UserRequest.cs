using System.ComponentModel.DataAnnotations;

namespace MelodyFusionAdnroid.Models.Request
{
    public class UserRequest
    {
        [Required]
        [StringLength(18, MinimumLength = 2)]
        public string firstName { get; set; } = string.Empty;

        [Required]
        [StringLength(18, MinimumLength = 2)]
        public string lastName { get; set; } = string.Empty;

        [Required]
        [StringLength(18, MinimumLength = 8)]
        public string userName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string email { get; set; } = string.Empty;
    }
}
