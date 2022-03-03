using DistriHelp.API.Data;
using DistriHelp.API.Data.Entities;
using DistriHelp.API.Helpers;
using DistriHelp.API.Models;
using DistriHelp.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Controllers
{

    public class RequestsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IUserHelper _userHelper;
        private readonly IEmailSender _emailSender;

        public RequestsController(DataContext context, ICombosHelper combosHelper, IConverterHelper converterHelper, IMailHelper mailHelper, IUserHelper userHelper, IEmailSender emailSender)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _mailHelper = mailHelper;
            _userHelper = userHelper;
            _emailSender = emailSender;
        }

        // GET: Requests
        public async Task<IActionResult> Index()
        {

            if (User.IsInRole("Admin"))
            {
                var mod = await _context.Requests.Include(x => x.Category).Include(x => x.RequesType).Include(x => x.Status).Include(x => x.User).ToListAsync();

                return View(mod);
            }
            else
            {
                if (User.Identity.Name == "distribucion@distrimedical.com.co" || User.Identity.Name == "auxiliarlogistica@distrimedical.com.co" || User.Identity.Name == "recepciontecnica2@distrimedical.com.co" || User.Identity.Name == "jaime.marulanda@distrimedical.com.co")
                {
                    var modi = await _context.Requests.Include(x => x.Category).Include(x => x.RequesType).Include(x => x.Status).Include(x => x.User).Where(x => x.Userr == "distribucion@distrimedical.com.co" || x.Userr == "auxiliarlogistica@distrimedical.com.co" || x.Userr == "recepciontecnica2@distrimedical.com.co" || x.Userr == "jaime.marulanda@distrimedical.com.co").ToListAsync();

                    return View(modi);
                }
                else if (User.Identity.Name == "alexandra.torres@distrimedical.com.co")
                {
                    var modii = await _context.Requests.Include(x => x.Category).Include(x => x.RequesType).Include(x => x.Status).Include(x => x.User).Where(x => x.Category.Id == 3 || x.Userr == "alexandra.torres@distrimedical.com.co").ToListAsync();

                    return View(modii);
                }
                else
                {
                    var modii = await _context.Requests.Include(x => x.Category).Include(x => x.RequesType).Include(x => x.Status).Include(x => x.User).Where(x => x.Userr == User.Identity.Name).ToListAsync();

                    return View(modii);
                }



            }

        }



        // GET: Request/Create
        public async Task<IActionResult> Create()
        {

            RequestViewModel model = new RequestViewModel
            {

                RequestTypes = _combosHelper.GetComboRequestTypes(),
                Categories = _combosHelper.GetComboCategories(),
                Statuses = _combosHelper.GetComboStatusesU(),


            };
            return View(model);

        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestViewModel requestViewModel,List<IFormFile> filess)
        {
            
            User user = await _userHelper.GetUserAsync(requestViewModel.Userr);
            var size = filess.Sum(x => x.Length);
            var filePaths = new List<string>();
            foreach (var formFile in filess)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), formFile.FileName);
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();

            if (ModelState.IsValid)
            {
                Request request = await _converterHelper.ToRequestAsync(requestViewModel, true);

                _context.Add(request);
                await _context.SaveChangesAsync();
            }

            if (User.IsInRole("Admin"))
            {
                requestViewModel.RequestTypes = _combosHelper.GetComboRequestTypes();
                requestViewModel.Categories = _combosHelper.GetComboCategories();
                requestViewModel.Statuses = _combosHelper.GetComboStatuses();
            }
            else
            {
                requestViewModel.RequestTypes = _combosHelper.GetComboRequestTypes();
                requestViewModel.Categories = _combosHelper.GetComboCategories();
                requestViewModel.Statuses = _combosHelper.GetComboStatusesU();
            }


            if (requestViewModel.StatusId == 3)
            {
                if (requestViewModel.CategoryId == 3)
                {
                    if (files.Count > 0)
                    {
                        var rng = new Random();
                        var message = new Message(new string[] { "alexandra.torres@distrimedical.com.co" }, "TICKET" + " " + requestViewModel.Tittle, requestViewModel.Description + " " + "\n" + "fecha de creación:" + " " + requestViewModel.DateI.ToShortDateString() , files, User.Identity.Name, user.Password);
                        _emailSender.SendEmailAsync(message);
                    }
                    else
                    {
                        var rng = new Random();
                        var message = new Message(new string[] { "alexandra.torres@distrimedical.com.co" }, "TICKET" + " " + requestViewModel.Tittle, requestViewModel.Description + " " + "\n" + "fecha de creación:" + " " + requestViewModel.DateI.ToShortDateString(), null, User.Identity.Name, user.Password);
                        _emailSender.SendEmailAsync(message);
                    }
                   

                }
                else
                {
                    if (files.Count > 0)
                    {
                        var rng = new Random();
                        var message = new Message(new string[] { "soporte@distrimedical.com.co" }, "TICKET" + " " + requestViewModel.Tittle, requestViewModel.Description + " " + "\n" + "fecha de creación:" + " " + requestViewModel.DateI.ToShortDateString(), files, User.Identity.Name, user.Password);
                        _emailSender.SendEmailAsync(message);
                    }
                    else
                    {
                        var rng = new Random();
                        var message = new Message(new string[] { "soporte@distrimedical.com.co" }, "TICKET" + " " + requestViewModel.Tittle, requestViewModel.Description + " " + "\n" + "fecha de creación:" + " " + requestViewModel.DateI.ToShortDateString(), null, User.Identity.Name, user.Password);
                        _emailSender.SendEmailAsync(message);
                    }
                 
                }


            }

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
            User user = await _userHelper.GetUserAsync(requestViewModel.Userr);
            var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();
            if (id != requestViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                Request request = await _converterHelper.ToRequestAsync(requestViewModel, false);
                _context.Requests.Update(request);
                await _context.SaveChangesAsync();
                if (requestViewModel.StatusId == 1)
                {
                    if (requestViewModel.CategoryId == 3)
                    {
                        if (files.Count > 0)
                        {
                            var rng = new Random();
                            var message = new Message(new string[] { requestViewModel.Userr }, "TICKET" + " " + requestViewModel.Tittle + "FUE RESUELTO", "El ticket fue solucionado por el técnico:" + User.Identity.Name + " " + "La solución al ticket fue:" + requestViewModel.Resolution + " " + "\n" + "fecha de resolución:" + " " + requestViewModel.DateF.ToShortDateString(), files, User.Identity.Name, user.Password);
                            _emailSender.SendEmailAsync(message);
                        }
                        else
                        {
                            var rng = new Random();
                            var message = new Message(new string[] { requestViewModel.Userr }, "TICKET" + " " + requestViewModel.Tittle + "FUE RESUELTO", "El ticket fue solucionado por el técnico:" + User.Identity.Name + " " + "La solución al ticket fue:" + requestViewModel.Resolution + " " + "\n" + "fecha de resolución:" + " " + requestViewModel.DateF.ToShortDateString(), null, User.Identity.Name, user.Password);
                            _emailSender.SendEmailAsync(message);
                        }
                  
                    

                    }
                    else
                    {
                        if (files.Count > 0)
                        {
                            var rng = new Random();
                            var message = new Message(new string[] { requestViewModel.Userr }, "TICKET" + " " + requestViewModel.Tittle + "FUE RESUELTO", "El ticket fue solucionado por el técnico:" + User.Identity.Name + " " + "La solución al ticket fue:" + requestViewModel.Resolution + " " + "\n" + "fecha de resolución:" + " " + requestViewModel.DateF.ToShortDateString(), files, User.Identity.Name, user.Password);
                            _emailSender.SendEmailAsync(message);
                        }
                        else
                        {
                            var rng = new Random();
                            var message = new Message(new string[] { requestViewModel.Userr }, "TICKET" + " " + requestViewModel.Tittle + "FUE RESUELTO", "El ticket fue solucionado por el técnico:" + User.Identity.Name + " " + "La solución al ticket fue:" + requestViewModel.Resolution + " " + "\n" + "fecha de resolución:" + " " + requestViewModel.DateF.ToShortDateString(), null, User.Identity.Name, user.Password);
                            _emailSender.SendEmailAsync(message);
                        }

                    }

                }
                else
                {
                    if (requestViewModel.StatusId == 3)
                    {
                        if (requestViewModel.CategoryId == 3)
                        {
                            if (files.Count > 0)
                            {
                                var rng = new Random();
                                var message = new Message(new string[] { "alexandra.torres@distrimedical.com.co" }, "TICKET" + " " + requestViewModel.Tittle + " " + "FUE ABIERTO NUEVAMENTE", requestViewModel.Description + " " + "\n" + "fecha de reapertura:" + " " + requestViewModel.DateI.ToShortDateString(), files, User.Identity.Name, user.Password);
                                _emailSender.SendEmailAsync(message);
                            }
                            else
                            {
                                var rng = new Random();
                                var message = new Message(new string[] { "alexandra.torres@distrimedical.com.co" }, "TICKET" + " " + requestViewModel.Tittle + " " + "FUE ABIERTO NUEVAMENTE", requestViewModel.Description + " " + "\n" + "fecha de reapertura:" + " " + requestViewModel.DateI.ToShortDateString(), null, User.Identity.Name, user.Password);
                                _emailSender.SendEmailAsync(message);
                            }
                         
                        }
                        else
                        {
                            if (files.Count > 0)
                            {
                                var rng = new Random();
                                var message = new Message(new string[] { "soporte@distrimedical.com.co" }, "TICKET" + " " + requestViewModel.Tittle + " " + "FUE ABIERTO NUEVAMENTE", requestViewModel.Description + " " + "\n" + "fecha de reapertura:" + " " + requestViewModel.DateI.ToShortDateString(), files, User.Identity.Name, user.Password);
                                _emailSender.SendEmailAsync(message);
                            }
                            else
                            {
                                var rng = new Random();
                                var message = new Message(new string[] { "soporte@distrimedical.com.co" }, "TICKET" + " " + requestViewModel.Tittle + " " + "FUE ABIERTO NUEVAMENTE", requestViewModel.Description + " " + "\n" + "fecha de reapertura:" + " " + requestViewModel.DateI.ToShortDateString(), null, User.Identity.Name, user.Password);
                                _emailSender.SendEmailAsync(message);
                            }
                           
                        }

                    }
                    else
                    {

                        if (requestViewModel.CategoryId == 3)
                        {

                            if (files.Count > 0)
                            {
                                var rng = new Random();
                                var message = new Message(new string[] { "alexandra.torres@distrimedical.com.co" }, "TICKET" + " " + requestViewModel.Tittle + " " + "SE ENCUENTRA PENDIENTE", requestViewModel.Description, files, User.Identity.Name, user.Password);
                                _emailSender.SendEmailAsync(message);
                            }
                            else
                            {
                                var rng = new Random();
                                var message = new Message(new string[] { "alexandra.torres@distrimedical.com.co" }, "TICKET" + " " + requestViewModel.Tittle + " " + "SE ENCUENTRA PENDIENTE", requestViewModel.Description, null, User.Identity.Name, user.Password);
                                _emailSender.SendEmailAsync(message);
                            }
                      

                        }
                        else
                        {
                            if (files.Count > 0)
                            {
                                var rng = new Random();
                                var message = new Message(new string[] { "soporte@distrimedical.com.co" }, "TICKET" + " " + requestViewModel.Tittle + " " + "SE ENCUENTRA PENDIENTE", requestViewModel.Description, files, User.Identity.Name, user.Password);
                                _emailSender.SendEmailAsync(message);
                            }
                            else
                            {
                                var rng = new Random();
                                var message = new Message(new string[] { "soporte@distrimedical.com.co" }, "TICKET" + " " + requestViewModel.Tittle + " " + "SE ENCUENTRA PENDIENTE", requestViewModel.Description, null, User.Identity.Name, user.Password);
                                _emailSender.SendEmailAsync(message);
                            }

                        }


                    }
                }
                return RedirectToAction(nameof(Index));
            }
            if (User.IsInRole("Admin"))
            {
                requestViewModel.RequestTypes = _combosHelper.GetComboRequestTypes();
                requestViewModel.Categories = _combosHelper.GetComboCategories();
                requestViewModel.Statuses = _combosHelper.GetComboStatuses();
                requestViewModel.Users = _combosHelper.GetComboUsersN();
            }
            else
            {
                requestViewModel.RequestTypes = _combosHelper.GetComboRequestTypes();
                requestViewModel.Categories = _combosHelper.GetComboCategories();
                requestViewModel.Statuses = _combosHelper.GetComboStatusesA();
                requestViewModel.Users = _combosHelper.GetComboUsersB();


            }



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
