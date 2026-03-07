using Microsoft.AspNetCore.Mvc;
using StockApp.StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockApp.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(StockUser user);
    }
}
