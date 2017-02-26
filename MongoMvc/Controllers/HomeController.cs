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
            _noteRepository.AddNote(new Note() {  Body = "Test note 1", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 1 });
            _noteRepository.AddNote(new Note() {  Body = "Test note 2", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 1 });
            _noteRepository.AddNote(new Note() {  Body = "Test note 3", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 2 });
            _noteRepository.AddNote(new Note() {  Body = "Test note 4", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 2 });

            ViewData["Message"] = string.Format($"Filled in 4 records");
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
