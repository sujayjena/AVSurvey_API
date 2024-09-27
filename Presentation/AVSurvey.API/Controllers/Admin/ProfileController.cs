using AVSurvey.Application.Enums;
using AVSurvey.Application.Helpers;
using AVSurvey.Application.Interfaces;
using AVSurvey.Application.Models;
using AVSurvey.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AVSurvey.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Role 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveRole(Role_Request parameters)
        {

            var fdgf = SessionManager.LoggedInUserId;

            int result = await _profileRepository.SaveRole(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";
            }
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetRoleList(BaseSearchEntity parameters)
        {
            IEnumerable<Role_Response> lstRoles = await _profileRepository.GetRoleList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetRoleById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _profileRepository.GetRoleById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region RoleHierarchy 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveRoleHierarchy(RoleHierarchy_Request parameters)
        {

            var fdgf = SessionManager.LoggedInUserId;

            int result = await _profileRepository.SaveRoleHierarchy(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";
            }
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetRoleHierarchyList(BaseSearchEntity parameters)
        {
            IEnumerable<RoleHierarchy_Response> lstRoleHierarchys = await _profileRepository.GetRoleHierarchyList(parameters);
            _response.Data = lstRoleHierarchys.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetRoleHierarchyById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _profileRepository.GetRoleHierarchyById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
