using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoreMvcExamApp.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public string? Address { get; set; }

        public string? ContactNo { get; set; }

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }

}
