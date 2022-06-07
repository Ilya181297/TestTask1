using System.ComponentModel.DataAnnotations;
using TestTask.Data;

namespace TestTask.Models
{
    /// <summary>
    /// Сущность подразделения
    /// </summary>
    public class Division : IDbEntity
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Division()
        {
            Children = new HashSet<Division>();
            Workers = new HashSet<Worker>();
        }

        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Дата формирования
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime FormationDate { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Идентификатор родительского подразделения
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Родительское подразделение
        /// </summary>
        public virtual Division? Parent { get; set; }

        /// <summary>
        /// Список дочерних подразделений
        /// </summary>
        public virtual ICollection<Division> Children { get; set; }

        /// <summary>
        /// Список работников входящих в данное подразделение
        /// </summary>
        public virtual ICollection<Worker> Workers { get; set; }

        /// <summary>
        /// Проверяет является ли переданное подразделение родительским для текущего
        /// </summary>
        /// <param name="divisionId">Идентификатор подразделения</param>
        public bool HasParent(int divisionId)
        {
            if (ParentId == divisionId)
                return true;

            return Parent is not null && Parent.HasParent(divisionId);
        }
    }
}
