using System.ComponentModel;

namespace TestTask.Common
{
    /// <summary>
    /// Перечисление пола роботника
    /// </summary>
    public enum Gender
    {
        [Description("Мужской")]
        Male = 0,
        [Description("Женский")]
        Female = 1
    }
}