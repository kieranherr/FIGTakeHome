using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIGTakeHomeAPI.Models
{
    public class Statement
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Advisor))]
        public int AdvisorId { get; set; }
        public Advisor? Advisor { get; set; }

        [MaxLength(50)]
        public string? WritingNumber { get; set; }

        [ForeignKey(nameof(Carrier))]
        public int CarrierId { get; set; }
        public Carrier? Carrier { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Premium { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CommissionAmount { get; set; }

        public DateTime StatementDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
