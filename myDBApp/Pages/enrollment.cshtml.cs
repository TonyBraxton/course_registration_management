using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myDBApp.Data;
using myDBApp.Models;

namespace myDBApp.Pages
{
    public class enrollmentsModel : PageModel
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
            string invaliddateOfBirth = "*Error:Invalid value entered for Credits. Credits column can only receive a number as input." + " ";
            string invalidStudentId = "*Error:Invalid Student ID. Course ID column can only receive a number as input." + " ";
            string invalidDateOfBirth = "*Error:Invalid date of birth. yyyy-mm-dd is the only acceptable format." + " ";
            string invalidStudentName = "*Error:Student Name can only contain letters." + " ";
            string nonExistingEnrollment = $"*Error: non-existing course can not be updated.";
            string invalidRegistrationStatus = $"*Error: Registration Status can only contain 'True' or 'False'.";
            string invalidDeptName = "*Error:Department Name can only contain letters.";

            // Action will either be add, edit or update based on the form data
            if (Request.Form["action"] == "edit")
            {
                var OriginalStudentId = Convert.ToInt32(Request.Form["ID"]);//keeps the original row id. Use it for exception handling
                                                                     //and see if it could update the courseId value

                MyStudent recent = new MyStudent();
                var studentId = newEnrollment.studentId; // Original courseID
                var courseId = newEnrollment.courseId;
                var course = _context.Course.Find(courseId);
                var student = _context.Student.Find(studentId);
                string existingStudent = $"*Error: The studentId '{studentId}' already exists in the table.";
                try
                {
                    if (student != null && course!=null)
                    {
                        enrollmentExists = true;
                        var enrollmentId = studentId + courseId;
                        var enrollment = _context.Enrollment.Find(enrollmentId);

                        if (student.studentId == OriginalStudentId && course.courseId==origincalCourseId)
                        {
                            enrollment.grade = newEnrollment.grade;//although coursename & Credits adapts to new value that newCourse.courseName
                                                                   //receives from the AddRow Form, courseId will not change here because it is set as primary key in MyCourse
                                                                   //unless you decide to set your model as .HasNoKey();
                            _context.Student.Update(enrollment);
                            _context.SaveChanges();
                            OnGet();

                            return RedirectToPage();
                        }
                        else
                        {
                            
                        }

                    }
                    else
                    {
                        throw new Exception(nonExistingEnrollment);
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
                    if (ModelState.IsValid)
                    {
                        if (student != null)
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
