

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestFul_API.Infrastructure.Context;
using RestFul_API.Infrastructure.Entities.Concrete;
using System;
using System.Linq;

namespace RestFul_API.Infrastructure.Seeding
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Categories.Any())
                {
                    return;
                }

                context.Categories.AddRange(
                    new Category
                    {
                        Name = "Boxing Equipment",
                        Description = "Boxing Equipment",
                    },
                    new Category
                    {
                        Name = "American Football Equipment",
                        Description = "American Football Equipment"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
