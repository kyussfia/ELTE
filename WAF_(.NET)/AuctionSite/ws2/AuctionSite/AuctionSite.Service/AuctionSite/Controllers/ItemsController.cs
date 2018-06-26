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
using System.Diagnostics;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;

namespace AuctionSite.Controllers
{
    [Route("api/Items")]
    public class ItemsController : BaseController
    {
        private readonly AuctionContext _context;

        public ItemsController(AuctionContext context)
        {
            _context = context;
        }

        // GET: api/Items/advID
        [HttpGet("{advertiserId}")]
        public IEnumerable<ItemDTO> GetItems([FromRoute] int advertiserId)
        {
            return _context.Items.Where(i => i.AdvertiserId == advertiserId).OrderByDescending(b => b.ClosedAt).Select(i => new ItemDTO {
                Id = i.Id,
                Name = i.Name,
                CategoryId = i.CategoryId,
                Description = i.Description,
                OriginalBid = i.OriginalBid,
                Currency = i.Currency,
                CreatedAt = i.CreatedAt,
                ClosedAt = i.ClosedAt,
                AdvertiserId = i.AdvertiserId,
                Picture = i.Picture
            }).ToList();
        }

        // GET: api/Items/5
        [HttpGet("{id}", Name = "GetItem")]
        public async Task<IActionResult> GetItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.Items.SingleOrDefaultAsync(m => m.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        [HttpPut("{item}")]
        public IActionResult PutItem([FromBody] ItemDTO item)
        {
            try
            {
                Item mod = _context.Items.FirstOrDefault(i => i.Id == item.Id);

                if (mod == null) // ha nincs ilyen azonosító, akkor hibajelzést küldünk
                    return NotFound();

                mod.Name = item.Name;
                mod.Description = item.Description == null ? "" : item.Description;
                mod.OriginalBid = item.OriginalBid;
                mod.CategoryId = item.CategoryId;
                mod.Currency = item.Currency;
                mod.AdvertiserId = item.AdvertiserId;
                mod.ClosedAt = item.ClosedAt;
                mod.CreatedAt = item.CreatedAt;
                mod.Picture = item.Picture;

                _context.SaveChanges();

                return CreatedAtRoute("GetItem", new { id = mod.Id }, item);
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Items
        [HttpPost]
        public IActionResult PostItem([FromBody] ItemDTO item)
        {
            try
            {
            /*if (HttpContext.Session.GetString("advertiser") == null || HttpContext.Session.GetString("advertiser") != item.AdvertiserId.ToString())
            {
                return Unauthorized();
            }*/

            if (item == null)
                return NoContent();

                var addedItem = _context.Items.Add(new Item
                {
                    Name = item.Name,
                    Description = item.Description == null ? "" : item.Description,
                    OriginalBid = item.OriginalBid,
                    CategoryId = item.CategoryId,
                    Currency = item.Currency,
                    AdvertiserId = item.AdvertiserId,
                    ClosedAt = item.ClosedAt,
                    CreatedAt = DateTime.Now,
                    Picture = item.Picture
                });

                _context.SaveChanges();

                item.Id = addedItem.Entity.Id;

                return CreatedAtRoute("GetItem", new { id = addedItem.Entity.Id }, item);
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}