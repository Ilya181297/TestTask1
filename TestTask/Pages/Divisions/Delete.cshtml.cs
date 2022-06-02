#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Common;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Divisions
{
    /// <summary>
    /// Страница для подтверждения удаления подразделения
    /// </summary>
    public class DeleteModel : PageModel
    {
        private readonly ITestTaskService _testTaskService;
        private readonly ILogger<DeleteModel> _logger;
        public readonly PageHelper _pageHelper;

        /// <summary>
        /// Конструктор страницы
        /// </summary>
        /// <param name="testTaskService">Сервис для работы с подразделениями и сотрудниками</param>
        /// <param name="logger">Логер</param>
        public DeleteModel(ITestTaskService testTaskService, ILogger<DeleteModel> logger)
        {
            _testTaskService = testTaskService;
            _logger = logger;
            _pageHelper = new PageHelper();
        }

        /// <summary>
        /// Полученная модель подразделения
        /// </summary>
        [BindProperty]
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
                _logger.LogError(ex, _pageHelper.GetErrorMessage("Divisions/Delete/OnGet"));

                return Page();
            }
        }

        /// <summary>
        /// Удаление подразделения в соответсвтии с идентификтором
        /// </summary>
        /// <param name="id">Идентификатор подразделения</param>
        public IActionResult OnPost(int id)
        {
            try
            {
                _testTaskService.DeleteDivision(id);

                return RedirectToPage("../Index", new { id = _pageHelper.GetFilterIdOnSession(HttpContext) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _pageHelper.GetErrorMessage("Divisions/Delete OnPost"));

                return Page();
            }
        }
    }
}
