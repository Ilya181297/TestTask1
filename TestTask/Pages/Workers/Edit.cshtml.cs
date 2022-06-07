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
        private readonly ITestTaskService _testTaskService;
        private readonly ILogger<EditModel> _logger;
        private readonly IPageHelper _pageHelper;

        /// <summary>
        /// Конструктор страницы
        /// </summary>
        /// <param name="testTaskService">Сервис для работы с подразделениями и сотрудниками</param>
        /// <param name="logger">Логер</param>
        public EditModel(ITestTaskService testTaskService, ILogger<EditModel> logger, IPageHelper pageHelper)
        {
            _testTaskService = testTaskService;
            _logger = logger;
            _pageHelper = pageHelper;
        }

        /// <summary>
        /// Полученный список всех подразделений
        /// </summary>
        public List<SelectListItem> Divisions { get; set; }

        /// <summary>
        /// Полученный список полов работника
        /// </summary>
        public List<SelectListItem> Genders { get; set; }

        /// <summary>
        /// Идентификатор выбранного подразделение
        /// </summary>
        [BindProperty]
        public int SelectedDivisionId { get; set; }

        /// <summary>
        /// Выбранный пол работника
        /// </summary>
        [BindProperty]
        public int SelectedGender { get; set; }

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
                Genders = _pageHelper.GetGenderListItems();
                Divisions = _pageHelper.ConvertToSelectList(_testTaskService.GetDivisions());

                if (id == 0)
                {
                    Worker = new Worker();
                    Worker.BirthDate = DateTime.Today.AddYears(-18);
                    SelectedDivisionId = _pageHelper.GetFilterIdOnSession(HttpContext);

                    return Page();
                }

                Worker = _testTaskService.GetWorker(id);

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
        /// Создание/редактирование работника
        /// </summary>
        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();

                Worker.DivisionId = SelectedDivisionId;
                Worker.GenderId = SelectedGender;

                _testTaskService.SaveWorker(Worker);

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
