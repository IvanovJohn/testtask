namespace PipelinesApp.Api.Pipelines.Forms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PipelinesApp.Api.Tasks.ViewModels;

    public class CreatePipelineForm
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public IEnumerable<TaskViewModel> Tasks { get; set; }
    }
}