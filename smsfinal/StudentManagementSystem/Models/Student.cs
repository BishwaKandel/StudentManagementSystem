namespace StudentManagementSystem.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateofBirth { get; set; } 
        public string Email { get; set; } = null!;

        //Navigation to Enrollment
        public ICollection<Enrollment>? Enrollments { get; set; } = null!;
    }
}
