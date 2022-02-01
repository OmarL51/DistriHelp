using DistriHelp.API.Data;
using DistriHelp.API.Data.Entities;
using DistriHelp.API.Helpers;
using DistriHelp.API.Models;
using DistriHelp.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DistriHelp.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public UsersController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.Include(x => x.Area).ToListAsync());
        }

        public IActionResult Create()
        {
            UserViewModel model = new UserViewModel
            {
                Areas = _combosHelper.GetComboAreas()

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _converterHelper.ToUserAsync(model, true);
                user.UserType = UserType.User;
                await _userHelper.AddUserAsync(user, "1234567");
                await _userHelper.AddUserToRoleAsync(user, user.UserType.ToString());
            }
            model.Areas = _combosHelper.GetComboAreas();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(Guid.Parse(Id));
            if (user == null)
            {
                return NotFound();
            }
            UserViewModel model = _converterHelper.ToUserViewModel(user);
            return View(model);
        }


        // POST: Areas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {

            if (ModelState.IsValid)
            {

                User user = await _converterHelper.ToUserAsync(model, false);
                await _userHelper.UpdateUserAsync(user);
                return RedirectToAction(nameof(Index));

            }
            model.Areas = _combosHelper.GetComboAreas();
            return View(model);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(Guid.Parse(Id));
            if (user == null)
            {
                return NotFound();
            }
            UserViewModel model = _converterHelper.ToUserViewModel(user);
            await _userHelper.DeleteUserAsync(user);
            return RedirectToAction(nameof(Index));

        }
    }
}

