using System.ComponentModel.DataAnnotations;

namespace CoreMvcExamApp.Models
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        [Required]
        public string CustomerName { get; set; }

        public string? Address { get; set; }

        public string? ContactNo { get; set; }

        public List<InvoiceItemViewModel> Items { get; set; } = new List<InvoiceItemViewModel>();
    }


}
