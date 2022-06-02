#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Common;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Workers
{
    /// <summary>
    /// Страница просмотра работника
    /// </summary>
    public class DetailsModel : PageModel
    {
        private readonly ITestTaskService _companyService;
        private readonly ILogger<DetailsModel> _logger;
        private readonly PageHelper _pageHelper;

        /// <summary>
        /// Конструктор страницы
        /// </summary>
        /// <param name="testTaskService">Сервис для работы с подразделениями и сотрудниками</param>
        /// <param name="logger">Логер</param>
        public DetailsModel(ITestTaskService companyService, ILogger<DetailsModel> logger)
        {
            _companyService = companyService;
            _logger = logger;
            _pageHelper = new PageHelper();

        }

        /// <summary>
        /// Полученная модель работника
        /// </summary>
        public Worker Worker { get; set; }

        /// <summary>
        /// Заполняет работника в соответсвтии с идентификтором
        /// </summary>
        /// <param name="id">Идентификатор работника</param>
        public IActionResult OnGet(int id)
        {
            try
            {
                Worker = _companyService.GetWorker(id);

                if (Worker is null)
                    return NotFound($"Worker with Id={id} is not found");

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _pageHelper.GetErrorMessage("Workers/Details/OnGet"));

                return Page();
            }
        }
    }
}
