#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuestionsAsked.Data;
using QuestionsAsked.Models;

namespace QuestionsAsked.Controllers
{
    public class QAPairsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QAPairsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QAPairs
        public async Task<IActionResult> Index()
        {
            return View(await _context.QAPair.ToListAsync());
        }

        // GET: QAPairs/SearchForm
        public async Task<IActionResult> SearchForm()
        {
            return View();
        }

        // POST: QAPairs/SearchResults
        public async Task<IActionResult> SearchResults(string SearchPhrase)
        {
            return View("Index",await _context.QAPair.Where(q => q.Question.Contains(SearchPhrase))  .ToListAsync());
        }

        // GET: QAPairs/Answer/5
        public async Task<IActionResult> Answer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qAPair = await _context.QAPair
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qAPair == null)
            {
                return NotFound();
            }

            return View(qAPair);
        }

        // GET: QAPairs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QAPairs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer")] QAPair qAPair)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qAPair);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(qAPair);
        }

        // GET: QAPairs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qAPair = await _context.QAPair.FindAsync(id);
            if (qAPair == null)
            {
                return NotFound();
            }
            return View(qAPair);
        }

        // POST: QAPairs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer")] QAPair qAPair)
        {
            if (id != qAPair.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qAPair);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QAPairExists(qAPair.Id))
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
            return View(qAPair);
        }

        // GET: QAPairs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qAPair = await _context.QAPair
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qAPair == null)
            {
                return NotFound();
            }

            return View(qAPair);
        }

        // POST: QAPairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qAPair = await _context.QAPair.FindAsync(id);
            _context.QAPair.Remove(qAPair);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QAPairExists(int id)
        {
            return _context.QAPair.Any(e => e.Id == id);
        }
    }
}
