namespace LibraryERD.Domain
{
    public class Employees
    {
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int ManagerId { get; set; }
        public Employees? Manager { get; set; }
    }
}
