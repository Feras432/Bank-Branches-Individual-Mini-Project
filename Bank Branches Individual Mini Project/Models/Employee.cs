using System.ComponentModel.DataAnnotations;

namespace Bank_Branches_Individual_Mini_Project.Models
{
    public class Employee
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CivilId { get; set; }
        [Required]
        public string Position { get; set;}
        [Required]
        public BankBranch BankBranch { get; set; }
    }
}
