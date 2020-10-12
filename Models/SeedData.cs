using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

namespace DigiSign.Models {
    public static class SeedData {
        public static void EnsurePopulated(IApplicationBuilder app) {
            docsdevEntities context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<docsdevEntities>();
            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();
            }
            if (!context.signer_employee.Any()) {
                context.signer_employee.AddRange(
                    new signer_employee {
                        employee_id = "1001",
                        employee_email = "septian@gmail.com",
                        employee_name = "septian",
                        department_id = "1",
                        lastnotification = DateTime.Now
                    }
                );
                context.SaveChanges();
            }
        }
    }
}