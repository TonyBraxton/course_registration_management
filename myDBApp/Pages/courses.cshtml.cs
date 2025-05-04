using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myDBApp.Data;
using myDBApp.Models;
using MySqlX.XDevAPI.Common;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace myDBApp.Pages
{
    public class coursesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public coursesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MyCourse newCourse { get; set; }
        public bool CourseExists { get; set; }


        public List<MyCourse> courseList { get; set; } = new List<MyCourse>();
        //important:
        //create a list of type courseID for exception handling in edit if statement

        public void OnGet()
        {
            courseList = _context.Course.ToList();//Note:Course(with capital "C") is the name we assigned to DBSet
        }

        public IActionResult OnPost()//38 & 43
        {
            string invalidCredits = "*Error:Invalid value entered for Credits. Credits column can only receive a number as input." + " ";
            string invalidCourseId = "*Error:Invalid Course ID. Course ID column can only receive a number as input." + " ";
            string invalidCourseName = "*Error:Course Name can only contain letters." + " ";
            string nonExistingCourse = $"*Error: non-existing course can not be updated.";

            // Action will either be add, edit or update based on the form data
            if (Request.Form["action"] == "edit")
            {
                var OriginalId = Convert.ToInt32(Request.Form["ID"]);//keeps the original row id. Use it for exception handling
                                                                    //and see if it could update the courseId value

                //var modifiedId = newCourse.ID;
                MyCourse recent = new MyCourse();
                var courseId = newCourse.courseId; // Original courseID
                var course = _context.Course.Find(courseId);
                string existingCourse = $"*Error: The courseId '{courseId}' already exists in the table.";
                try
                {//instead of this while loop i coud've used while(modelState.isvalid) to confirm inputs based non empty fields 
                 //and variable type(datatype) of MyCourse properties(int courseID, string courseName and int credits)
                    if (course != null)
                    {
                        CourseExists = true;


                        if (course.courseId != OriginalId)
                        {
                            throw new Exception(existingCourse);
                        }
                        else
                        {
                            course.courseName = newCourse.courseName;//although coursename & Credits adapts to new value that newCourse.courseName
                                                                     //receives from the AddRow Form, courseId will not change here because it is set as primary key in MyCourse
                                                                     //unless you decide to set your model as .HasNoKey();
                            course.credits = newCourse.credits;
                            _context.Course.Update(course);
                            _context.SaveChanges();
                            OnGet();

                            return RedirectToPage();
                        }
                        
                    }
                    else
                    {
                        throw new Exception(nonExistingCourse);
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
                var courseId = Convert.ToInt32(Request.Form["courseID"]);
                var course = _context.Course.Find(courseId);
                string userConfirmDelete = Request.Form["hiddenDelete"].ToString();
                if (course != null)
                {
                    _context.Course.Remove(course);
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
                var targetId = newCourse.courseId;
                var course = _context.Course.Find(targetId);
                string existingCourse = $"*Error:A course with ID '{newCourse.courseId}' already exists in the table.";

                // Add a new course
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (course != null)
                        {
                            throw new Exception(existingCourse);
                        }
                        else
                        {
                            _context.Course.Add(newCourse);
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