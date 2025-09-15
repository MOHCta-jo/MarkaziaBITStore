using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.DTOs.ResultDTOs
{
    public class GetItemsListResult
    {
        public int ItemID { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public int? Points { get; set; }
        public int? CategoryId { get; set; }

    }
}
