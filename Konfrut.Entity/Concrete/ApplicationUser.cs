using Microsoft.AspNetCore.Identity;

namespace Konfrut.Entity.Concrete
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? IdentifierNumber { get; set; } = string.Empty;
        public string? KGKRegistrationNumber { get; set; } = string.Empty;
        public string? SerialNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = DateTime.MinValue;
        public string? PhoneNumber { get; set; } = string.Empty;
    }
}
