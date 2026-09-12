using System.ComponentModel.DataAnnotations;

namespace LearnHub_Api.Entities
{
    [Owned]
    public class RefreshToken
    {
        [MaxLength(500)]
        public string Token { get; set; } = string.Empty;

        public DateTime ExpireOn { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? RevokedOn { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpireOn;
        public bool IsActive => RevokedOn is null && !IsExpired;
    }
}