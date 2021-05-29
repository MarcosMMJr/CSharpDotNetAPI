using System.ComponentModel.DataAnnotations;

namespace vaivoa.Models
{
  public class CardInfo
  {
    //Id
    [Key]
    public int Id { get; set; }
    
    //CardNumber
    [Required(ErrorMessage = "Campo obrigatório.")]
    [MaxLength(16, ErrorMessage = "Este campo deve conter 16 dígitos.")]
    [MinLength(16, ErrorMessage = "Este campo deve conter 16 dígitos.")]   
     public string CardNumber { get; set; }

    //CardType
    [Required(ErrorMessage = "Campo obrigatório.")]
    [MaxLength(7, ErrorMessage = "Este campo deve ser 'Físico' ou 'Virtual'.")]
    [MinLength(6, ErrorMessage = "Este campo deve ser 'Físico' ou 'Virtual'.")]   
     public string CardType { get; set; }

    //AccountId
    [Required(ErrorMessage = "Este campo é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida.")]
    public int AccountId {get; set; }

    public AccountInfo AccountInfo {get; set; }
  }
}