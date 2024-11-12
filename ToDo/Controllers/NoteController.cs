using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class NoteController : Controller
    {
        private readonly AppDbContext _context;

        public NoteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Note
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Notes_lw5_02.ToListAsync());
        }

        // GET: Note/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note_lw5_02 = await _context.Notes_lw5_02
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note_lw5_02 == null)
            {
                return NotFound();
            }

            return View(note_lw5_02);
        }

        // GET: Note/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Note/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,UserId")] Note_lw5_02 note_lw5_02)
        {
            if (ModelState.IsValid)
            {
                _context.Add(note_lw5_02);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(note_lw5_02);
        }

        // GET: Note/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note_lw5_02 = await _context.Notes_lw5_02.FindAsync(id);
            if (note_lw5_02 == null)
            {
                return NotFound();
            }
            return View(note_lw5_02);
        }

        // POST: Note/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,UserId")] Note_lw5_02 note_lw5_02)
        {
            if (id != note_lw5_02.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note_lw5_02);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Note_lw5_02Exists(note_lw5_02.Id))
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
            return View(note_lw5_02);
        }

        // GET: Note/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note_lw5_02 = await _context.Notes_lw5_02
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note_lw5_02 == null)
            {
                return NotFound();
            }

            return View(note_lw5_02);
        }

        // POST: Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note_lw5_02 = await _context.Notes_lw5_02.FindAsync(id);
            if (note_lw5_02 != null)
            {
                _context.Notes_lw5_02.Remove(note_lw5_02);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Note_lw5_02Exists(int id)
        {
            return _context.Notes_lw5_02.Any(e => e.Id == id);
        }
    }
}
