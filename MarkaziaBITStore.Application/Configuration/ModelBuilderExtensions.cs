using MarkaziaBITStore.Application.Entites;
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
            modelBuilder.Entity<BitCatCategory>()
                .HasQueryFilter(b => b.BitCatCancelled == false || b.BitCatCancelled == null);


            modelBuilder.Entity<BitColColor>()
                .HasQueryFilter(b => b.BitColCancelled == false || b.BitColCancelled == null); 
            
            modelBuilder.Entity<BitItcItemsColor>()
                .HasQueryFilter(b => b.BitItcCancelled == false || b.BitItcCancelled == null); 
            
            
            modelBuilder.Entity<BitItmItem>()
                .HasQueryFilter(b => b.BitItmCancelled == false || b.BitItmCancelled == null);      
            
            modelBuilder.Entity<BitIciItemsColorImage>()
                .HasQueryFilter(b => b.BitIciCancelled == false || b.BitIciCancelled == null);

        }
    }
}
