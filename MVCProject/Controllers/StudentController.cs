using MVCProject.Models;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Data;
using System;
using Microsoft.EntityFrameworkCore;

namespace MVCProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDBContext _db;

        public StudentController(AppDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Student> allStudent = _db.Students;
            return View(allStudent);
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            var students = GetStudents();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString)).ToList();
            }

            ViewBag.SearchString = searchString;
            return View(students);
        }

        public List<Student> GetStudents()
        {
            return _db.Students.ToList();
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student obj)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Students.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student obj)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //ค้นหาข้อมูล
            var obj = _db.Students.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Students.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
