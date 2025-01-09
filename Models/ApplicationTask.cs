using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using project_task_manager.Enums;
using project_task_manager.Areas.Identity.Data;

namespace project_task_manager.Models
{
    public class ApplicationTask
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(1024)")]
        public string Description { get; set; }

        public Priority Priority { get; set; }
        public Status Status { get; set; }

        public string? ExecutorId { get; set; }
        public int ProjectId { get; set; }

        public ApplicationUser? Executor { get; set; }
        public ApplicationProject? Project { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Solution { get; set; } 
    }
}
