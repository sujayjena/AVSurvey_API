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
        private readonly IAdminMasterRepository _adminMasterRepository;

        public UserController(IUserRepository userRepository, IAdminMasterRepository adminMasterRepository)
        {
            _userRepository = userRepository;
            _adminMasterRepository = adminMasterRepository;

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

                #region // Add/Update User Category

                // Delete Old User Category

                var vUserCategoryDELETEObj = new UserCategory_Request()
                {
                    Action = "DELETE",
                    UserId = result,
                    CategoryId = 0
                };
                int resultUserCategoryDELETE = await _userRepository.SaveUserCategory(vUserCategoryDELETEObj);


                // Add new User Category
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

                #region // Add/Update Branch Mapping

                // Delete Old mapping of employee

                var vBracnMapDELETEObj = new BranchMapping_Request()
                {
                    Action = "DELETE",
                    UserId = result,
                    BranchId = 0
                };
                int resultBranchMappingDELETE = await _userRepository.SaveBranchMapping(vBracnMapDELETEObj);


                // Add new mapping of employee
                foreach (var vBranchitem in parameters.BranchList)
                {
                    var vBracnMapObj = new BranchMapping_Request()
                    {
                        Action = "INSERT",
                        UserId = result,
                        BranchId = vBranchitem.BranchId
                    };

                    int resultBranchMapping = await _userRepository.SaveBranchMapping(vBracnMapObj);
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
                    //User Category
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

                    //Branch Mapping
                    var vBranchMappingObj = await _userRepository.GetBranchMappingByEmployeeId(vResultObj.Id, 0);
                    foreach (var item in vBranchMappingObj)
                    {
                        var vBranchObj = await _adminMasterRepository.GetBranchById(Convert.ToInt32(item.BranchId));
                        var vBrMapResOnj = new BranchMapping_Response()
                        {
                            Id = item.Id,
                            UserId = vResultObj.Id,
                            EmployeeId = vResultObj.Id,
                            BranchId = item.BranchId,
                            BranchName = vBranchObj != null ? vBranchObj.BranchName : string.Empty,
                        };

                        vResultObj.BranchList.Add(vBrMapResOnj);
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
