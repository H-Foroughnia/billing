using billing.Models.Account;

namespace billing.Interfaces;

public interface IUserRepository
{
    #region account
    Task<bool> IsUserExistPhoneNumber(string phoneNumber);
    Task CreateUser(Card card);
    Task<Card> GetUserByPhoneNumber(string phoneNumber);
    Task<Card> GetUserById(long userId);
    void UpdateUser(Card card);
    Task SaveChange();
    #endregion
}