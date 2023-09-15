using billing.Interfaces;
using billing.Repositories;
using billing.Services;

namespace billing;

public class DependencyContainer
{
    public static void RegisterService(IServiceCollection services)
    {
        #region services
        services.AddScoped<IUserService, UserService>();
        #endregion

        #region repositories
        services.AddScoped<IUserRepository, UserRepository>();
        #endregion

        #region tools
        services.AddScoped<IPasswordHelper, PasswordHelper>();
        #endregion
    }
}