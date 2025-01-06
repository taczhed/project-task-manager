using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_task_manager.Areas.Identity.Data;
using project_task_manager.Models;

namespace project_task_manager.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Project
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Projects.Include(a => a.Manager);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Project/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationProject = await _context.Projects
                .Include(a => a.Manager)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (applicationProject == null)
            {
                return NotFound();
            }

            return View(applicationProject);
        }

        // GET: Project/Create
        public IActionResult Create()
        {
            ViewData["ManagerId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,ManagerId")] ApplicationProject applicationProject)
        {

            _context.Add(applicationProject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationProject = await _context.Projects.FindAsync(id);
            if (applicationProject == null)
            {
                return NotFound();
            }
            ViewData["ManagerId"] = new SelectList(_context.Users, "Id", "Email", applicationProject.ManagerId);
            return View(applicationProject);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,ManagerId")] ApplicationProject applicationProject)
        {
            if (id != applicationProject.ID)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(applicationProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationProjectExists(applicationProject.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
        }

        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationProject = await _context.Projects
                .Include(a => a.Manager)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (applicationProject == null)
            {
                return NotFound();
            }

            return View(applicationProject);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationProject = await _context.Projects.FindAsync(id);
            if (applicationProject != null)
            {
                _context.Projects.Remove(applicationProject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ID == id);
        }
    }
}
