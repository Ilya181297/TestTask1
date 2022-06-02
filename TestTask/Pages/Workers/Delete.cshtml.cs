#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Common;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Workers
{
    /// <summary>
    /// Страница для подтверждения удаления работника
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
        /// Полученная модель работника
        /// </summary>
        [BindProperty]
        public Worker Worker { get; set; }

        /// <summary>
        /// Возвращает страницу с заполненным работником в соответсвтии с идентификатором
        /// </summary>
        /// <param name="id">Идентификатор работника</param>
        public IActionResult OnGet(int id)
        {
            try
            {
                Worker = _testTaskService.GetWorker(id);

                if (Worker is null)
                    return NotFound($"Worker with Id={id} is not found");

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _pageHelper.GetErrorMessage("Workers/Delete/OnGet"));

                return Page();
            }
        }

        /// <summary>
        /// Удаление работника в соответсвтии с идентификтором
        /// </summary>
        /// <param name="id">Идентификатор работника</param>
        public IActionResult OnPost(int id)
        {
            try
            {
                _testTaskService.DeleteWorker(id);

                return RedirectToPage("../Index", new { id = _pageHelper.GetFilterIdOnSession(HttpContext) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _pageHelper.GetErrorMessage("Workers/Index/OnPost"));

                return Page();
            }
        }
    }
}
