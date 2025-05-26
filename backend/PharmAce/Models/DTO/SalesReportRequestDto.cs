using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmAce.Models.DTO
{
    public class SalesReportRequestDto
    {
        public Guid? SupplierId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}