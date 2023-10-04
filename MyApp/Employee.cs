using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyApp;

public class Employee
{
    public enum Genders {
        [Description("Male")]
        Male,
        [Description("Female")]
        Female
    }
    [Key]
    public int Id { get; set; }
    public string FullName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Genders Gender { get; set; }
    public int FindAge(DateOnly dateOfBirth)
    {
       var now = DateOnly.FromDateTime(DateTime.Now);
        var age = now.Year - dateOfBirth.Year;
        if (dateOfBirth > now.AddYears(-age)) age--;
        return age;
    }

    public Employee()
    {
        
    }
    public Employee(string fullName, DateOnly dateOfBirth, Genders gender)
    {
        FullName = fullName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
    }
}