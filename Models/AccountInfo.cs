using System.ComponentModel.DataAnnotations;

namespace vaivoa.Models
{
  public class AccountInfo
  {
    [Key]
    public int Id { get; set; }

    //Email
    [Required(ErrorMessage = "Campo obrigatório.")]
    [MaxLength(30, ErrorMessage = "Este campo deve conter entre 11 e 30 caracteres.")]
    [MinLength(11, ErrorMessage = "Este campo deve conter entre 11 e 30 caracteres.")]
    public string Email { get; set; }

    //AccountName
    [Required(ErrorMessage = "Campo obrigatório.")]
    [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
    [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
    public string AccountName { get; set; }
  }
}