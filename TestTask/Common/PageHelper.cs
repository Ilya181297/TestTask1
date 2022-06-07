using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Models;

namespace TestTask.Common
{
    /// <summary>
    /// Класс для группировки методов, которые необходимы для страниц
    /// </summary>
    public class PageHelper : IPageHelper
    {
        private const string SessionKey = "SelectedDivisionId";

        public int GetFilterIdOnSession(HttpContext httpContext)
        {
            var session = httpContext?.Session;
            if (session is null)
                return default;

            return session.GetInt32(SessionKey) ?? 0;
        }

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

        public string GetErrorMessage(string sourceName) => $"An error occurred in the method {sourceName}";
    }
}
