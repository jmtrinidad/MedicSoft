namespace MedicSoft.Web.Data
{
    using Entities;
    using MedicSoft.Web.Helpers;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.userHelper.CheckRoleAsync("SA");
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("User");
            await this.userHelper.CheckRoleAsync("Technical");

            // Add Country and cities
            //if (!this.context.Countries.Any())
            //{
            //    var cities = new List<City>();
            //    cities.Add(new City { Name = "Santo Domingo" });
            //    cities.Add(new City { Name = "San Francisco de Macoris" });
            //    cities.Add(new City { Name = "Santiago Rodriguez" });

            //    this.context.Countries.Add(new Country
            //    {
            //        Cities = cities,
            //        Name = "Republica Dominicana"
            //    });

            //    await this.context.SaveChangesAsync();
            //}
            // Add user
            var user = await this.userHelper.GetUserByEmailAsync("jm.trinidad.99@hotmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Jose Miguel",
                    LastName = "Trinidad ",
                    Email = "jm.trinidad.99@hotmail.com",
                    UserName = "jm.trinidad.99@hotmail.com",
                    PhoneNumber = "829-436-0332",
                    Address = "Papi Olivier #165 APT#4",
                    IsEnabled = true,
                     
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
                await this.userHelper.AddUserToRoleAsync(user, "SA");
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "SA");
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "SA");
            }
            // Add products
            //if (!this.context.Products.Any())
            //{
            //    this.AddProduct("iPhone X", user);
            //    this.AddProduct("Magic Mouse", user);
            //    this.AddProduct("iWatch Series 4", user);
            //    await this.context.SaveChangesAsync();
            //}
        }

        //private void AddProduct(string name, User user)
        //{
        //    this.context.Products.Add(new Product
        //    {
        //        Name = name,
        //        Price = this.random.Next(1000),
        //        IsAvailabe = true,
        //        Stock = this.random.Next(100),
        //        User = user
        //    });
        //}
    }
}
