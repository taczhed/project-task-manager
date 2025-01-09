using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_task_manager.Areas.Identity.Data;
using project_task_manager.Models;
using project_task_manager.Enums;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace project_task_manager.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = new List<string>(await _userManager.GetRolesAsync(user));
 
            var totalTasks = await _context.Tasks.CountAsync();
            var completedTasks = await _context.Tasks.CountAsync(t => t.Status == Status.Done);
            var inProgressTasks = await _context.Tasks.CountAsync(t => t.Status == Status.Progress);
            var overdueTasks = await _context.Tasks.CountAsync(t => t.Status == Status.Backlog);

            var taskStats = new
            {
                CompletedPercentage = totalTasks > 0 ? (completedTasks * 100) / totalTasks : 0,
                InProgressPercentage = totalTasks > 0 ? (inProgressTasks * 100) / totalTasks : 0,
                OverduePercentage = totalTasks > 0 ? (overdueTasks * 100) / totalTasks : 0
            };

            ViewBag.User = user;
            ViewBag.Roles = roles;
            ViewBag.TaskStats = taskStats;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
