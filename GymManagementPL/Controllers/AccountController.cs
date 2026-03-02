using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.AccountViewModels;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAccountService _accountService;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager)
		{
			_accountService = accountService;
			_signInManager = signInManager;
		}
		#region Login
		public ActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Login(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid) return View(loginViewModel);

			var User = _accountService.ValidateUser(loginViewModel);
			if (User is null)
			{
				ModelState.AddModelError("InvalidLogin", "Invalid email or password.");
				return View(loginViewModel);
			}
			// Sign in the user
			var Result = _signInManager.PasswordSignInAsync(User, loginViewModel.Password, loginViewModel.RememberMe, false).Result;
			if(Result.IsNotAllowed)
				ModelState.AddModelError("NotAllowed", "Your account is not allowed to sign in.");
			if (Result.IsLockedOut)
				ModelState.AddModelError("LockedOut", "Your account is locked out.");
			if(Result.Succeeded)
				return RedirectToAction("Index", "Home");
			return View(loginViewModel);
		}
		#endregion
		#region Logout
		[HttpPost]
		public ActionResult Logout()
		{
			_signInManager.SignOutAsync().GetAwaiter().GetResult();
			return RedirectToAction(nameof(Login));
		}
		#endregion
		#region AccessDenied
		public ActionResult AccessDenied()
		{
			return View();
		}
		#endregion
	}
}
