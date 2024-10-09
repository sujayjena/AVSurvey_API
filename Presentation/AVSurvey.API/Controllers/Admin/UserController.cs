using AVSurvey.Application.Enums;
using AVSurvey.Application.Helpers;
using AVSurvey.Application.Interfaces;
using AVSurvey.Application.Models;
using AVSurvey.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AVSurvey.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region User 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveUser(User_Request parameters)
        {
            int result = await _userRepository.SaveUser(parameters);

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

                #region // Add/Update Branch Mapping

                // Delete Old mapping of employee

                var vUserCategoryDELETEObj = new UserCategory_Request()
                {
                    Action = "DELETE",
                    UserId = result,
                    CategoryId = 0
                };
                int resultUserCategoryDELETE = await _userRepository.SaveUserCategory(vUserCategoryDELETEObj);


                // Add new mapping of employee
                foreach (var vUserCategoryitem in parameters.UserCategoryList)
                {
                    var vUserCategoryObj = new UserCategory_Request()
                    {
                        Action = "INSERT",
                        UserId = result,
                        CategoryId = vUserCategoryitem.CategoryId
                    };

                    int resultUserCategory = await _userRepository.SaveUserCategory(vUserCategoryObj);
                }

                #endregion
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetUserList(BaseSearchEntity parameters)
        {
            IEnumerable<User_Response> lstUsers = await _userRepository.GetUserList(parameters);

            foreach (var item in lstUsers)
            {
                var vUserCategoryObj = await _userRepository.GetUserCategoryByEmployeeId(item.Id, 0);
                item.UserCategoryList = (vUserCategoryObj.ToList());
            }
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetUserById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _userRepository.GetUserById(Id);

                if (vResultObj != null)
                {
                    var vUserCategoryObj = await _userRepository.GetUserCategoryByEmployeeId(vResultObj.Id, 0);

                    foreach (var item in vUserCategoryObj)
                    {
                        var vCatResOnj = new UserCategory_Response()
                        {
                            Id = item.Id,
                            UserId = vResultObj.Id,
                            CategoryId = item.CategoryId,
                            CategoryName = item.CategoryName
                        };

                        vResultObj.UserCategoryList.Add(vCatResOnj);
                    }
                }
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetUserLisByRoleIdOrRoleName(UserListByRole_Search parameters)
        {
            IEnumerable<UserListByRole_Response> lstUsers = await _userRepository.GetUserLisByRoleIdOrRoleName(parameters);
            _response.Data = lstUsers.ToList();
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetReportingToEmpListForSelectList(ReportingToEmpListParameters parameters)
        {
            IEnumerable<SelectList_Response> lstResponse = await _userRepository.GetReportingToEmployeeForSelectList(parameters);
            _response.Data = lstResponse.ToList();
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEmployeesListByReportingTo(int EmployeeId)
        {
            IEnumerable<EmployeesListByReportingTo_Response> lstResponse = await _userRepository.GetEmployeesListByReportingTo(EmployeeId);
            _response.Data = lstResponse.ToList();
            return _response;
        }

        #endregion
    }
}
