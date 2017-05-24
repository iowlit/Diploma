using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoMvc.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoMvc.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace MongoMvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IDisciplineRepository _DisciplineRepository;
        private readonly ILecturerRepository _LecturerRepository;

        public HomeController(IDisciplineRepository DisciplineRepository, ILecturerRepository LecturerRepository)
        {
            _DisciplineRepository = DisciplineRepository;
            _LecturerRepository = LecturerRepository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Read(int? page)
        {
            var dummyItems = await _DisciplineRepository.GetAllAsync();
            var pager = new Pager(dummyItems.Count(), page);

            var viewModel = new DisciplinePagination
            {
                Disciplines = dummyItems.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager
            };

            return View(viewModel);            
        }        

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Lectures = new MultiSelectList(_LecturerRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Discipline disc, string[] Id)
        {
            disc.Lectors = _LecturerRepository.GetLectorsByArray(Id);                                             
            await _DisciplineRepository.AddAsync(disc);
            ViewBag.Lectures = null;
            return RedirectToAction("Read");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null)
            {
                return new BadRequestResult();
            }
            Discipline disc = await _DisciplineRepository.GetByIdAsync(id);
            if(disc == null)
            {
                return new NotFoundResult();
            }
            ViewBag.Lectures = new MultiSelectList(_LecturerRepository.GetAll(), "Id", "Name");
            return View(disc);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Discipline disc, string[] Id)
        {
            disc.Lectors = _LecturerRepository.GetLectorsByArray(Id);
            await _DisciplineRepository.UpdateAsync(disc.Id, disc);
            ViewBag.Lectures = null;
            return RedirectToAction("Read");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            Discipline disc = await _DisciplineRepository.GetByIdAsync(id);
            if (disc == null)
            {
                return new NotFoundResult();
            }            
            return View(disc);
        }

        [HttpPost, ActionName("Delete")]       
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            await _DisciplineRepository.RemoveByIdAsync(id);           
            return RedirectToAction("Read");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Course(int id)
        {
            var dcs = await _DisciplineRepository.GetByCourseAsync(id);
            if (dcs == null)
            {
                return new NotFoundResult();
            }
            return View(dcs);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Discipline(string id)
        {
            var dcs = await _DisciplineRepository.GetByIdAsync(id);
            if (dcs == null)
            {
                return new NotFoundResult();
            }
            
            return View(dcs);
        }
    }
}
