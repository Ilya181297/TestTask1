using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Models;

namespace TestTask.Pages
{
    public static class PageHelper
    {
        private static readonly SelectListItem _rootItem = new SelectListItem { Value = "0", Text = "Корневой" };
        public static List<SelectListItem> ConvertToSelectList(IEnumerable<Division> divisions, bool withRootItem = false)
        {
            var listItems = divisions.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            if (withRootItem)
                listItems.Insert(0, _rootItem);

            return listItems;
        }
        public static List<SelectListItem> GetGenderListItems()
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
    }
}
