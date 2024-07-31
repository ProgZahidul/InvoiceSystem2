using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMvcExamApp.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }

}
