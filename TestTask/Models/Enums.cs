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

    public static class GenderExtensions
    {
        public static string GetString(this Gender gender)
        {
            return gender switch
            {
                Gender.Male => "Мужской",
                Gender.Female => "Женский",
                _ => "",
            };
        }
    }
}