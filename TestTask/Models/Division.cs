using System.ComponentModel.DataAnnotations;

namespace TestTask.Models
{
    public class Division
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateOfFormation { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
