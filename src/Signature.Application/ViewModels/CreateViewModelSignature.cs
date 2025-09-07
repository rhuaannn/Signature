using Signature.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Signature.Application.ViewModels
{
    public class CreateViewModelSignature
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Situation { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public CreateViewModelSignature(string name, int situation, string description, DateTime createdDate)
        {
            Name = name;
            Situation = situation;
            Description = description;
            CreatedDate = createdDate;
        }


    }
}
