using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using project_task_manager.Models;

namespace project_task_manager.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    // Navigation Properties
    public ICollection<ApplicationProject> ManagedProjects { get; set; } // Projects managed by this user
    public ICollection<ApplicationTask> ExecutedTasks { get; set; }      // Tasks executed by this user
}

