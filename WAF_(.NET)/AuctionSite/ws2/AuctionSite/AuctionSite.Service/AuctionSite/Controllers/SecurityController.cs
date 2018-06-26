using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AuctionSite.Models.Entities;

namespace AuctionSite.Controllers
{
    [Produces("application/json")]
    [Route("api/Security")]
    public class SecurityController : BaseController
    {
        public SecurityController(Models.IAuctionInterface em)
        {
            auctionService = em;
        }

        [HttpGet("Login/{userName}/{userPassword}")]
        public async Task<IActionResult> Login(String userName, String userPassword)
        {
            try
            {
                // bejelentkeztetjük a felhasználót
                var result = auctionService.LoginAdvertiserPwd(userName, userPassword);
                if (!result.Result) // ha nem sikerült, akkor nincs bejelentkeztetés
                    return   Forbid();

                HttpContext.Session.SetString("user", userName);
                // ha sikeres volt az ellenőrzés
                return Ok();
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Logout")]
        [Authorize] // csak bejelentklezett felhasználóknak
        public async Task<IActionResult> Logout()
        {
            try
            {
                // kijelentkeztetjük az aktuális felhasználót
                if (HttpContext.Session.GetString("advertiser") != null)
                {
                    // ha be volt jelentkezve egy felhasználó, akkor kijelentkeztetjük
                    HttpContext.Session.Remove("advertiser");
                }

                var result = auctionService.Logout();

                if (!result.Result)
                {
                    return Forbid();
                }

                return Ok();
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}