using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionSite.Models.Db;
using AuctionSite.Models.Entities;
using System.Diagnostics;

namespace AuctionSite.Controllers
{
    [Route("api/Advertisers")]
    public class AdvertisersController : Controller
    {
        private readonly AuctionContext _context;

        public AdvertisersController(AuctionContext context)
        {
            _context = context;
        }

        // GET: api/Advertisers/name
        [HttpGet("{username}")]
        public IActionResult GetAdvertiser([FromRoute] String username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var advertiser = _context.Advertisers.SingleOrDefault(m => m.Username == username);

            if (advertiser == null)
            {
                return NotFound();
            }

            return Ok(advertiser);
        }
    }
}