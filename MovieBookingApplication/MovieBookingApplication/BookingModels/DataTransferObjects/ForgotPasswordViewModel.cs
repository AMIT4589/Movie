using System.ComponentModel.DataAnnotations;

namespace MovieBookingApplication.BookingModels.DataTransferObjects
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
