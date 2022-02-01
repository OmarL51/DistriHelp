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
        public async Task<IActionResult> Create()
        {
            Request request = await _context.Requests.Include(x => x.RequesType).Include(x => x.Category).Include(x => x.Status).FirstOrDefaultAsync();
            if (request == null)
            {
                return NotFound();
            }


            RequestViewModel model = new RequestViewModel
            {
                RequestTypes = _combosHelper.GetComboRequestTypes(),
                Categories = _combosHelper.GetComboCategories(),
                Statuses = _combosHelper.GetComboStatuses(),
                Users = _combosHelper.GetComboUsers()

            };
           
            return View(model);

        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestViewModel requestViewModel)
        {
            if (ModelState.IsValid)
            {
                Request request = await _converterHelper.ToRequestAsync(requestViewModel, true);
            
            }
            requestViewModel.RequestTypes = _combosHelper.GetComboRequestTypes();
            requestViewModel.Categories = _combosHelper.GetComboCategories();
            requestViewModel.Statuses = _combosHelper.GetComboStatuses();
            requestViewModel.Users = _combosHelper.GetComboUsers();
            return RedirectToAction(nameof(Index));
        }

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Request request = await _context.Requests
                .Include(x => x.RequesType)
                .Include(x => x.Category)
                .Include(x => x.Status).Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            RequestViewModel model = _converterHelper.ToRequestViewModel(request);
            return View(model);
        }

        // POST: Request/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RequestViewModel requestViewModel)
        {

            if (id != requestViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Request request = await _converterHelper.ToRequestAsync(requestViewModel, false);
                    _context.Requests.Update(request);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe esta solicitud");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            requestViewModel.RequestTypes = _combosHelper.GetComboRequestTypes();
            requestViewModel.Categories = _combosHelper.GetComboCategories();
            requestViewModel.Statuses = _combosHelper.GetComboStatuses();
            requestViewModel.Users = _combosHelper.GetComboUsersN();
            return View(requestViewModel);
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
            

            return View();
        }

       
    }
}
