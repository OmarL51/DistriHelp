using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DistriHelp.API.Data;
using DistriHelp.API.Data.Entities;
using DistriHelp.API.Helpers;
using DistriHelp.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace DistriHelp.API.Controllers
{

    public class RequestsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public RequestsController(DataContext context, ICombosHelper combosHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        // GET: Requests
        public async Task<IActionResult> Index()
        {
            return View(await _context.Requests.Include(x => x.Category).Include(x => x.RequesType).Include(x => x.Status).Include(x => x.User).ToListAsync());
        }



        // GET: Request/Create
        public IActionResult Create()
        {
            RequestViewModel model = new RequestViewModel
            {
                RequestTypes = _combosHelper.GetComboRequestTypes(),
                Categories = _combosHelper.GetComboCategories(),
                Statuses = _combosHelper.GetComboStatuses(),
                Users = _combosHelper.GetComboUsersN()
            };
            return View(model);
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                Request request = await _converterHelper.ToRequestAsync(model, true);

                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            model.RequestTypes = _combosHelper.GetComboRequestTypes();
            model.Categories = _combosHelper.GetComboCategories();
            model.Statuses = _combosHelper.GetComboStatuses();
            return RedirectToAction(nameof(Index));
        }

        // GET: Areas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Request request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            RequestViewModel model = _converterHelper.ToRequestViewModel(request);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                Request request = await _converterHelper.ToRequestAsync(model, true);

                _context.Update(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            model.RequestTypes = _combosHelper.GetComboRequestTypes();
            model.Categories = _combosHelper.GetComboCategories();
            model.Statuses = _combosHelper.GetComboStatuses();
            return RedirectToAction(nameof(Index));
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Request request = await _context.Requests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }
            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> AddVehicle(string id)
        {
            //if (string.IsNullOrEmpty(id))
            //{
            //    return NotFound();
            //}

            //User user = await _context.Users
            //    .Include(x => x.Vehicles)
            //    .FirstOrDefaultAsync(x => x.Id == id);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            //VehicleViewModel model = new VehicleViewModel
            //{
            //    Brands = _combosHelper.GetComboBrands(),
            //    UserId = user.Id,
            //    VehicleTypes = _combosHelper.GetComboVehicleTypes()
            //};

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRequest(RequestViewModel requestViewModel)
        {
            //Request request = await _context.Requests
            //    .Include(x => x.RequesType).Include(x => x.Category).Include(x => x.Status).Include(x => x.User)
            //    .FirstOrDefaultAsync(x => x.Id == requestViewModel.Id);
            //if (request == null)
            //{
            //    return NotFound();
            //}



            //try
            //{
            //    request.Vehicles.Add(vehicle);
            //    _context.Users.Update(user);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Details), new { id = user.Id });
            //}
            //catch (DbUpdateException dbUpdateException)
            //{
            //    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
            //    {
            //        ModelState.AddModelError(string.Empty, "Ya existe un vehículo con esa placa.");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    ModelState.AddModelError(string.Empty, exception.Message);
            //}

            //vehicleViewModel.Brands = _combosHelper.GetComboBrands();
            //vehicleViewModel.VehicleTypes = _combosHelper.GetComboVehicleTypes();
            return View();
        }
    }
}
