using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoMvc.Repository;
using MongoMvc.Model;
using Microsoft.AspNetCore.Authorization;

namespace MongoMvc.Controllers
{
    [Authorize]
    public class LecturerController : Controller
    {        
        private readonly ILecturerRepository _LecturerRepository;
        private readonly IDisciplineRepository _DisciplineRepository;

        public LecturerController(ILecturerRepository LecturerRepository, IDisciplineRepository DisciplineRepository)
        {            
            _LecturerRepository = LecturerRepository;
            _DisciplineRepository = DisciplineRepository;
        }
               

        public async Task<IActionResult> Read()
        {          
            return View(await _LecturerRepository.GetAllAsync());
        }        

        [HttpGet]
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Lecturer lc)
        {                                                         
            await _LecturerRepository.AddAsync(lc);            
            return RedirectToAction("Read");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null)
            {
                return new BadRequestResult();
            }
            Lecturer lc = await _LecturerRepository.GetByIdAsync(id);
            if(lc == null)
            {
                return new NotFoundResult();
            }            
            return View(lc);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Lecturer lc)
        {            
            await _LecturerRepository.UpdateAsync(lc.Id, lc);            
            return RedirectToAction("Read");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            Lecturer lc = await _LecturerRepository.GetByIdAsync(id);
            if (lc == null)
            {
                return new NotFoundResult();
            }            
            return View(lc);
        }

        [HttpPost, ActionName("Delete")]       
        public async Task<IActionResult> ConfirmDelete(string id, bool check = false)
        {
            var lc = _LecturerRepository.GetById(id);
            if (check)
            {
                _DisciplineRepository.RemoveLecturerAsync(id, lc);
                
            }
            await _LecturerRepository.RemoveByIdAsync(id);
            return RedirectToAction("Read");
        }        
    }
}
