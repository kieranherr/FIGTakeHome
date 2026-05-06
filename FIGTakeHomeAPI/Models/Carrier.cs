using System.ComponentModel.DataAnnotations;

namespace FIGTakeHomeAPI.Models
{
    public class Carrier
    {
        [Key]
        int Id { get; set; } 
        string CarrierName { get; set; }
        
    }
}
