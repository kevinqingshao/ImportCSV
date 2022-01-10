using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImportCSV.Data;
using ImportCSV.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Data;
using ImportCSV.Services;

namespace ImportCSV.Controllers
{
    public class StandardAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StandardAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StandardAccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.StandardAccount.ToListAsync());
        }

        // GET: StandardAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardAccount = await _context.StandardAccount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (standardAccount == null)
            {
                return NotFound();
            }

            return View(standardAccount);
        }

        // GET: StandardAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StandardAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountCode,Name,Type,OpenDate,Currency")] StandardAccount standardAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(standardAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(standardAccount);
        }

        // GET: StandardAccounts/Import
        public IActionResult Import()
        {
            return View();
        }

        // POST: StandardAccounts/Import
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Import")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile upload, string fileFormat, bool hasCSVHeader)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.FileName.Length > 0)
                {
                    if (upload.FileName.EndsWith(".csv"))
                    {
                        using (var sreader = new StreamReader(upload.OpenReadStream()))
                        {
                            try
                            {
                                List<StandardAccount> standardAccounts = new List<StandardAccount>();
                                if (hasCSVHeader)
                                {
                                    string[] headers = sreader.ReadLine().Split(','); //Header
                                }
                                while (!sreader.EndOfStream)   //get all the content in rows 
                                {
                                    string[] rows = sreader.ReadLine().Split(',');
                                    StandardAccount standardAccount = new StandardAccount();
                                    switch (fileFormat)
                                    {
                                        case "0":
                                            new Transform(new StandardTransform()).Parse(rows, standardAccount);
                                            break;
                                        case "1":
                                            new Transform(new Type1Transform()).Parse(rows, standardAccount);
                                            break;
                                        case "2":
                                            new Transform(new Type2Transform()).Parse(rows, standardAccount);
                                            break;
                                        default:
                                            ModelState.AddModelError("File", "You have chosen an invalid mode of transform.");
                                            break;
                                    }
                                    standardAccounts.Add(standardAccount);
                                }
                                _context.StandardAccount.AddRange(standardAccounts);
                                await _context.SaveChangesAsync();
                            }
                            catch (Exception e)
                            {
                                ModelState.AddModelError("File", "This is an error: " + e);
                            }
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }

        // GET: StandardAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardAccount = await _context.StandardAccount.FindAsync(id);
            if (standardAccount == null)
            {
                return NotFound();
            }
            return View(standardAccount);
        }

        // POST: StandardAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountCode,Name,Type,OpenDate,Currency")] StandardAccount standardAccount)
        {
            if (id != standardAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(standardAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StandardAccountExists(standardAccount.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(standardAccount);
        }

        // GET: StandardAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardAccount = await _context.StandardAccount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (standardAccount == null)
            {
                return NotFound();
            }

            return View(standardAccount);
        }

        // POST: StandardAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var standardAccount = await _context.StandardAccount.FindAsync(id);
            _context.StandardAccount.Remove(standardAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StandardAccountExists(int id)
        {
            return _context.StandardAccount.Any(e => e.Id == id);
        }
    }
}
