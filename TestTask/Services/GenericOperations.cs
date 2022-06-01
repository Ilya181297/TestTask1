using Microsoft.EntityFrameworkCore.ChangeTracking;
using TestTask.Data;

namespace TestTask.Services
{
    /// <summary>
    /// Реализация полная фигня
    /// Зато теперь Илья знаком с дженериками
    /// </summary>
    /// <typeparam name="T">Принимаемый тип</typeparam>
    public class GenericOperations<T> where T : IDbEntity
    {
        private readonly TestTaskContext _context;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст для работы с бд</param>
        /// <exception cref="ArgumentNullException">context is null</exception>
        public GenericOperations(TestTaskContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Сохранение
        /// </summary>
        /// <param name="item">Сохраняемая модель</param>
        /// <param name="updateModelInCache">Метод обновления в кэше</param>
        /// <param name="addToDictionary">Метод добавления в кэш</param>
        /// <param name="addToDbSet">Функция добавления модели в контекст</param>
        public void Save(T item, Action<T> updateModelInCache
            , Action<int, T> addToDictionary, Func<T, EntityEntry> addToDbSet)
        {
            if (item.Id == 0)
                Add(item, addToDictionary, addToDbSet);
            else
                Edit(item, updateModelInCache);
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="item">Удаляемая модель</param>
        /// <param name="removeFromDbSet">Функция удаления сущности из контекста</param>
        /// <param name="removeFromDict">Функция удаления модели из кэша</param>
        public void Delete(T item, Func<T, EntityEntry> removeFromDbSet, Func<int, bool> removeFromDict)
        {
            removeFromDbSet?.Invoke(item);
            _context.SaveChanges();
            removeFromDict?.Invoke(item.Id);
        }

        private void Add(T item, Action<int, T> addToDictionary, Func<T, EntityEntry> addToDbSet)
        {
            addToDbSet?.Invoke(item);
            _context.SaveChanges();
            addToDictionary?.Invoke(item.Id, item);
        }

        private void Edit(T item, Action<T> updateModelInCache)
        {
            updateModelInCache?.Invoke(item);
            _context.SaveChanges();
        }
    }
}
