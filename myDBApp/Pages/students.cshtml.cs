using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myDBApp.Data;
using myDBApp.Models;

namespace myDBApp.Pages
{
    public class studentsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public studentsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MyStudent newStudent { get; set; }
        public bool studentExists { get; set; }


        public List<MyStudent> StudentList { get; set; } = new List<MyStudent>();
        //important:
        //create a list of type courseID for exception handling in edit if statement

        public void OnGet()
        {
            StudentList = _context.Student.ToList();//Note:Course(with capital "C") is the name we assigned to DBSet
        }

        public IActionResult OnPost()//38 & 43
        {
            string nonExistingStudent = $"*Error: non-existing course can not be updated.";
            string invalidRegistrationStatus = $"*Error: Registration Status can only contain 'True' or 'False'.";

            // Action will either be add, edit or update based on the form data
            if (Request.Form["action"] == "edit")
            {
                var OriginalId = Convert.ToInt32(Request.Form["ID"]);//keeps the original row id. Use it for exception handling
                                                                     //and see if it could update the courseId value

                MyStudent recent = new MyStudent();
                var studentId = newStudent.studentId; // Original courseID
                var student = _context.Student.Find(studentId);
                string existingStudent = $"*Error: The studentId '{studentId}' already exists in the table.";
                try
                {
                    if (student != null)
                    {
                        studentExists = true;

                        if (student.studentId != OriginalId)
                        {
                            throw new Exception(existingStudent);
                        }
                        else
                        {
                            student.studentName = newStudent.studentName;//although coursename & Credits adapts to new value that newCourse.courseName
                                                                         //receives from the AddRow Form, courseId will not change here because it is set as primary key in MyCourse
                                                                         //unless you decide to set your model as .HasNoKey();
                            student.dateOfBirth = newStudent.dateOfBirth;
                            student.registered = newStudent.registered;
                            student.deptName = newStudent.deptName;
                            _context.Student.Update(student);
                            _context.SaveChanges();
                            OnGet();

                            return RedirectToPage();
                        }

                    }
                    else
                    {
                        throw new Exception(nonExistingStudent);
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
                var studentId = Convert.ToInt32(Request.Form["studentId"]);
                var student = _context.Student.Find(studentId);
                if (student != null)
                {
                    _context.Student.Remove(student);
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
                var targetId = newStudent.studentId;
                var student = _context.Student.Find(targetId);
                string existingStudent = $"*Error:A course with ID '{newStudent.studentId}' already exists in the table.";

                // Add a new course
                try
                {
                    if(ModelState.IsValid)
                    {
                        if(student!=null)
                        {
                            throw new Exception(existingStudent);
                        }
                        else
                        {
                            _context.Student.Add(newStudent);
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
