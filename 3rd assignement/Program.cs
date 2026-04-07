using _3rd_assignement.Data;
using _3rd_assignement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _3rd_assignement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // =======================
            // DATABASE
            // =======================
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // =======================
            // IDENTITY
            // =======================
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // =======================
            // PIPELINE
            // =======================
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            // =======================
            // SEED DATA (PROPRE)
            // =======================
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var context = services.GetRequiredService<ApplicationDbContext>();

                // ---------- ROLES ----------
                string[] roles = { "Admin", "Institution", "Student" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }

                // ---------- ADMIN ----------
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

                // ---------- INSTITUTION ----------
                var instEmail = "institution@test.com";

                if (await userManager.FindByEmailAsync(instEmail) == null)
                {
                    var inst = new IdentityUser
                    {
                        UserName = instEmail,
                        Email = instEmail,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(inst, "Test123!");
                    await userManager.AddToRoleAsync(inst, "Institution");
                }

                // ---------- COURSES ----------
                if (!context.Courses.Any())
                {
                    context.Courses.AddRange(
                        new Course { Title = "Math", Credits = 5 },
                        new Course { Title = "Physics", Credits = 4 }
                    );

                    await context.SaveChangesAsync();
                }

                // ---------- STUDENT USER ----------
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

                    // IMPORTANT
                    var studentEntity = new Student
                    {
                        UserId = studentUser.Id
                    };

                    context.Students.Add(studentEntity);
                    await context.SaveChangesAsync();
                }
            }

            app.Run();
        }
    }
}