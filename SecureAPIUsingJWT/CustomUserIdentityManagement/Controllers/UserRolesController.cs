using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomUserIdentityManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomUserIdentityManagement.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var userRoleViewModel = new UserRolesViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = await GetUserRoles(user)
                };
                userRolesViewModel.Add(userRoleViewModel);
            }

            return View(userRolesViewModel);
        }

        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        // Manager or Edit user roles
        public async Task<IActionResult> Manage(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return View("NotFound");
            }

            var model = new ManageUserRoleViewModel();
            model.manageUserRoleModels = new List<ManageUserRoleModel>();
            model.UserId = userId;
            model.UserName = user.UserName;

            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRoleModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.manageUserRoleModels.Add(userRolesViewModel);
                
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(ManageUserRoleViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.UserId);

            if(user == null)
            {
                return View();
            }

            // Find all roles from specific user
            var roles = await _userManager.GetRolesAsync(user);
            // Remove all roles from user 
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(viewModel);
            }

            // add selected roles for specific user.
            result = await _userManager.AddToRolesAsync(user, viewModel.manageUserRoleModels
                .Where(x => x.Selected)
                .Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(viewModel);
            }

            return RedirectToAction("Index");

        }
    }
}
