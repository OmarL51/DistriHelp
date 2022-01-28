using DistriHelp.API.Data;
using DistriHelp.API.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Controllers
{
    public class SolutionsController : Controller
    {
        private readonly DataContext _context;
        public SolutionsController(DataContext context)
        {
            _context = context;
        }


        // GET: Solutions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Solutions.ToListAsync());
        }

        // GET: Solutions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Solution solution = await _context.Solutions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solution == null)
            {
                return NotFound();
            }

            return View(solution);
        }

        // GET: Solutions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Solutionss/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Solution solution)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(solution);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe esta solución");
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
            return View(solution);
        }

        // GET: Solutions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Solution solution = await _context.Solutions.FindAsync(id);
            if (solution == null)
            {
                return NotFound();
            }
            return View(solution);
        }

        // POST: Solutions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Solution solution)
        {
            if (id != solution.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solution);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe esta solución");
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
            return View(solution);
        }

        // GET: Solutions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Solution solution = await _context.Solutions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solution == null)
            {
                return NotFound();
            }
            _context.Solutions.Remove(solution);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
