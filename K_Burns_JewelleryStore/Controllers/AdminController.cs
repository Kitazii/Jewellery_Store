using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using K_Burns_JewelleryStore.Models;
using K_Burns_JewelleryStore.Models.ViewModels;

namespace K_Burns_JewelleryStore.Controllers
{
    //controller inherits from AccountController so I can borrow the login/registration methods
    [Authorize(Roles = "Admin")]//this controller can be accessed only by amind role
    public class AdminController : AccountController
    {
        //here is an instance of JewelleryDbContext
        private JewelleryStoreDbContext db = new JewelleryStoreDbContext();

        public AdminController() : base()
        {

        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : base(userManager, signInManager)
        {

        }

        // GET: Admin
        public ActionResult Index()
        {
            //get all the users and order them by the registration date
            var users = db.Users.OrderBy(u => u.RegisteredAt).ToList();

            //send the list of users to the Index view
            return View(users);
        }

        //this ActionResult will proccess the search form on the index page
        [HttpPost]
        public ActionResult Index(string SearchString)
        {
            //get all the users who have been searched for and order them by the registration date
            //Doesn't need to be a list as it will only ever be ONE user returned
            var users = db.Users.Where(u => u.UserName.Equals(SearchString.Trim())).OrderBy(u => u.RegisteredAt);

            //if the search entered is empty
            //Redirect to the admin index page
            if(SearchString.Equals(""))
            {
                return RedirectToAction("Index");
            }
            //send the entered user to the Index view
            return View(users);
        }

        //***********************************************************************************

        //2: CREATE A NEW EMPLOYEE
        //***********************************************************************************

        [HttpGet]
        public ActionResult CreateEmployee()
        {
            CreateEmployeeViewModel employee = new CreateEmployeeViewModel();

            //get all the role from the database and store them as selectedListitem list so roles can be displayed in a dropdownlist
            var roles = db.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }).ToList();

            //assign the role to the employee roles property
            employee.Roles = roles;

            //send the employee model to the view
            return View(employee);
        }

