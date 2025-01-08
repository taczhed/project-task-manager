using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using project_task_manager.Enums;
using project_task_manager.Areas.Identity.Data;

namespace project_task_manager.Models
{
    public class ApplicationTask
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(1024)")]
        public string Description { get; set; }

        public Priority Priority { get; set; }
        public Status Status { get; set; }

        // Foreign Keys
        public string ExecutorId { get; set; } // User assigned to this task
        public int ProjectId { get; set; }  // The project to which this task belongs

        // Navigation Properties
        public ApplicationUser Executor { get; set; }  // User executing the task
        public ApplicationProject Project { get; set; } // Project this task belongs to
    }
}
