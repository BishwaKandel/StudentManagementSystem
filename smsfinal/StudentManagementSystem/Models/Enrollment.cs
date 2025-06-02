using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        // StudentID - Foreign Key from Student ; 1-M relationship
        [Required(ErrorMessage = "Please select a student")]
        [Display(Name = "Student")]
        [ForeignKey("Student")]
        public int StudentID { get; set; }

        // CourseID - Foreign Key from Course - 1-M Relationship 
        [Required(ErrorMessage = "Please select a course")]
        [Display(Name = "Course")]
        [ForeignKey("Course")]
        public int CourseID { get; set; }

        //Navigation Back to Student
        public virtual Student? Student { get; set; }

        //Navigation Back to Course
        public virtual Course? Course { get; set; }

        [Required(ErrorMessage = "Please select an enrollment date")]
        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }
}
