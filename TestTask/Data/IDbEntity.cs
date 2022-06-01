namespace TestTask.Data
{
    /// <summary>
    /// Интерфейсом помечаем сущности базы данных
    /// </summary>
    public interface IDbEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; }
    }
}
