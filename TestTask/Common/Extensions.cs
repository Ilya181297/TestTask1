using System.ComponentModel;
using System.Reflection;

namespace TestTask.Common
{
    /// <summary>
    /// Класс расширения объектов
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Возвращает значение из атрибута Description при его наличии
        /// </summary>
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute != null ? attribute.Description : value.ToString();
        }
    }
}
