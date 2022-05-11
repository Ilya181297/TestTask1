using System.ComponentModel.DataAnnotations;

namespace TestTask.Models
{
    public class Worker
    {
        public int ID { get; set; }
        public string Surname { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string Post { get; set; } = string.Empty;
        public bool DrivingLicense { get; set; }
        public int DivisionId { get; set; }
        public virtual Division? Division { get; set; }

        public string GenderStr => ((Gender)this.Gender).GetString();

    }
}
