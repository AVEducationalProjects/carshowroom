using CarShowRoom.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowRoom
{
    public class DbInitializer
    {
        public static async Task Initialize(CarShowRoom.Db.CRMContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; 
            }

            await CreateDefaultUserAndRoleForApplication(userManager, roleManager);
        }

        private static async Task CreateDefaultUserAndRoleForApplication(UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            const string administratorRole = "admin";
            const string email = "admin@auto.com";

            await CreateDefaultAdministratorRole(rm, administratorRole);
            var user = await CreateDefaultUser(um, email);
            await SetPasswordForDefaultUser(um, email, user);
            await AddDefaultRoleToDefaultUser(um, email, administratorRole, user);
        }

        private static async Task CreateDefaultAdministratorRole(RoleManager<IdentityRole> rm, string administratorRole)
        {
            var ir = await rm.CreateAsync(new IdentityRole(administratorRole));
            if (!ir.Succeeded)
            { 
                var exception = new ApplicationException($"Default role `{administratorRole}` cannot be created");
                throw exception;
            }
        }

        private static async Task<ApplicationUser> CreateDefaultUser(UserManager<ApplicationUser> um, string email)
        {
            var user = new ApplicationUser { Email = email, UserName=email };

            var ir = await um.CreateAsync(user);
            if (!ir.Succeeded)
            {
                var exception = new ApplicationException($"Default user `{email}` cannot be created");
                throw exception;
            }

            var createdUser = await um.FindByEmailAsync(email);
            return createdUser;
        }

        private static async Task SetPasswordForDefaultUser(UserManager<ApplicationUser> um, string email, ApplicationUser user)
        {
            const string password = "Pa$$w0rd";
            var ir = await um.AddPasswordAsync(user, password);
            if (!ir.Succeeded)
            {
                var exception = new ApplicationException($"Password for the user `{email}` cannot be set");
                throw exception;
            }
        }

        private static async Task AddDefaultRoleToDefaultUser(UserManager<ApplicationUser> um, string email, string administratorRole, ApplicationUser user)
        {
            var ir = await um.AddToRoleAsync(user, administratorRole);
            if (!ir.Succeeded)
            {
                var exception = new ApplicationException($"The role `{administratorRole}` cannot be set for the user `{email}`");
                throw exception;
            }
        }

        private static string GetIdentiryErrorsInCommaSeperatedList(IdentityResult ir)
        {
            string errors = null;
            foreach (var identityError in ir.Errors)
            {
                errors += identityError.Description;
                errors += ", ";
            }
            return errors;
        }
    }
}