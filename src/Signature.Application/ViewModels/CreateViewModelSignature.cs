using Signature.Domain.Enum;
using Signature.Exception.Exception;
using System.ComponentModel.DataAnnotations;

namespace Signature.Application.ViewModels
{
    public class CreateViewModelSignature
    {
        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "A situação é obrigatória")]
        [Range(0, 2, ErrorMessage = "Valor inválido para situação. Use: 0 (Ativo), 1 (Inativo) ou 2 (Cancelado)")]
        public int? Situation { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public CreateViewModelSignature()
        {

        }
        public CreateViewModelSignature(string name, int situation, string description, DateTime createdDate, DateTime endDate)
        {
            switch (situation)
            {
                case (int)SignatureEnum.Active:
                case (int)SignatureEnum.Locked:
                case (int)SignatureEnum.Cancelled:
                    break;
                default:
                    throw new DomainValidationException($"Invalid situation value: {situation}");
            }


            Name = name;
            Situation = situation;
            Description = description;
            CreatedDate = createdDate;
            EndDate = endDate;

        }


    }
}
