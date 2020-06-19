namespace Pipelines.Api.Tasks.Forms
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTaskForm
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}