        [HttpPost]
        public ActionResult CreateEmployee(CreateEmployeeViewModel model)
        {
            //if model is not null
            if (ModelState.IsValid)
            {
                //build the employee
                Employee newEmployee = new Employee
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true,
                    Street = model.Street,
                    City = model.City,
                    PostCode = model.PostCode,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmploymentStatus = model.EmployementStatus,
                    IsActive = true,
                    IsSuspended = false,
                    RegisteredAt = DateTime.Now
                };

                //create user, and store in the database and pass the password to be hashed
                var result = UserManager.Create(newEmployee, model.Password);
                //if user was stored in the database successfully
                if (result.Succeeded)
                {
                    //then add user to the role selected
                    UserManager.AddToRole(newEmployee.Id, model.Role);

                    return RedirectToAction("Index", "Admin");
                }
            }
            //something is wrong so go back to the create employee view
            return View(model);
        }

        //***********************************************************************************

        //***********************************************************************************
        // ADMIN CAN EDIT STAFF AND CUSTOMERS
        //***********************************************************************************

        //***********************************************************************************
        // EDIT STAFF
        //***********************************************************************************
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditStaff(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //find employee in the database by id
            Employee staff = db.Users.Find(id) as Employee;

            if (staff == null)
            {
                return HttpNotFound();
            }

            //send employees details to the view
            return View(new EditEmployeeViewModel
            {
                Street = staff.Street,
                City = staff.City,
                PostCode = staff.PostCode,
                PhoneNumber = staff.PhoneNumber,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                EmployementStatus = staff.EmploymentStatus,
                IsSuspended = staff.IsSuspended
            });
        }
        //POST: Users/EditStaff/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditStaff(string id,
            [Bind(Include = "FirstName,LastName,Street,City,PostCode,PhoneNumber,EmployementStatus,IsSuspended")] EditEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee staff = (Employee)await UserManager.FindByIdAsync(id);//find user by id and cast it as an employee

                UpdateModel(staff);//update the new staff details by using the model

                IdentityResult result = await UserManager.UpdateAsync(staff);//update the new staff details in the database

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }

            }

            return View(model);
        }

        //***********************************************************************************
        // EDIT CUSTOMERS
        //***********************************************************************************
        [HttpGet]
        public ActionResult EditCustomer(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Users.Find(id) as Customer; //find user by id and return it as a customer

            if (customer == null)
            {
                return HttpNotFound();
            }

            //send customer's details to the view
            return View(new EditCustomerViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Street = customer.Street,
                City = customer.City,
                PostCode = customer.PostCode,
                PhoneNumber = customer.PhoneNumber,
                CustomerType = customer.CustomerType,
                IsSuspended = customer.IsSuspended
            });
        }

        //POST: Users/EditMember/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCustomer(string id,
            [Bind(Include = "FirstName,LastName,Street,City,PostCode,PhoneNumber,EmployementStatus,IsSuspended")] EditEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                //get customer from database by id
                Customer customer = (Customer)await UserManager.FindByIdAsync(id);

                UpdateModel(customer);//update customer details using the values from the model

                IdentityResult result = await UserManager.UpdateAsync(customer);//update customer details in the database

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
            }

            return View(model);
        }

        //GET: Users/Details/5
        public ActionResult Details (string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (user is Employee)
            {
                return View("DetailsStaff", (Employee)user);
            }

            if (user is Customer)
            {
                return View("DetailsCustomer", (Customer)user);
            }

            return HttpNotFound();
        }

        //***********************************************************************************
        // CREATE NEW ROLE
        //***********************************************************************************
        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                //get the roleManager
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                //making sure that there are no duplicate roles stored in the database
                if(!roleManager.RoleExists(model.RoleName))
                {
                    //create and save the new role in the database
                    roleManager.Create(new IdentityRole(model.RoleName));

                    return RedirectToAction("Index", "Admin");
                }
            }

            return View(model);
        }

        //***********************************************************************************
        // CHANGE USER'S ROLE
        //***********************************************************************************
        [HttpGet]
        public async Task<ActionResult> ChangeRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Can't change your own role.
            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("Index", "Admin");
            }

            //get user by id
            User user = await UserManager.FindByIdAsync(id);
            //get user's current role
            string oldRole = (await UserManager.GetRolesAsync(id)).Single(); //Only ever a single role.

            //get all the roles from the database and store them as a list of selectedlistitems
            var items = db.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name,
                Selected = r.Name == oldRole
            }).ToList();

            //build the changeroleviewmodel object including the list of roles
            //and send it to the view displaying the roles in a dropdownlist with the user's current role displayed as selected
            return View(new ChangeRoleViewModel
            {
                UserName = user.UserName,
                Roles = items,
                OldRole = oldRole
            });
        }

        //POST: Users/ChangeRole/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ChangeRole")]
        public async Task<ActionResult> ChangeRoleConfirmed(string id, [Bind(Include = "Role")] ChangeRoleViewModel model)
        {
            // Can't change your own role.
            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("Index", "Admin");
            }

            if (ModelState.IsValid)
            {
                User user = await UserManager.FindByIdAsync(id); //get user by id

                //get user's current role
                string oldRole = (await UserManager.GetRolesAsync(id)).Single(); //Only ever a single role.

                //if current role is the same with selected role then there is no point to update the database
                if (oldRole == model.Role)
                {
                    return RedirectToAction("Index", "Admin");
                }

                //Remove user from the old role first.
                await UserManager.RemoveFromRoleAsync(id, oldRole);
                //now we are adding the user to the new role
                await UserManager.AddToRoleAsync(id, model.Role);

                //if the user was suspended
                if (model.Role == "Suspended")
                {
                    //then set isSuspended to true
                    user.IsSuspended = true;

                    //update user's details in the database
                    await UserManager.UpdateAsync(user);
                }

                return RedirectToAction("Index", "Admin");
            }

            return View(model);
        }
        //***********************************************************************************
        // DELETE USERS
        //***********************************************************************************


        //GET: Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //check we're not deleting our own account.
            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("Index", "Admin");
            }

            //find user by id in the database
            User user = await UserManager.FindByIdAsync(id);//get user by id

            //if user doesn't exist
            if (user == null)
            {
                return HttpNotFound();
            }

            //Delete user.
            await UserManager.DeleteAsync(user);

            return RedirectToAction("Index", "Admin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}