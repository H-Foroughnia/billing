using System.ComponentModel.DataAnnotations;

namespace billing.ViewModels.Account;

public class RegisterUserViewModel
{
    [Display(Name = "شماره کارت")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string CardNumber { get; set; }
}
public enum RegisterUserResult
{
    MobileExists,
    Success
}