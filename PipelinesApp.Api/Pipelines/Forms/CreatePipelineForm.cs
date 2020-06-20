namespace PipelinesApp.Api.Pipelines.Forms
{
    using System.ComponentModel.DataAnnotations;

    public class CreatePipelineForm
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}