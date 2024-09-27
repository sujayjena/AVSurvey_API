using AVSurvey.Application.Helpers;
using AVSurvey.Application.Interfaces;
using AVSurvey.Application.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSurvey.Persistence.Repositories
{
    public class UserRepository : GenericRepository, IUserRepository
    {
        private IConfiguration _configuration;

        public UserRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        #region User 

        public async Task<int> SaveUser(User_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@UserName", parameters.UserName);
            queryParameters.Add("@GenderId", parameters.GenderId);
            queryParameters.Add("@MobileNumber", parameters.MobileNumber);
            queryParameters.Add("@EmailId", parameters.EmailId);
            queryParameters.Add("@Password", !string.IsNullOrWhiteSpace(parameters.Password) ? EncryptDecryptHelper.EncryptString(parameters.Password) : string.Empty);
            queryParameters.Add("@RoleId", parameters.RoleId);
            queryParameters.Add("@ReportingTo", parameters.ReportingTo);
            queryParameters.Add("@AddressLine", parameters.AddressLine);
            queryParameters.Add("@MobileUniqueId", parameters.MobileUniqueId);
            queryParameters.Add("@IsMobileUser", parameters.IsMobileUser);
            queryParameters.Add("@IsWebUser", parameters.IsWebUser);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveUser", queryParameters);
        }

        public async Task<IEnumerable<User_Response>> GetUserList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<User_Response>("GetUserList", queryParameters);

            if (SessionManager.LoggedInUserId > 1)
            {
                if (SessionManager.LoggedInUserId > 2)
                {
                    result = result.Where(x => x.Id > 2).ToList();
                }
                else
                {
                    result = result.Where(x => x.Id > 1).ToList();
                }
            }

            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<User_Response?> GetUserById(long Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<User_Response>("GetUserById", queryParameters)).FirstOrDefault();
        }

        public async Task<int> SaveUserCategory(UserCategory_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Action", parameters.Action);
            queryParameters.Add("@EmployeeId", parameters.UserId);
            queryParameters.Add("@CategoryId", parameters.CategoryId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveUserCategory", queryParameters);
        }

        public async Task<IEnumerable<UserCategory_Response>> GetUserCategoryByEmployeeId(int EmployeeId, int CategoryId)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@EmployeeId", EmployeeId);
            queryParameters.Add("@CategoryId", CategoryId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<UserCategory_Response>("GetUserCategoryByEmployeeId", queryParameters);

            return result;
        }

        public async Task<IEnumerable<UserListByRole_Response>> GetUserLisByRoleIdOrRoleName(UserListByRole_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@RoleId", parameters.RoleId.SanitizeValue());
            queryParameters.Add("@RoleName", parameters.RoleName.SanitizeValue());

            var result = await ListByStoredProcedure<UserListByRole_Response>("GetUserByRoleIdOrRoleName", queryParameters);

            return result;
        }

        public async Task<IEnumerable<SelectList_Response>> GetReportingToEmployeeForSelectList(ReportingToEmpListParameters parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@RoleId", parameters.RoleId);
            queryParameters.Add("@RegionId", parameters.RegionId.SanitizeValue());

            return await ListByStoredProcedure<SelectList_Response>("GetReportingToEmployeeForSelectList", queryParameters);
        }

        public async Task<IEnumerable<EmployeesListByReportingTo_Response>> GetEmployeesListByReportingTo(int EmployeeId)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@EmployeeId", EmployeeId);

            return await ListByStoredProcedure<EmployeesListByReportingTo_Response>("GetEmployeesListByReportingTo", queryParameters);
        }

        #endregion
    }
}
