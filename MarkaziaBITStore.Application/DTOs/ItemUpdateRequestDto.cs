using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.DTOs
{
    public class ItemUpdateRequestDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public int Points { get; set; }
        public int Status { get; set; }
    }
}
