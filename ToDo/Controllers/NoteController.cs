using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var notes = await _context.Notes_lw9_02
                .Where(note => note.UserId.ToString() == userId)
                .ToListAsync();
            Console.WriteLine($"Заметки: {string.Join(", ", notes.Select(n => n.Title))}");

            return View(notes);
        }

        // GET: Note/Create
        [Authorize]
        public IActionResult Create()
        {
            return Redirect("~/Note");
        }

        // POST: Note/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Title,Description,File")] Note_lw9_02 note_lw9_02)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var createNote = new Note_lw9_02
            {
                Title = note_lw9_02.Title,
                Description = note_lw9_02.Description,
                UserId = Int32.Parse(userId),
                File = note_lw9_02.File,

            };

            if (ModelState.IsValid)
            {

                if (createNote.File != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(createNote.File.FileName) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(createNote.File.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await createNote.File.CopyToAsync(stream);
                    }
                    createNote.FilePatch = "/uploads/" + fileName;
                }
                _context.Add(createNote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Redirect("~/Note");
        }

        // GET: Note/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (id == null)
            {
                return NotFound();
            }
            var note_lw5_02 = await _context.Notes_lw9_02.FindAsync(id);

            if (note_lw5_02 == null)
            {
                return NotFound();
            }
            return View(note_lw5_02);
        }

        // POST: Note/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,UserId,File")] Note_lw9_02 note_lw9_02)
        {
            if (id != note_lw9_02.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (note_lw9_02.File != null)
                    {
                        // Генерация уникального имени файла
                        var fileName = Path.GetFileNameWithoutExtension(note_lw9_02.File.FileName) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(note_lw9_02.File.FileName);

                        // Путь для сохранения файла
                        var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                        // Проверка существования директории и создание, если она не существует
                        if (!Directory.Exists(uploadsDirectory))
                        {
                            Directory.CreateDirectory(uploadsDirectory);
                        }

                        var filePath = Path.Combine(uploadsDirectory, fileName);

                        // Сохранение файла на сервере
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await note_lw9_02.File.CopyToAsync(stream);
                        }

                        // Сохранение пути к файлу в модели
                        note_lw9_02.FilePatch = "/uploads/" + fileName;
                    }

                    _context.Update(note_lw9_02);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Note_lw5_02Exists(note_lw9_02.Id))
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
            return View(note_lw9_02);
        }

        // GET: Note/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note_lw5_02 = await _context.Notes_lw9_02
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
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note_lw5_02 = await _context.Notes_lw9_02.FindAsync(id);
            if (note_lw5_02 != null)
            {
                _context.Notes_lw9_02.Remove(note_lw5_02);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Notes/DeleteFile/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var note = await _context.Notes_lw9_02.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            // Удаление файла с сервера
            if (!string.IsNullOrEmpty(note.FilePatch))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", note.FilePatch.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                note.FilePatch = null;
                _context.Update(note);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Edit), new { id = note.Id });
        }


        private bool Note_lw5_02Exists(int id)
        {
            return _context.Notes_lw9_02.Any(e => e.Id == id);
        }
    }

}
