using System.ComponentModel.DataAnnotations;
using billing.Models.BaseEntities;

namespace billing.Models.Account;

public class Card: BaseEntity
{
    [Display(Name = "شماره کارت ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string CardNumber { get; set; }
    
}