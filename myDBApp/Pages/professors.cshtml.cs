using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myDBApp.Data;
using myDBApp.Models;

namespace myDBApp.Pages
{
    public class professorsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public professorsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MyProfessor newProfessor { get; set; }
        public bool professorExists { get; set; }


        public List<MyProfessor> ProfessorList { get; set; } = new List<MyProfessor>();
        //important:
        //create a list of type courseID for exception handling in edit if statement

        public void OnGet()
        {
            ProfessorList = _context.Professor.ToList();//Note:Course(with capital "C") is the name we assigned to DBSet
        }

        public IActionResult OnPost()//38 & 43
        {
            string invaliddateOfBirth = "*Error:Invalid value entered for Credits. Credits column can only receive a number as input." + " ";
            string invalidStudentId = "*Error:Invalid Student ID. Course ID column can only receive a number as input." + " ";
            string invalidDateOfBirth = "*Error:Invalid date of birth. yyyy-mm-dd is the only acceptable format." + " ";
            string invalidStudentName = "*Error:Student Name can only contain letters." + " ";
            string nonExistingProfessor = $"*Error: non-existing course can not be updated.";
            string invalidRegistrationStatus = $"*Error: Registration Status can only contain 'True' or 'False'.";
            string invalidDeptName = "*Error:Department Name can only contain letters.";

            // Action will either be add, edit or update based on the form data
            if (Request.Form["action"] == "edit")
            {
                var OriginalId = Convert.ToInt32(Request.Form["ID"]);//keeps the original row id. Use it for exception handling
                                                                     //and see if it could update the courseId value

                MyProfessor recent = new MyProfessor();
                var professorId = newProfessor.professorId; // Original courseID
                var professor = _context.Professor.Find(professorId);
                string existingProfessor = $"*Error: The studentId '{professorId}' already exists in the table.";
                try
                {
                    if (professor != null)
                    {
                        professorExists = true;

                        if (professor.professorId!= OriginalId)
                        {
                            throw new Exception(existingProfessor);
                        }
                        else
                        {
                            professor.professorName = newProfessor.professorName;//although coursename & Credits adapts to new value that newCourse.courseName
                                                                         //receives from the AddRow Form, courseId will not change here because it is set as primary key in MyCourse
                                                                         //unless you decide to set your model as .HasNoKey();
                            professor.deptName= newProfessor.deptName;
                            _context.Professor.Update(professor);
                            _context.SaveChanges();
                            OnGet();

                            return RedirectToPage();
                        }

                    }
                    else
                    {
                        throw new Exception(nonExistingProfessor);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    OnGet();
                    return Page();
                }
            }

            else if (Request.Form["action"] == "delete")
            {
                var professorId = Convert.ToInt32(Request.Form["professorId"]);
                var professor = _context.Professor.Find(professorId);
                if (professor != null)
                {
                    _context.Professor.Remove(professor);
                    _context.SaveChanges();
                    OnGet();
                    return RedirectToPage();
                }
                else
                {
                    OnGet();
                    return Page();
                }
            }
            else
            {
                var targetId = newProfessor.professorId;
                var professor = _context.Professor.Find(targetId);
                string existingProfessor = $"*Error:A course with ID '{newProfessor.professorId}' already exists in the table.";

                // Add a new course
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (professor != null)
                        {
                            throw new Exception(existingProfessor);
                        }
                        else
                        {
                            _context.Professor.Add(newProfessor);
                            _context.SaveChanges();
                            OnGet();
                            return RedirectToPage();

                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    OnGet();
                    return Page();
                }

            }

            return Page();
        }
    }
}
