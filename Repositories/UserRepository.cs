using billing.Context;
using billing.Interfaces;
using billing.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace billing.Repositories;

public class UserRepository: IUserRepository
{
    #region constractor
    private readonly BillDbContext _context;
    public UserRepository(BillDbContext context)
    {
        _context = context;
    }

    #endregion

    #region account
    public async Task<bool> IsUserExistPhoneNumber(string cardNumber)
    {
        return await _context.Cards.AsQueryable().AnyAsync(c => c.CardNumber == cardNumber);
    }

    public async Task CreateUser(Card card)
    {
        await _context.Cards.AddAsync(card);
           
    }
    public async Task<Card> GetUserByPhoneNumber(string cardNumber)
    {
        return await _context.Cards.AsQueryable()
            .SingleOrDefaultAsync(c => c.CardNumber == cardNumber);
    }
    public void UpdateUser(Card card)
    {
        _context.Cards.Update(card);
    }

    public async Task SaveChange()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Card> GetUserById(long userId)
    {
        return await _context.Cards.AsQueryable()
            .SingleOrDefaultAsync(c => c.Id == userId);
    }




    #endregion
}