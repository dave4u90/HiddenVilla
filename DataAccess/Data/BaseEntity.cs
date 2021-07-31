using System;
using System.ComponentModel.DataAnnotations;
using Common;

namespace DataAccess.Data
{
    public class BaseEntity
    {
#nullable enable
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
#nullable disable
    }
}
