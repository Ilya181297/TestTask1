#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICompanyService _companyService;

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ICompanyService companyService, ILogger<IndexModel> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        public List<Division> Divisions { get; set; }
        public List<Worker> Workers { get; set; }
        public Division SelectedDivision { get; set; }

        public void OnGet(int id)
        {
            try
            {
                Divisions = _companyService.GetDivisions().FindAll(x => !x.ParentId.HasValue);
                SelectedDivision = _companyService.GetDivision(id);
                
                if (SelectedDivision is null)
                {
                    Workers = _companyService.GetWorkers();
                    PageHelper.SelectedDivisionIdOnFilter = null;
                    return;
                }

                PageHelper.SelectedDivisionIdOnFilter = SelectedDivision.Id;
                Workers = _companyService.GetAllWorkersByDivision(SelectedDivision);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, PageHelper.GetErrorMessage("Index/OnGetFilter"));
            }
        }
    }
}
