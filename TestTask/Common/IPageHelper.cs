using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Models;

namespace TestTask.Common
{
    /// <summary>
    /// Интерфейс для группировки методов, которые необходимы для страниц
    /// </summary>
    public interface IPageHelper
    {
        /// <summary>
        /// Возвращает текущее значение ид. выбранного подразделения в сессии по ключу <see cref="SessionKey"/>
        /// </summary>
        /// <param name="httpContext">Контекст запроса</param>
        int GetFilterIdOnSession(HttpContext httpContext);

        /// <summary>
        /// Выставляет новое значение ид. выбранного подразделения в сессии по ключу <see cref="SessionKey"/>
        /// </summary>
        /// <param name="httpContext">Контекст запроса</param>
        /// <param name="divisionId">Идентификатор выбранного подразделения</param>
        void SetFilterIdOnSession(HttpContext httpContext, int divisionId);

        /// <summary>
        /// Преобразует список подразделений в список для отображения на странице
        /// </summary>
        /// <param name="divisions">Список подразделений</param>
        /// <param name="isWithRootItem">True - будет добавлен корневой элемент</param>
        List<SelectListItem> ConvertToSelectList(IEnumerable<Division> divisions, bool isWithRootItem = false);

        /// <summary>
        /// Возвращает все элементы перечисления <see cref="Gender"/> в виде списка
        /// </summary>
        List<SelectListItem> GetGenderListItems();

        /// <summary>
        /// Создает и возвращает сообщение об ошибке для записи в лог
        /// </summary>
        /// <param name="sourceName">Источник возникновения ошибки</param>
        string GetErrorMessage(string sourceName);
    }
}
