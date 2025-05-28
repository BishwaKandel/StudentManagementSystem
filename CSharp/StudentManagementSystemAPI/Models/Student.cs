namespace StudentManagementSystemAPI.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public DateTime DateofBirth { get; set; }
        public string Email { get; set; }
        
        //Navigation to Enrollment
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
