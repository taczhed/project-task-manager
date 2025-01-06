using project_task_manager.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using project_task_manager.Areas.Identity.Data;

namespace project_task_manager.Models
{
    public class ApplicationProject
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(1024)")]
        public string Description { get; set; }

        // Foreign Key
        public string ManagerId { get; set; } // The manager of this project

        // Navigation Properties
        public ApplicationUser Manager { get; set; } // Manager of the project
        public ICollection<ApplicationTask> Tasks { get; set; } // Tasks associated with the project

    }
}
