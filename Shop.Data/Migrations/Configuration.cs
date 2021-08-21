namespace Shop.Data.Migrations
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Shop.Data.ShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Shop.Data.ShopDbContext context)
        {
            CreateProductCategorySample(context);
            CreateUser(context);
            //  This method will be called after migrating to the latest version.
        }

        private void CreateUser(Shop.Data.ShopDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "nguyenhai",
                Email = "nguyenhai@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Nguyen Ngoc Minh Hai"
            };

            manager.Create(user, "123456@");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("nguyenhai@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateProductCategorySample(Shop.Data.ShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory() { Name="Gaming",Alias="gaming",Status=true },
                 new ProductCategory() { Name="Workstation",Alias="workstation",Status=true },
                  new ProductCategory() { Name="Ultrabook",Alias="ultrabook",Status=true },
                   new ProductCategory() { Name="Khác",Alias="khac",Status=true }
            };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }


    }
}