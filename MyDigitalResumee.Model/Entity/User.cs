using MyDigitalResumee.Model.Enum;

namespace MyDigitalResumee.Model.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public GenderEnum Gender { get; set; }
        public string? Cpf { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string? Office { get; set; }
        public double? SalaryClaim { get; set; }
    }
}