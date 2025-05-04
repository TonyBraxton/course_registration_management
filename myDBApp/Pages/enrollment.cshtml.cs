using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myDBApp.Data;
using myDBApp.Models;
using System.Diagnostics.Eventing.Reader;

namespace myDBApp.Pages
{
    public class enrollmentsModel: PageModel
    {
        private readonly ApplicationDbContext _context;
        public enrollmentsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MyEnrollment newEnrollment { get; set; }
        public bool enrollmentExists { get; set; }


        public List<MyEnrollment> EnrollmentList { get; set; } = new List<MyEnrollment>();
        //important:
        //create a list of type courseID for exception handling in edit if statement

        public void OnGet()
        {
            EnrollmentList = _context.Enrollment.ToList();//Note:Course(with capital "C") is the name we assigned to DBSet
        }

        public IActionResult OnPost()//38 & 43
        {

            // Action will either be add, edit or update based on the form data
            if (Request.Form["action"] == "edit")
            {
                var OriginalStudentId = Convert.ToInt32(Request.Form["studentID"]);//keeps the original row id. Use it for exception handling
                                                                     //and see if it could update the courseId value
                var OriginalCourseId = Convert.ToInt32(Request.Form["courseID"]);

                //MyEnrollment recent = new MyEnrollment();
                var studentId = newEnrollment.studentId; // Original courseID
                var courseId = newEnrollment.courseId;
                var enrollment = _context.Enrollment.Find(studentId, courseId);
                string existingEnrolllment = $"*Error: The enrollment '{studentId}', '{courseId}' already exists in the table.";
                try
                {
                    if (enrollment!=null)
                    {
                        enrollmentExists = true;

                        if (enrollment.studentId != OriginalStudentId || enrollment.courseId != OriginalCourseId)
                        {
                            throw new Exception(existingEnrolllment);
                        }
                        else
                        {
                            enrollment.grade = newEnrollment.grade;//although coursename & Credits adapts to new value that newCourse.courseName
                                                                   //receives from the AddRow Form, courseId will not change here because it is set as primary key in MyCourse
                                                                   //unless you decide to set your model as .HasNoKey();
                            _context.Enrollment.Update(enrollment);
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
                var studentId = newEnrollment.studentId;
                var courseId = newEnrollment.courseId;
                var enrollment = _context.Enrollment.Find(studentId, courseId);
                string ID = $"*Error: The student ID and course ID cannot be updated from the enrollment page.";


                // Add a new course
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (enrollment != null)
                        {
                            _context.Enrollment.Add(newEnrollment);
                            _context.SaveChanges();
                            OnGet();
                            return RedirectToPage();
                        }
                        else
                        {
                            throw new Exception(ID);
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
