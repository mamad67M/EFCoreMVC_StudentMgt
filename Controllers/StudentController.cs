using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Controllers
{
    public class StudentController : Controller
    {
        public readonly ApplicationDbContexte _dbContexte;
        public StudentController(ApplicationDbContexte dbContext)
        {
            this._dbContexte = dbContext;
        }

        //la list des etudiants
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await _dbContexte.Students.ToListAsync();
            return View(students);
        }

        //Ajouter un etudiant Partie 1 (Get)
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        /*
        Ajouter un etudiant Partie 2 (Post)
        Attention ! ici on passe par l'objet viewModel qui est aussi un objet Etudiant,
        Dans la construction de cet objet on prend toutes les variable sauf Id : c'est EF que le gère
        les deux objets sont identiques sauf que l'un est un viewModel et l'autre est un objet Etudiant
         */
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };
            await _dbContexte.Students.AddAsync(student);
            await _dbContexte.SaveChangesAsync();
            // Redirection vers la liste des étudiants après l'ajout
            return RedirectToAction("List", "Student");
        }

        //editer un etudiant (1)
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        { 
            var student = await _dbContexte.Students.FindAsync(id);
            return View(student);
        }
        // Récupération des données de l'étudiant à partir
        // du formulaire (vue --> 1)  et mise à jour dans la base de données avec la methode POST
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await _dbContexte.Students.FindAsync(viewModel.Id);
            if (student != null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;
                await _dbContexte.SaveChangesAsync();
            }
            return RedirectToAction("List","Student");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
          var student = await _dbContexte.Students.FindAsync(id);
            
          if(student == null)
                {
                    return NotFound();
                }
                _dbContexte.Students.Remove(student);
                await _dbContexte.SaveChangesAsync();
                return RedirectToAction("List", "Student");
        }







        /*
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _dbContexte.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            var studentViewModel = new AddStudentViewModel
            {
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone,
                Subscribed = student.Subscribed
            };
            return View(studentViewModel);
        }
        */
    }
}
