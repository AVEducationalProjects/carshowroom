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
            const string cassierRole = "cassier";

            const string adminEmail = "admin@auto.com";
            const string accountEmail = "account@auto.com";
            const string cassierEmail = "cassier@auto.com";

            await CreateRole(rm, administratorRole);
            await CreateRole(rm, cassierRole);

            var admin = await CreateUser(um, adminEmail);
            await SetDefaultPasswordForUser(um, adminEmail, admin);
            await AddRoleToUser(um, adminEmail, administratorRole, admin);

            var account = await CreateUser(um, accountEmail);
            await SetDefaultPasswordForUser(um, accountEmail, account);
            await AddRoleToUser(um, accountEmail, administratorRole, account);

            var cassier = await CreateUser(um, cassierEmail);
            await SetDefaultPasswordForUser(um, cassierEmail, cassier);
            await AddRoleToUser(um, cassierEmail, cassierRole, cassier);

        }

        private static async Task CreateRole(RoleManager<IdentityRole> rm, string administratorRole)
        {
            var ir = await rm.CreateAsync(new IdentityRole(administratorRole));
            if (!ir.Succeeded)
            { 
                var exception = new ApplicationException($"Default role `{administratorRole}` cannot be created");
                throw exception;
            }
        }

        private static async Task<ApplicationUser> CreateUser(UserManager<ApplicationUser> um, string email)
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

        private static async Task SetDefaultPasswordForUser(UserManager<ApplicationUser> um, string email, ApplicationUser user)
        {
            const string password = "Pa$$w0rd";
            var ir = await um.AddPasswordAsync(user, password);
            if (!ir.Succeeded)
            {
                var exception = new ApplicationException($"Password for the user `{email}` cannot be set");
                throw exception;
            }
        }

        private static async Task AddRoleToUser(UserManager<ApplicationUser> um, string email, string administratorRole, ApplicationUser user)
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