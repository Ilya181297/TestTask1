#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Common;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages
{
    /// <summary>
    /// Главная страница справочника
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ITestTaskService _companyService;

        private readonly ILogger<IndexModel> _logger;

        private readonly PageHelper _pageHelper;

        /// <summary>
        /// Конструктор страницы
        /// </summary>
        /// <param name="testTaskService">Сервис для работы с подразделениями и сотрудниками</param>
        /// <param name="logger">Логер</param>
        public IndexModel(ITestTaskService companyService, ILogger<IndexModel> logger)
        {
            _companyService = companyService;
            _logger = logger;
            _pageHelper = new PageHelper();
        }

        /// <summary>
        /// Полученный список подразделений
        /// </summary>
        public List<Division> Divisions { get; set; }

        /// <summary>
        /// Полученный список работников
        /// </summary>
        public List<Worker> Workers { get; set; }

        /// <summary>
        /// Выбранное подразделение для фильтрации
        /// </summary>
        public Division SelectedDivision { get; set; }

        /// <summary>
        /// Возвращает страницу с работниками в соответсвтии с ид. выбранного подразделения
        /// </summary>
        /// <param name="id">Идентификатор подразделения</param>
        public void OnGet(int id)
        {
            try
            {
                _pageHelper.SetFilterIdOnSession(HttpContext, id);

                Divisions = _companyService.GetDivisions().FindAll(x => !x.ParentId.HasValue);
                SelectedDivision = _companyService.GetDivision(id);

                Workers = SelectedDivision is null 
                    ? _companyService.GetWorkers() 
                    : _companyService.GetAllWorkersByDivision(SelectedDivision);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _pageHelper.GetErrorMessage("Index/OnGetFilter"));
            }
        }
    }
}
