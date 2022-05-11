using System.ComponentModel.DataAnnotations;

namespace TestTask.Models
{
    public class Division
    {
        public Division()
        {
            Children = new HashSet<Division>();
            Workers = new HashSet<Worker>();
        }

        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateOfFormation { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public virtual Division? Parent { get; set; }
        public virtual ICollection<Division> Children { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
    }
}
