using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace K_Burns_JewelleryStore.Models
{
    public class DatabaseInitialiser:DropCreateDatabaseAlways<JewelleryStoreDbContext>
    {
        protected override void Seed(JewelleryStoreDbContext context)
        {
            base.Seed(context);

            //if there are no records stored in the Users table
            if(!context.Users.Any())
            {
                //first we are going to create some roles and store them in the Roles table
                
                //to create and store roles we need a RoleManager
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                //if the Admin role doesn't exist
                if(!roleManager.RoleExists("Admin"))
                {
                    //then we create one
                    roleManager.Create(new IdentityRole("Admin"));
                }

                if (!roleManager.RoleExists("Staff"))
                {
                    roleManager.Create(new IdentityRole("Staff"));
                }

                if (!roleManager.RoleExists("Manager"))
                {
                    roleManager.Create(new IdentityRole("Manager"));
                }

                if (!roleManager.RoleExists("Customer"))
                {
                    roleManager.Create(new IdentityRole("Customer"));
                }
                //this is a role for customers that are suspended no longer allowed to trade or buy from us
                if (!roleManager.RoleExists("Suspended"))
                {
                    roleManager.Create(new IdentityRole("Suspended"));
                }

                context.SaveChanges();

                //*************************************Create Users**************************************************

                //to create users-customers or employees - we need a UserManager
                UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

                //Create an ADMIN
                //first check if the admin exists in the database
                if (userManager.FindByName("admin@jewellerystore.com") == null)
                {
                    //Super liberal password validation for password for seeds
                    userManager.PasswordValidator = new PasswordValidator
                    {
                        RequireDigit = false,
                        RequiredLength = 1,
                        RequireLowercase = false,
                        RequireNonLetterOrDigit = false,
                        RequireUppercase = false
                    };

                    //create an admin employee
                    var administrator = new Employee
                    {
                        UserName = "admin@jewellerystore.com",
                        Email = "admin@jewellerystore.com",
                        FirstName = "Adam",
                        LastName = "Adamson",
                        Street = "12 Greedy St",
                        City = "Glasgow",
                        PostCode = "G5 7GG",
                        RegisteredAt = DateTime.Now.AddYears(-5),
                        EmailConfirmed = true,
                        IsActive = true,
                        IsSuspended = false,
                        EmploymentStatus = EmploymentStatus.FullTime
                    };
                    //add admin to the Users table
                    userManager.Create(administrator, "admin123");
                    //assign it to the Admin role
                    userManager.AddToRoles(administrator.Id, "Admin");
                }

                //Add a few employees
                //first check if jeff already exists in the database
                if (userManager.FindByName("jeff@jewellerystore.com") == null)
                {
                    //if it doesn't then create him
                    var jeff = new Employee
                    {
                        UserName = "jeff@jewellerystore.com",
                        Email = "jeff@jewellerystore.com",
                        FirstName = "Jeff",
                        LastName = "Jefferson",
                        Street = "156 Gladys St",
                        City = "Glasgow",
                        PostCode = "G9 7GG",
                        RegisteredAt = DateTime.Now.AddYears(-2),
                        EmailConfirmed = true,
                        IsActive = true,
                        IsSuspended = false,
                        EmploymentStatus = EmploymentStatus.FullTime
                    };
                    //add jeff and the password to the Users table
                    userManager.Create(jeff, "manager");
                    //assign the manager role to jeff
                    userManager.AddToRoles(jeff.Id, "Manager");
                }

                if (userManager.FindByName("xander@jewellerystore.com") == null)
                {
                    var alex = new Employee
                    {
                        UserName = "xander@jewellerystore.com",
                        Email = "xander@jewellerystore.com",
                        FirstName = "Alex",
                        LastName = "Alexei",
                        Street = "6 Arlington St",
                        City = "Glasgow",
                        PostCode = "G3 7GG",
                        RegisteredAt = DateTime.Now.AddYears(-3),
                        EmailConfirmed = true,
                        IsActive = true,
                        IsSuspended = false,
                        EmploymentStatus = EmploymentStatus.PartTime
                    };
                    userManager.Create(alex, "staff2");
                    userManager.AddToRoles(alex.Id, "Staff");
                }

                //Add a few Customers
                if (userManager.FindByName("billy@gmail.com") == null)
                {
                    var customer = new Customer
                    {
                        UserName = "billy@gmail.com",
                        Email = "billy@gmail.com",
                        FirstName = "Billy",
                        LastName = "Crow",
                        Street = "123 Happy St",
                        City = "Glasgow",
                        PostCode = "G56 7DF",
                        RegisteredAt = DateTime.Now.AddMonths(-5),
                        EmailConfirmed = true,
                        IsActive = true,
                        IsSuspended = false,
                        CustomerType=CustomerType.Private
                    };
                    userManager.Create(customer, "customer1");
                    userManager.AddToRole(customer.Id, "Customer");
                }

                if (userManager.FindByName("bob@gmail.com") == null)
                {
                    var bob = new Customer
                    {
                        UserName = "bob@gmail.com",
                        Email = "bob@gmail.com",
                        FirstName = "Bob",
                        LastName = "Williams",
                        Street = "56 Grumpy St",
                        City = "Glasgow",
                        PostCode = "G56 7DF",
                        RegisteredAt = DateTime.Now.AddYears(-1),
                        EmailConfirmed = true,
                        IsActive = true,
                        IsSuspended = false,
                        CustomerType = CustomerType.Private
                    };
                    userManager.Create(bob, "customer2");
                    userManager.AddToRole(bob.Id, "Customer");
                }

                if (userManager.FindByName("steveb@gmail.com") == null)
                {
                    var steve = new Customer
                    {
                        UserName = "steveb@gmail.com",
                        Email = "steveb@gmail.com",
                        FirstName = "Steve",
                        LastName = "Fist",
                        Street = "1 Confused St",
                        City = "Edinburgh",
                        PostCode = "EH6 7DF",
                        RegisteredAt = DateTime.Now.AddMonths(-10),
                        EmailConfirmed = true,
                        IsActive = true,
                        IsSuspended = false,
                        CustomerType = CustomerType.Private
                    };
                    userManager.Create(steve, "customer3");
                    userManager.AddToRole(steve.Id, "Customer");
                }

                if (userManager.FindByName("gary@gmail.com") == null)
                {
                    var gary = new Customer
                    {
                        UserName = "gary@gmail.com",
                        Email = "gary@gmail.com",
                        FirstName = "Garry",
                        LastName = "Hugh",
                        Street = "3 Hungry St",
                        City = "Glasgow",
                        PostCode = "G7 7DF",
                        RegisteredAt = DateTime.Now,
                        EmailConfirmed = true,
                        IsActive = true,
                        IsSuspended = false,
                        CustomerType = CustomerType.Private
                    };
                    userManager.Create(gary, "customer4");
                    userManager.AddToRole(gary.Id, "Customer");
                }

                //a suspended customer
                if (userManager.FindByName("bill@gmail.com") == null)
                {
                    var bill = new Customer
                    {
                        UserName = "bill@gmail.com",
                        Email = "bill@gmail.com",
                        FirstName = "Bill",
                        LastName = "Black",
                        Street = "67 Hopeless St",
                        City = "Glasgow",
                        PostCode = "G9 7DF",
                        RegisteredAt = DateTime.Now.AddDays(-28),
                        EmailConfirmed = true,
                        IsActive = true,
                        IsSuspended = true,
                        CustomerType = CustomerType.Private
                    };
                    userManager.Create(bill, "suspended1");
                    userManager.AddToRole(bill.Id, "Suspended");
                }

                context.SaveChanges();

                //Create a few categories
                var rings = new Category { Name = "Rings" };
                var bracelets = new Category { Name = "Bracelets" };
                var necklesses = new Category { Name = "Necklesses" };
                var earrings = new Category { Name = "Earrings" };

                var bespoke = new Category { Name = "Bespoke" };
                var repairs = new Category { Name = "Repairs" };

                context.Categories.Add(rings);
                context.Categories.Add(bracelets);
                context.Categories.Add(necklesses);
                context.Categories.Add(earrings);
                context.Categories.Add(bespoke);
                context.Categories.Add(repairs);
                context.SaveChanges();

                //Create a few products

                context.Products.Add(new Product()
                {
                    ProductName = "Bracelet 1",
                    UnitPrice = 399,
                    UnitsInStock = 100,
                    StockUpdatedOn = DateTime.Now.AddMonths(-3),
                    ImageUrl = "/Images/Products/bracelet1.PNG",
                    Discontinued = false,
                    OnSale = true,
                    Category = bracelets
                });

                context.Products.Add(new Product()
                {
                    ProductName = "Bracelet 2",
                    UnitPrice = 299,
                    UnitsInStock = 0,
                    StockUpdatedOn = DateTime.Now.AddMonths(-1),
                    ImageUrl = "/Images/Products/bracelet2.PNG",
                    Discontinued = false,
                    OnSale = false,
                    Category = bracelets
                });

                context.Products.Add(new Product()
                {
                    ProductName = "Bracelet 3",
                    UnitPrice = 499,
                    UnitsInStock = 35,
                    StockUpdatedOn = DateTime.Now.AddMonths(-7),
                    ImageUrl = "/Images/Products/bracelet3.PNG",
                    Discontinued = false,
                    OnSale = false,
                    Category = bracelets
                });

                context.Products.Add(new Product()
                {
                    ProductName = "Earring 1",
                    UnitPrice = 199,
                    UnitsInStock = 135,
                    StockUpdatedOn = DateTime.Now.AddMonths(-6),
                    ImageUrl = "/Images/Products/earrings1.PNG",
                    Discontinued = false,
                    OnSale = true,
                    Category = earrings
                });

                context.Products.Add(new Product()
                {
                    ProductName = "Earring 2",
                    UnitPrice = 299,
                    UnitsInStock = 135,
                    StockUpdatedOn = DateTime.Now.AddMonths(-5),
                    ImageUrl = "/Images/Products/earrings2.PNG",
                    Discontinued = false,
                    OnSale = false,
                    Category = earrings
                });

                context.Products.Add(new Product()
                {
                    ProductName = "Earring 3",
                    UnitPrice = 299,
                    UnitsInStock = 15,
                    StockUpdatedOn = DateTime.Now.AddMonths(-5),
                    ImageUrl = "/Images/Products/earrings3.PNG",
                    Discontinued = true,
                    OnSale = false,
                    Category = bespoke
                });

                context.Products.Add(new Product()
                {
                    ProductName = "Earring 4",
                    UnitPrice = 199,
                    UnitsInStock = 17,
                    StockUpdatedOn = DateTime.Now.AddMonths(-2),
                    ImageUrl = "/Images/Products/earrings4.PNG",
                    Discontinued = false,
                    OnSale = false,
                    Category = earrings
                });

                context.Products.Add(new Product()
                {
                    ProductName = "Neckless 1",
                    UnitPrice = 199,
                    UnitsInStock = 170,
                    StockUpdatedOn = DateTime.Now.AddMonths(-2),
                    ImageUrl = "/Images/Products/neckless_1.PNG",
                    Discontinued = false,
                    OnSale = false,
                    Category = necklesses
                });

                context.Products.Add(new Product()
                {
                    ProductName = "Neckless 2",
                    UnitPrice = 399,
                    UnitsInStock = 10,
                    StockUpdatedOn = DateTime.Now.AddMonths(-2),
                    ImageUrl = "/Images/Products/Neckless2.PNG",
                    Discontinued = false,
                    OnSale = false,
                    Category = necklesses
                });

                context.Products.Add(new Product()
                {
                    ProductName = "Neckless 3",
                    UnitPrice = 299,
                    UnitsInStock = 6,
                    StockUpdatedOn = DateTime.Now.AddMonths(-10),
                    ImageUrl = "/Images/Products/neckless3.PNG",
                    Discontinued = true,
                    OnSale = false,
                    Category = repairs
                });

                context.Products.Add(new Product()
                {
                    ProductName = "Ring 1",
                    UnitPrice = 699,
                    UnitsInStock = 16,
                    StockUpdatedOn = DateTime.Now.AddMonths(-1),
                    ImageUrl = "/Images/Products/ring_1.PNG",
                    Discontinued = false,
                    OnSale = false,
                    Category = rings
                });

                context.Products.Add(new Product()
                {
                    ProductName = "Ring 2",
                    UnitPrice = 599,
                    UnitsInStock = 164,
                    StockUpdatedOn = DateTime.Now.AddMonths(-3),
                    ImageUrl = "/Images/Products/ring_2.PNG",
                    Discontinued = false,
                    OnSale = false,
                    Category = rings
                });

                context.Products.Add(new Product()
                {
                    ProductName = "Ring 3",
                    UnitPrice = 999,
                    UnitsInStock = 4,
                    StockUpdatedOn = DateTime.Now.AddMonths(-3),
                    ImageUrl = "/Images/Products/ring3.PNG",
                    Discontinued = true,
                    OnSale = false,
                    Category = bespoke
                });

                context.SaveChanges();

                //seed a few card types

                context.CardTypes.Add(new CardType()
                {
                    CardTypeName = "Mastercard"
                });

                context.CardTypes.Add(new CardType()
                {
                    CardTypeName = "Visa"
                });

                context.CardTypes.Add(new CardType()
                {
                    CardTypeName = "American Express"
                });

                context.CardTypes.Add(new CardType()
                {
                    CardTypeName = "Mastercard Debit"
                });

                context.CardTypes.Add(new CardType()
                {
                    CardTypeName = "Visa Debit"
                });

                context.SaveChanges();
            }//end of if any user



        }//end of Seed method
    }//end of class
}//end of namespace