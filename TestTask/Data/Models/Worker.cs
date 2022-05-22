using System.ComponentModel.DataAnnotations;

namespace TestTask.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string Surname { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? MiddleName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        public string Post { get; set; } = string.Empty;
        public bool IsHasDriveLicense { get; set; }
        public int DivisionId { get; set; }
        public virtual Division? Division { get; set; }

        public string Gender => ((Gender)this.GenderId).GetDescription();
    }
}
