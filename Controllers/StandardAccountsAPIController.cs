using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImportCSV.Data;
using ImportCSV.Models;

namespace ImportCSV.Controllers
{
    /// <summary>
    /// About this api controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StandardAccountsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// About StandardAccountsAPIController.
        /// </summary>
        public StandardAccountsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StandardAccountsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StandardAccount>>> GetStandardAccount()
        {
            return await _context.StandardAccount.ToListAsync();
        }
        /// <summary>
        /// Gets the list of all Standard Account.
        /// </summary>
        /// <returns>The list of Standard Account.</returns>
        // GET: api/StandardAccountsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StandardAccount>> GetStandardAccount(int id)
        {
            var standardAccount = await _context.StandardAccount.FindAsync(id);

            if (standardAccount == null)
            {
                return NotFound();
            }

            return standardAccount;
        }

        /// <summary>
        /// PUT Standard Account.
        /// </summary>
        // PUT: api/StandardAccountsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStandardAccount(int id, StandardAccount standardAccount)
        {
            if (id != standardAccount.Id)
            {
                return BadRequest();
            }

            _context.Entry(standardAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StandardAccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Post Standard Account.
        /// </summary>
        // POST: api/StandardAccountsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StandardAccount>> PostStandardAccount(StandardAccount standardAccount)
        {
            _context.StandardAccount.Add(standardAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStandardAccount", new { id = standardAccount.Id }, standardAccount);
        }
        /// <summary>
        /// DELETE Standard Account.
        /// </summary>
        // DELETE: api/StandardAccountsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStandardAccount(int id)
        {
            var standardAccount = await _context.StandardAccount.FindAsync(id);
            if (standardAccount == null)
            {
                return NotFound();
            }

            _context.StandardAccount.Remove(standardAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StandardAccountExists(int id)
        {
            return _context.StandardAccount.Any(e => e.Id == id);
        }
    }
}
