using billing.Interfaces;
using billing.Models.Account;
using billing.ViewModels.Account;

namespace billing.Services;

public class UserService: IUserService
{
    #region constractor
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        public UserService(IUserRepository userRepository ,IPasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        }

        #endregion

        #region account
        public async Task<RegisterUserResult> RegisteUser(RegisterUserViewModel register)
        {
            if(!await _userRepository.IsUserExistPhoneNumber(register.CardNumber))
            {
                var user = new Card
                {
                    CardNumber = register.CardNumber,
                };
                await _userRepository.CreateUser(user);
                await _userRepository.SaveChange();
                return RegisterUserResult.Success;
            }

            return RegisterUserResult.MobileExists;
        }

        public async Task<LoginUserResult> LoginUser(LoginUserViewModel login)
        {
            var user = await _userRepository.GetUserByPhoneNumber(login.CardNumber);
            if (user == null) return LoginUserResult.NotFound;

            return LoginUserResult.Success;
        }

        public async Task<Card> GetUserByPhoneNumber(string phoneNumber)
        {
            return await _userRepository.GetUserByPhoneNumber(phoneNumber);
        }

        public async Task<ActiveAccountResult> ActiveAccount(ActiveAccountViewModel activeAccount)
        {
            var user = await _userRepository.GetUserByPhoneNumber(activeAccount.PhoneNumber);

            if (user == null) return ActiveAccountResult.NotFound;
            
                _userRepository.UpdateUser(user);
                await _userRepository.SaveChange();

                return ActiveAccountResult.Success;
            }

        public Task<Card> GetUserById(long userId)
        {
            throw new NotImplementedException();
        }
}
        #endregion