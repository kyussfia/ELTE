using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionSite.Models.Db;
using AuctionSite.Models.Entities;
using AuctionSite.Data;

namespace AuctionSite.Controllers
{
    [Route("api/Bids")]
    public class BidsController : Controller
    {
        private readonly AuctionContext _context;

        public BidsController(AuctionContext context)
        {
            _context = context;
        }

        // GET: api/Bids
        [HttpGet]
        public IEnumerable<BidDTO> GetBids()
        {
            return _context.Bids.Select(b => new BidDTO
            {
                Id = b.Id,
                ItemId = b.ItemId,
                CreatedAt = b.CreatedAt,
                Price = b.Price,
                UserId = b.UserId
            }).ToList();
        }

        // GET: api/Bids/5
        [HttpGet("{itemId}")]
        public IActionResult GetBids([FromRoute] int itemId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bids = _context.Bids.Where(b => b.ItemId == itemId).OrderByDescending(b => b.CreatedAt).ToList();

            if (bids == null)
            {
                return NotFound();
            }

            return Ok(bids);
        }
    }
}