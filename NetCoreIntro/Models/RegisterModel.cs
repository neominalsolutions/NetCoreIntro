
// System.Data.Annotations ile modelde validasyon tanımları yapılabilir
// Attribute based yöntem



using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCoreIntro.Models
{
  public class RegisterModel
  {

    [Required(ErrorMessage = "Name alanı boş geçilemez")]
    [MinLength(8,ErrorMessage ="Name alanı Minimmum 8 karakter olmalıdır")]
    [DisplayName("Adı")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "E-posta alanı boş geçilemez")]
    [EmailAddress(ErrorMessage = "E-posta formatına uygun giriniz")]
    public string? Email { get; set; }

    [Display(Name = "Şifre")]
    [Required(ErrorMessage = "Parola alanı boş geçilemez")]
    [Compare("PasswordConfirm", ErrorMessage = "Parolalar eşleşmiyor")]
    //[RegularExpression(@"^(?!.([A-Za-z0-9])\1{1})(?=.?[A-Z])(?=.?[a-z])(?=.?[0-9])(?=.?[#?!@$%^&-]).{8,}$", ErrorMessage = "Parola Hatalı")]
    // min 1 digit 1 upper 1 lowercase min 8 karakter
    public string? Password { get; set; }

    [Required(ErrorMessage = "PasswordConfirm alanı boş geçilemez")]
    [MinLength(8, ErrorMessage = "PasswordConfirm alanı Minimmum 8 karakter olmalıdır")]
    public string? PasswordConfirm { get; set; }

    public string? City { get; set; }



  }
}
