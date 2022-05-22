using System.ComponentModel;
using System.Reflection;

namespace TestTask.Models
{
    public enum Gender
    {
        [Description("Мужской")]
        Male = 0,
        [Description("Женский")]
        Female = 1
    }

    public static class GenderExtensions
    {
        public static string GetDescription(this Gender gender)
        {
            var field = gender.GetType().GetField(gender.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute != null ? attribute.Description : gender.ToString();
        }
    }
}