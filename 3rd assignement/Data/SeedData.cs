using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using _3rd_assignement.Models;

namespace _3rd_assignement.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // =========================
            // ROLES
            // =========================
            string[] roles = { "Admin", "Institution", "Student" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // =========================
            // ADMIN USER
            // =========================
            var adminEmail = "admin@test.com";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, "Admin123!");

                await userManager.AddToRoleAsync(admin, "Admin");
            }

            // =========================
            // STUDENT USER + STUDENT ENTITY
            // =========================
            var studentEmail = "student@test.com";

            if (await userManager.FindByEmailAsync(studentEmail) == null)
            {
                var studentUser = new IdentityUser
                {
                    UserName = studentEmail,
                    Email = studentEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(studentUser, "Student123!");

                await userManager.AddToRoleAsync(studentUser, "Student");

                // IMPORTANT : créer Student dans ta table
                var studentEntity = new Student
                {
                    UserId = studentUser.Id
                };

                context.Students.Add(studentEntity);
                await context.SaveChangesAsync();
            }

            // =========================
            // INSTITUTION USER
            // =========================
            var instEmail = "inst@test.com";

            if (await userManager.FindByEmailAsync(instEmail) == null)
            {
                var inst = new IdentityUser
                {
                    UserName = instEmail,
                    Email = instEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(inst, "Inst123!");

                await userManager.AddToRoleAsync(inst, "Institution");
            }
        }
    }
}