using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.DTOs.ResultDTOs
{
    public class GetItemColorImagesListResult
    {
        public int ImageID { get; set; }
        public int ItemColorID { get; set; }
        public int Sequence { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsDefault { get; set; }
        public int? Status { get; set; }
    }
}
