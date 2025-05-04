using System.ComponentModel.DataAnnotations;

namespace myDBApp.Models
{
    public class MyEnrollment
    {
        public int studentId { get; set; }
        public int courseId { get; set; }
        public string? grade { get; set; }

        
    }
}
