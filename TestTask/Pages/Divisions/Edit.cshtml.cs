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
        /// Полученная модель подразделения
        /// </summary>
        [BindProperty]
        public Division Division { get; set; }

        /// <summary>
        /// Выбранное родительское подразделение
        /// </summary>
        [BindProperty]
        public int SelectedParentId { get; set; }

        /// <summary>
        /// Заполняет подразделение в соответсвтии с идентификтором
        /// </summary>
        /// <param name="id">Идентификатор подразедления</param>
        public IActionResult OnGet(int id)
        {
            try
            {
                var divisions = _companyService.GetDivisions().Where(x => x.Id != id);
                Divisions = _pageHelper.ConvertToSelectList(divisions, true);

                if (id == 0)
                {
                    Division = new Division();
                    Division.FormationDate = DateTime.Today;
                    SelectedParentId = _pageHelper.GetFilterIdOnSession(HttpContext);

                    return Page();
                }

                Division = _companyService.GetDivision(id);

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
        /// Создает/редактирует подразделение
        /// </summary>
        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();

                Division.ParentId = SelectedParentId == 0 ? null : SelectedParentId;
                _companyService.SaveDivision(Division);

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

