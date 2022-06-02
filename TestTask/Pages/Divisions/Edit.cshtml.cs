#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Common;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages.Divisions
{
    /// <summary>
    /// Страница создания/редактирования подразделения
    /// </summary>
    public class EditModel : PageModel
    {
        private readonly ITestTaskService _testTaskService;
        private readonly ILogger<EditModel> _logger;
        public readonly PageHelper _pageHelper;

        /// <summary>
        /// Конструктор страницы
        /// </summary>
        /// <param name="testTaskService">Сервис для работы с подразделениями и сотрудниками</param>
        /// <param name="logger">Логер</param>
        public EditModel(ITestTaskService testTaskService, ILogger<EditModel> logger)
        {
            _testTaskService = testTaskService;
            _logger = logger;
            _pageHelper = new PageHelper();
        }

        /// <summary>
        /// Полученный список всех подразделений
        /// </summary>
        public List<SelectListItem> Divisions { get; set; }

        /// <summary>
        /// Полученная модель подразделения
        /// </summary>
        [BindProperty]
        public Division Division { get; set; }

        /// <summary>
        /// Идентифиактор выбранного родительского подразделения
        /// </summary>
        [BindProperty]
        public int SelectedParentId { get; set; }

        /// <summary>
        /// Возвращает страницу с заполненным подразделением в соответсвтии с идентификатором
        /// </summary>
        /// <param name="id">Идентификатор подразедления</param>
        public IActionResult OnGet(int id)
        {
            try
            {
                var divisions = _testTaskService.GetDivisions().Where(x => x.Id != id);
                Divisions = _pageHelper.ConvertToSelectList(divisions, true);

                if (id == 0)
                {
                    Division = new Division();
                    Division.FormationDate = DateTime.Today;
                    SelectedParentId = _pageHelper.GetFilterIdOnSession(HttpContext);

                    return Page();
                }

                Division = _testTaskService.GetDivision(id);

                if (Division is null)
                    return NotFound();

                SelectedParentId = Division.ParentId ?? 0;

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _pageHelper.GetErrorMessage("Divisions/Edit/OnGet"));

                return Page();
            }
        }

        /// <summary>
        /// Создание/редактирование подразделения
        /// </summary>
        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();

                Division.ParentId = SelectedParentId == 0 ? null : SelectedParentId;
                _testTaskService.SaveDivision(Division);

                return RedirectToPage("../Index", new { id = _pageHelper.GetFilterIdOnSession(HttpContext) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _pageHelper.GetErrorMessage("Divisions/Edit/OnPost"));

                return Page();
            }
        }
    }
}

