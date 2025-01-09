using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using project_task_manager.Areas.Identity.Data;
using project_task_manager.Enums;
using project_task_manager.Models;

namespace project_task_manager.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TaskController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tasks.Include(a => a.Executor).Include(a => a.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        
        public async Task<IActionResult> ForMe()
        {
            var userId = _userManager.GetUserId(User);
            var applicationDbContext = _context.Tasks
                .Include(a => a.Executor)
                .Include(a => a.Project)
                .Where(t => t.ExecutorId == userId);

            return View(await applicationDbContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationTask = await _context.Tasks
                .Include(a => a.Executor)
                .Include(a => a.Project)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (applicationTask == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (!User.IsInRole("Admin") && applicationTask.ExecutorId != userId)
            {
                return Forbid();
            }

            return View(applicationTask);
        }

        
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ExecutorId"] = new SelectList(_context.Users, "Id", "Email");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ID", "Title");
            ViewData["Priority"] = new SelectList(Enum.GetValues(typeof(Priority)));
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(Status)));

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,Priority,Status,ExecutorId,ProjectId")] ApplicationTask applicationTask)
        {
            _context.Add(applicationTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationTask = await _context.Tasks.FindAsync(id);
            if (applicationTask == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (!User.IsInRole("Admin") && applicationTask.ExecutorId != userId)
            {
                return Forbid();
            }

            ViewData["ExecutorId"] = new SelectList(_context.Users, "Id", "Email", applicationTask.ExecutorId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ID", "Title", applicationTask.ProjectId);
            ViewData["Priority"] = new SelectList(Enum.GetValues(typeof(Priority)));
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(Status)));
            return View(applicationTask);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Priority,ExecutorId,ProjectId")] ApplicationTask applicationTask)
        {
            if (id != applicationTask.ID)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (!User.IsInRole("Admin") && applicationTask.ExecutorId != userId)
            {
                return Forbid();
            }

            try
            {
                _context.Update(applicationTask);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationTaskExists(applicationTask.ID))
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

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationTask = await _context.Tasks
                .Include(a => a.Executor)
                .Include(a => a.Project)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (applicationTask == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (!User.IsInRole("Admin") && applicationTask.ExecutorId != userId)
            {
                return Forbid();
            }

            return View(applicationTask);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationTask = await _context.Tasks.FindAsync(id);

            if (applicationTask == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (!User.IsInRole("Admin") && applicationTask.ExecutorId != userId)
            {
                return Forbid();
            }

            _context.Tasks.Remove(applicationTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddSolution(int id, string solution)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.ID == id);
            if (task == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (!User.IsInRole("Admin") && task.ExecutorId != userId)
            {
                return Forbid();
            }

            task.Solution = solution;
            task.Status = Status.Done; 
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); 
        }

        private bool ApplicationTaskExists(int id)
        {
            return _context.Tasks.Any(e => e.ID == id);
        }
    }
}
