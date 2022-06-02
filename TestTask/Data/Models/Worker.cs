using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestTask.Common;
using TestTask.Data;

namespace TestTask.Models
{
    /// <summary>
    /// Модель работника
    /// </summary>
    public class Worker : IDbEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Имя 
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Отчетсво
        /// </summary>
        public string? MiddleName { get; set; }

        /// <summary>
        /// Дата рождения 
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public int GenderId { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string Post { get; set; } = string.Empty;

        /// <summary>
        /// Наличие водительских прав. 0 - нет, 1 - есть
        /// </summary>
        public int IsHasDriveLicense { get; set; }

        /// <summary>
        /// Идентификатор подразделения
        /// </summary>
        public int DivisionId { get; set; }

        /// <summary>
        /// Модель подразделения. Навигационное свойство
        /// </summary>
        public virtual Division? Division { get; set; }

        /// <summary>
        /// Пол в строковом представлении. <see cref="Models.Gender"/>
        /// </summary>
        public string Gender => ((Gender)this.GenderId).GetDescription();

        /// <summary>
        /// Наличие водительских прав в булевом представлении. False - нет, True - есть
        /// </summary>
        [NotMapped]
        public bool IsHasDriveLicenseBool
        {
            get { return IsHasDriveLicense == 1; }
            set { IsHasDriveLicense = value ? 1 : 0; }
        }

        /// <summary>
        /// Проверяет входит ли данный работник в подразделение 
        /// или в какой-либо из дочерних этого подразделения
        /// </summary>
        /// <param name="divisionId">Идентификатор подразделения</param>
        public bool IsIncludeInDivision(int divisionId) => Division != null
            && (DivisionId == divisionId || Division.HasParent(divisionId));
    }
}
