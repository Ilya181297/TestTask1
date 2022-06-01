#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Common;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Workers
{
    /// <summary>
    /// Страница создания/редактирования работника
    /// </summary>
    public class EditModel : PageModel
    {
        private readonly ITestTaskService _companyService;
        private readonly ILogger<EditModel> _logger;
        private readonly PageHelper _pageHelper;

        /// <summary>
        /// Конструктор страницы
        /// </summary>
        /// <param name="testTaskService">Сервис для работы с подразделениями и сотрудниками</param>
        /// <param name="logger">Логер</param>
        public EditModel(ITestTaskService companyService, ILogger<EditModel> logger)
        {
            _companyService = companyService;
            _logger = logger;
            _pageHelper = new PageHelper();
        }

        /// <summary>
        /// Список подразделений
        /// </summary>
        public List<SelectListItem> Divisions { get; set; }

        /// <summary>
        /// Список пола работника
        /// </summary>
        public List<SelectListItem> Genders { get; set; }

        /// <summary>
        /// Выбранное подразделение
        /// </summary>
        [BindProperty]
        public int SelectedDivisionId { get; set; }

        /// <summary>
        /// Выбранный пол
        /// </summary>
        [BindProperty]
        public int SelectedGender { get; set; }

        /// <summary>
        /// Полученная модель работника
        /// </summary>
        [BindProperty]
        public Worker Worker { get; set; }

        /// <summary>
        /// Заполняет работника в соответсвтии с идентификтором
        /// </summary>
        /// <param name="id">Идентификатор работника</param>
        public IActionResult OnGet(int id)
        {
            try
            {
                Genders = _pageHelper.GetGenderListItems();
                Divisions = _pageHelper.ConvertToSelectList(_companyService.GetDivisions());

                if (id == 0)
                {
                    Worker = new Worker();
                    Worker.BirthDate = DateTime.Today.AddYears(-18);
                    SelectedDivisionId = _pageHelper.GetFilterIdOnSession(HttpContext);

                    return Page();
                }

                Worker = _companyService.GetWorker(id);

                if (Worker is null)
                    return NotFound($"Worker with Id={id} is not found");

                SelectedDivisionId = Worker.DivisionId;
                SelectedGender = Worker.GenderId;

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _pageHelper.GetErrorMessage("Workers/Edit/OnGet"));

                return Page();
            }
        }

        /// <summary>
        /// Создаёт/редактирует работника
        /// </summary>
        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();

                Worker.DivisionId = SelectedDivisionId;
                Worker.GenderId = SelectedGender;

                _companyService.SaveWorker(Worker);

                return RedirectToPage("../Index", new { id = _pageHelper.GetFilterIdOnSession(HttpContext) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _pageHelper.GetErrorMessage("Workers/Edit/OnPost"));

                return Page();
            }
        }
    }
}
