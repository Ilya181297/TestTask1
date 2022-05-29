using System.ComponentModel;

namespace TestTask.Models
{
    public enum Gender
    {
        [Description("Мужской")]
        Male = 0,
        [Description("Женский")]
        Female = 1
    }
}