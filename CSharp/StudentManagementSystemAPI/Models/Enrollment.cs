using System.Text.Json.Serialization;

namespace StudentManagementSystemAPI.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        // StudentID - Foreign Key from Student ; 1-M relationship
        public int StudentID { get; set; }

        // CourseID - Foreign Key from Course - 1-M Relationship 
        public int CourseID { get; set; }

        //Navigation Back to Student
        public Student Student { get; set; } = null!;

        //Navigation Back to Course
        public Course Course { get; set; } = null!;


        public DateTime EnrollmentDate { get; set; }


    }
}
