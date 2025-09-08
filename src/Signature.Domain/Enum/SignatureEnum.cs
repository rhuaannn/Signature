using System.ComponentModel.DataAnnotations;

namespace Signature.Domain.Enum
{
    public enum SignatureEnum
    {
        [Display(Name = "Ativa")]
        Active = 0,
        [Display(Name = "Cancelado")]
        Cancelled = 1,
        [Display(Name = "Bloqueado")]
        Locked = 2
    }
}
