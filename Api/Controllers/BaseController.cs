using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockApp.StockApp.Core.Models;

namespace StockApp.Api.Controllers
{
    public class BaseController : Controller
    {
        private readonly UserManager<StockUser> _userManager;
        public BaseController(UserManager<StockUser> userManager)
        {
            _userManager = userManager;
        }
        protected async Task<StockUser?> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(User);
        }
    }
}
