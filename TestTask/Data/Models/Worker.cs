using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestTask.Data.Models;

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
        public int IsHasDriveLicense { get; set; }
        public int DivisionId { get; set; }
        public virtual Division? Division { get; set; }

        public string Gender => ((Gender)this.GenderId).GetDescription();

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
