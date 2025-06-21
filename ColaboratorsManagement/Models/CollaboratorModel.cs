namespace ColaboratorsManagement.Models
{
    public class CollaboratorModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int YearsOfExperience { get; set; }
        public string? BetterTechnology { get; set; }
    }
}
