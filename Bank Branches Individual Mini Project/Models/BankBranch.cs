using System.ComponentModel.DataAnnotations;

namespace Bank_Branches_Individual_Mini_Project.Models
{
    public class BankBranch
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string LocationName { get; set; }
        [Url]
        public string LocationURL { get; set; }
        [Required]
        public string BranchManager { get; set; }
        [Required]
        public string BranchName { get; set; }
        [Required]
        public int EmployeeCount { get; set; }
        [Required]
        public List<Employee> Employees { get; set; }

    }
}
