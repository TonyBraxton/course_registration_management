using System.ComponentModel.DataAnnotations;

namespace myDBApp.Models
{
    public class MyCourse
    {
        [Key]
        public int? courseId { get; set; }
        [StringLength(100)]
        public string courseName { get; set; } //binding usually apply to setting attribute as[key}, setting it as [Required],
                                                //setting a [Range],setting a [MaxLength].
        public int? credits { get; set; }

        public bool IsString(string input)
        {

            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }
        
    }
}
