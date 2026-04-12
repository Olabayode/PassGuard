using System.ComponentModel.DataAnnotations;

namespace PassGuard.Models.ViewModels
{
    public class VisitorFormViewModel
    {
        public int VisitorId { get; set; }

        [Required]
        public string FullName { get; set; } = "";

        [Required]
        public string Phone { get; set; } = "";
    }
}
