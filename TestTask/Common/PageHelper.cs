using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Models;

namespace TestTask.Common
{
    /// <summary>
    /// Класс для группировки методов, которые необходимы для страниц
    /// </summary>
    public class PageHelper
    {
        private const string SessionKey = "SelectedDivisionId";

        /// <summary>
        /// Возвращает текущее значение ид. выбранного подразделения в сессии по ключу <see cref="SessionKey"/>
        /// </summary>
        /// <param name="httpContext">Контекст запроса</param>
        public int GetFilterIdOnSession(HttpContext httpContext)
        {
            var session = httpContext?.Session;
            if (session is null)
                return default;

            return session.GetInt32(SessionKey) ?? 0;
        }

        /// <summary>
        /// Выставляет новое значение ид. выбранного подразделения в сессии по ключу <see cref="SessionKey"/>
        /// </summary>
        /// <param name="httpContext">Контекст запроса</param>
        /// <param name="divisionId">Идентификатор выбранного подразделения</param>
        public void SetFilterIdOnSession(HttpContext httpContext, int divisionId)
        {
            var session = httpContext?.Session;
            if (session is null)
                return;

            session.SetInt32(SessionKey, divisionId);
        }

        /// <summary>
        /// Корневой элемент списка
        /// </summary>
        private readonly SelectListItem _rootItem = new SelectListItem { Value = "0", Text = "Корневой" };

        /// <summary>
        /// Преобразует список подразделений в список для отображения на странице
        /// </summary>
        /// <param name="divisions">Список подразделений</param>
        /// <param name="isWithRootItem">True - будет добавлен корневой элемент</param>
        public List<SelectListItem> ConvertToSelectList(IEnumerable<Division> divisions, bool isWithRootItem = false)
        {
            var listItems = divisions.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            if (isWithRootItem)
                listItems.Insert(0, _rootItem);

            return listItems;
        }

        /// <summary>
        /// Возвращает все элементы перечисления <see cref="Gender"/> в виде списка
        /// </summary>
        public List<SelectListItem> GetGenderListItems()
        {
            return Enum.GetValues(typeof(Gender))
                    .Cast<Gender>()
                    .Select(v => new SelectListItem
                    {
                        Value = ((int)v).ToString(),
                        Text = v.GetDescription()
                    })
                    .ToList();
        }

        /// <summary>
        /// Создает и возвращает сообщение об ошибке для записи в лог
        /// </summary>
        /// <param name="sourceName">Источник возникновения ошибки</param>
        public string GetErrorMessage(string sourceName) => $"An error occurred in the method {sourceName}";
    }
}
