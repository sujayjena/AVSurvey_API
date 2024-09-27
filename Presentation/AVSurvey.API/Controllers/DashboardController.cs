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
        private IFileManager _fileManager;

        public DashboardController(IFileManager fileManager)
        {
            _fileManager = fileManager;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }
    }
}
