#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Common;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Divisions
{
    /// <summary>
    /// Страница детального просмотра подразделения
    /// </summary>
    public class DetailsModel : PageModel
    {
        private readonly ITestTaskService _testTaskService;
        private readonly ILogger<DetailsModel> _logger;
        private readonly IPageHelper _pageHelper;

        /// <summary>
        /// Конструктор страницы
        /// </summary>
        /// <param name="testTaskService">Сервис для работы с подразделениями и сотрудниками</param>
        /// <param name="logger">Логер</param>
        public DetailsModel(ITestTaskService testTaskService, ILogger<DetailsModel> logger, IPageHelper pageHelper)
        {
            _testTaskService = testTaskService;
            _logger = logger;
            _pageHelper = pageHelper;
        }

        /// <summary>
        /// Полученная модель подразделения
        /// </summary>
        public Division Division { get; set; }

        /// <summary>
        /// Возвращает страницу с заполненным подразделением в соответсвтии с идентификатором
        /// </summary>
        /// <param name="id">Идентификатор подразделения</param>
        public IActionResult OnGet(int id)
        {
            try
            {
                Division = _testTaskService.GetDivision(id);

                if (Division is null)
                    return NotFound($"Division with Id={id} is not found");

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _pageHelper.GetErrorMessage("Divisions/Details/OnGet"));

                return Page();
            }
        }
    }
}
