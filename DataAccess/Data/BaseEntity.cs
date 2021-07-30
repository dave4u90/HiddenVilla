using System;
using System.ComponentModel.DataAnnotations;
using Common;

namespace DataAccess.Data
{
    public class BaseEntity
    {
        [Required]
        public string CreatedBy { get; set; } = SD.Role_Admin;
        [Required]
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; } = SD.Role_Admin;
        public DateTime UpdatedDate { get; set; }
        public string DeletedBy { get; set; } = SD.Role_Admin;
        public DateTime DeletedDate { get; set; }
    }
}
