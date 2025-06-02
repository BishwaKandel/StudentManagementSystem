using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly AppDbContext _context;

        public EnrollmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Enrollments.Include(e => e.Course).Include(e => e.Student);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (enrollment == null) return NotFound();

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "FirstName");
            return View();
        }

        // POST: Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,CourseID,EnrollmentDate")] Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseID);
                ViewData["StudentID"] = new SelectList(_context.Students, "ID", "FirstName", enrollment.StudentID);
                return View(enrollment);
            }

            try
            {
                // Check if enrollment already exists
                var existingEnrollment = await _context.Enrollments
                    .FirstOrDefaultAsync(e => e.StudentID == enrollment.StudentID
                        && e.CourseID == enrollment.CourseID);

                if (existingEnrollment != null)
                {
                    ModelState.AddModelError("", "This student is already enrolled in this course.");
                    ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseID);
                    ViewData["StudentID"] = new SelectList(_context.Students, "ID", "FirstName", enrollment.StudentID);
                    return View(enrollment);
                }

                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error saving enrollment: " + ex.Message);
                ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseID);
                ViewData["StudentID"] = new SelectList(_context.Students, "ID", "FirstName", enrollment.StudentID);
                return View(enrollment);
            }
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) return NotFound();

            ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "FirstName", enrollment.StudentID);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentID,CourseID,EnrollmentDate")] Enrollment enrollment)
        {
            if (id != enrollment.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Id)) return NotFound();
                    throw;
                }
            }

            ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "FirstName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (enrollment == null) return NotFound();

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.Id == id);
        }

    }
}
