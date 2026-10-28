namespace LumeeraCare.Models
{
    public class Customer
    {
        public long Id { get; set; }

        // Personal name fields
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Contact / login fields
        public string Email { get; set; } = string.Empty;

        // Stored password hash (caller/service must hash plaintext before storing)
        public string? PasswordHash { get; set; }

        public string? Phone { get; set; }

        // Active flag
        public bool IsActive { get; set; } = true;

        // Creation timestamp
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    }
}