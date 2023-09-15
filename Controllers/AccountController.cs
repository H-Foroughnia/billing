using System.Security.Claims;
using billing.Interfaces;
using billing.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace billing.Controllers;

public class AccountController : SiteBaseController
{
    #region constractor
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region register
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost("register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel register)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisteUser(register);
                switch (result)
                {
                    case RegisterUserResult.MobileExists:
                        TempData[ErrorMessage] = "شماره تلفن وارد شده قبلا در سیستم ثبت شده است";
                        break;
                    case RegisterUserResult.Success:
                        TempData[SuccessMessage] = "ثبت نام شما با موفقیت انجام شد";
                        return RedirectToAction("ActiveAccount","Account",new { mobile =register.CardNumber });
                }
            }
        
            return View(register);
        }
        #endregion

        #region login
        [HttpGet("/")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUser(login);
                switch (result)
                {
                    case LoginUserResult.NotFound:
                        TempData[ErrorMessage] = "شماره کارت معتبر نمی باشد، دوباره تلاش کنید!";
                        break;
                    // case LoginUserResult.NotActive:
                    //     TempData[ErrorMessage] = "حساب کاربری شما فعال نمیباشد";
                    //     break;
                    // case LoginUserResult.IsBlocked:
                    //     TempData[WarningMessage] = "حساب شما توسط واحد پشتیبانی مسدود شده است";
                    //     TempData[InfoMessage] = "جهت اطلاع بیشتر لطفا به قسمت تماس باما مراجعه کنید";
                    //     break;
                    case LoginUserResult.Success:
                        var user = await _userService.GetUserByPhoneNumber(login.CardNumber);
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,user.CardNumber),
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principle = new ClaimsPrincipal(identity);
                        TempData[SuccessMessage] = "شماره کارت تایید شد!";
                        return Redirect("home/index");
                }
            }

            return View(login);
        }
        #endregion

        #region log-out
        
        [HttpGet("log-Out")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            TempData[InfoMessage] = "شما با موفقیت خارج شدید";
            return Redirect("/");
        }
        #endregion
        
        #region activate account
        
        
        [HttpGet("activate-account/{mobile}")]
        public async Task<IActionResult> ActiveAccount(string mobile)
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");
            var activeAccount = new ActiveAccountViewModel { PhoneNumber = mobile };
        
            return View(activeAccount);
        }
        
        [HttpPost("activate-account/{mobile}"),ValidateAntiForgeryToken]
        public async Task<IActionResult> ActiveAccount(ActiveAccountViewModel activeAccount)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ActiveAccount(activeAccount);
                switch (result)
                {
                    case ActiveAccountResult.Error:
                        TempData[ErrorMessage] = "عملیات فعال کردن حساب کاربری با شکست مواجه شد";
                        break;
                    case ActiveAccountResult.NotFound:
                        TempData[WarningMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                        break;
                    case ActiveAccountResult.Success:
                        TempData[SuccessMessage] = "حساب کاربری شما با موفقیت فعال شد";
                        TempData[InfoMessage] = "لظفا جهت ادامه فراید وارد حساب کاربری خود شود";
                        return RedirectToAction("Login");
                }
            }
            return View(activeAccount);
        }
        
        #endregion
}