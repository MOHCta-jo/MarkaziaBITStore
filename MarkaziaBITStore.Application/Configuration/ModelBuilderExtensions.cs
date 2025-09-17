using MarkaziaBITStore.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.Configuration
{
    public static class ModelBuilderExtensions
    {
        public static void AddGlobalQueryFilters(this ModelBuilder modelBuilder)
        {

            // Add global query filters for soft-deleted entities // if you want to retrive pass to service true for IgnoreQueryFilters parameter
            modelBuilder.Entity<BIT_CAT_Category>()
                .HasQueryFilter(b => b.BIT_CAT_Cancelled == false || b.BIT_CAT_Cancelled == null);


            modelBuilder.Entity<BIT_COL_Colors>()
                .HasQueryFilter(b => b.BIT_COL_Cancelled == false || b.BIT_COL_Cancelled == null); 
            
            modelBuilder.Entity<BIT_ITC_ItemsColor>()
                .HasQueryFilter(b => b.BIT_ITC_Cancelled == false || b.BIT_ITC_Cancelled == null); 
            
            
            modelBuilder.Entity<BIT_ITM_Items>()
                .HasQueryFilter(b => b.BIT_ITM_Cancelled == false || b.BIT_ITM_Cancelled == null);      
            
            modelBuilder.Entity<BIT_ICI_ItemsColorImages>()
                .HasQueryFilter(b => b.BIT_ICI_Cancelled == false || b.BIT_ICI_Cancelled == null);

        }
    }
}
