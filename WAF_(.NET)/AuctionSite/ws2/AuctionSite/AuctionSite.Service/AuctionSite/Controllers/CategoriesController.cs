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
    [Route("api/Categories")]
    public class CategoriesController : Controller
    {
        private readonly AuctionContext _context;

        public CategoriesController(AuctionContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _context.Categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var cat = await _context.Categories.SingleOrDefaultAsync(m => m.Id == id);

            if (cat == null)
            {
                return NotFound();
            }

            CategoryDTO category = new CategoryDTO
            {
                Id = cat.Id,
                Name = cat.Name,
            };

            return Ok(category);
        }
    }
}