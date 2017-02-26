using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MongoMvc.Interfaces;
using MongoMvc.Model;
using System.Collections.Generic;

namespace MongoMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public HomeController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Read()
        {
            //IEnumerable<Note> noteElements = await _noteRepository.GetAllNotes();
            //ViewData["Message"] = string.Format($"Note Id: {Id} - Body: {noteElement.Body}");

            return View(await _noteRepository.GetAllNotes());
        }

        public IActionResult Init()
        {
            _noteRepository.RemoveAllNotes();
            _noteRepository.AddNote(new Discipline() { Name = "Математичний аналіз, ч.1", ModuleType = ModuleType.Required, ModuleDescr = "загальна кількість годин - 240 (Кредитів: EKTS - 8)..", Lectors = new List<Lector> { new Lector() { Name = "Гладун Володимир Романович", Descr = "доцент, кандидат фіз-мат наук" } }, YearPart = YearPart.Autumn, Course = 1, ControlType=ControlType.Exam ,CreatedOn = DateTime.Now});
            _noteRepository.AddNote(new Discipline() { Name = "Математичний аналіз, ч.1", ModuleType = ModuleType.Required, ModuleDescr = "загальна кількість годин - 240 (Кредитів: EKTS - 8)..", Lectors = new List<Lector> { new Lector() { Name = "Гладун Володимир Романович", Descr = "доцент, кандидат фіз-мат наук" }, new Lector() { Name = "Гнатів Богдан Васильович", Descr = "доцент, кандидат фіз-мат наук" } }, YearPart = YearPart.Autumn, Course = 1, ControlType = ControlType.Exam, CreatedOn = DateTime.Now });


            ViewData["Message"] = string.Format($"Filled in 4 records");
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
