using AVSurvey.Application.Enums;
using AVSurvey.Application.Helpers;
using AVSurvey.Application.Interfaces;
using AVSurvey.Application.Models;
using AVSurvey.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AVSurvey.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IDashboardRepository _dashboardRepository;
        private IFileManager _fileManager;

        public DashboardController(IFileManager fileManager, IDashboardRepository dashboardRepository)
        {
            _fileManager = fileManager;
            _dashboardRepository = dashboardRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
            
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetDashboard_SurveySummary(Dashboard_Search_Request parameters)
        {
            var objList = await _dashboardRepository.GetDashboard_SurveySummary(parameters);
            _response.Data = objList.ToList();
            return _response;
        }
    }
}
