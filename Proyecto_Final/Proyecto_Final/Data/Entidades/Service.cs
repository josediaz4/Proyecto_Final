using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Final.Data.Entidades
{
    public class Service
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

        [Display(Name = "Precio")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]        
        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        public decimal Price { get; set; }

        public bool State { get; set; }

        [Display(Name = "Duración")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]        
        public float Duration { get; set; }

        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://mlpeluquerias.azurewebsites.net/images/nono-image.png"
            : $"https://peluqueria.blob.core.windows.net/service/{ImageId}";
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
