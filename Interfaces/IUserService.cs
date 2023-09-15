using billing.Models.Account;
using billing.ViewModels.Account;

namespace billing.Interfaces;

public interface IUserService
{
    #region account
    Task<RegisterUserResult> RegisteUser(RegisterUserViewModel register);
    Task<LoginUserResult> LoginUser(LoginUserViewModel login);
    Task<Card> GetUserByPhoneNumber(string phoneNumber);
    Task<ActiveAccountResult> ActiveAccount(ActiveAccountViewModel activeAccount);
    Task<Card> GetUserById(long userId);
    #endregion
}