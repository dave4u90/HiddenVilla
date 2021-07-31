using System;
using System.ComponentModel.DataAnnotations;
using Common;

namespace DataAccess.Data
{
    public class BaseEntity
    {
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        
#nullable enable
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
#nullable disable
    }
}
