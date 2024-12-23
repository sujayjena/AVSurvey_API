using AVSurvey.Domain.Entities;
using AVSurvey.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AVSurvey.Application.Models
{
    public class UserModel
    {
    }

    public class User_Request : BaseEntity
    {
        public User_Request()
        {
            UserCategoryList = new List<UserCategory_Request>();
            BranchList = new List<BranchMapping_Request>();
        }

        public string? UserName { get; set; }

        [DefaultValue(0)]
        public int? GenderId { get; set; }

        public string? MobileNumber { get; set; }

        public string? EmailId { get; set; }

        public string? Password { get; set; }

        public int? RoleId { get; set; }

        public int? ReportingTo { get; set; }

        public string? AddressLine { get; set; }

        public string? MobileUniqueId { get; set; }

        public bool? IsMobileUser { get; set; }

        public bool? IsWebUser { get; set; }

        public bool? IsActive { get; set; }

        public List<UserCategory_Request>? UserCategoryList { get; set; }
        public List<BranchMapping_Request>? BranchList { get; set; }
    }

    public class User_Response : BaseResponseEntity
    {
        public User_Response()
        {
            UserCategoryList = new List<UserCategory_Response>();
            BranchList = new List<BranchMapping_Response>();
        }

        public string? UserName { get; set; }

        public int? GenderId { get; set; }

        public string? GenderName { get; set; }

        public string? MobileNumber { get; set; }

        public string? EmailId { get; set; }

        public string? Password { get; set; }

        public int? RoleId { get; set; }

        public string? RoleName { get; set; }

        public int? ReportingTo { get; set; }

        public string? ReportingToName { get; set; }

        public string? ReportingToMobileNo { get; set; }

        public string? AddressLine { get; set; }

        public string? MobileUniqueId { get; set; }

        public bool? IsMobileUser { get; set; }

        public bool? IsWebUser { get; set; }

        public bool? IsActive { get; set; }

        public List<UserCategory_Response>? UserCategoryList { get; set; }
        public List<BranchMapping_Response>? BranchList { get; set; }
    }

    public class UserListByRole_Search
    {
        public string? RoleId { get; set; }

        public string? RoleName { get; set; }
    }

    public class UserListByRole_Response
    {
        public int? UserId { get; set; }

        public string? UserName { get; set; }
    }

    public class SelectList_Response
    {
        public long Value { get; set; }
        public string? Text { get; set; }
    }

    public class ReportingToEmpListParameters
    {
        public long RoleId { get; set; }
        public long? RegionId { get; set; }
    }

    public partial class EmployeesListByReportingTo_Response
    {
        public int? Id { get; set; }
        public string? EmployeeName { get; set; }
        public int? UserId { get; set; }
    }

    public class UserCategory_Request : BaseEntity
    {
        [JsonIgnore]
        public string? Action { get; set; }
        [JsonIgnore]
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
    }
    public class UserCategory_Response : BaseEntity
    {
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

    public class BranchMapping_Request : BaseEntity
    {
        [JsonIgnore]
        public string? Action { get; set; }
        [JsonIgnore]
        public int? UserId { get; set; }
        public int? BranchId { get; set; }
    }

    public class BranchMapping_Response : BaseEntity
    {
        public int? UserId { get; set; }

        [JsonIgnore]
        public int? EmployeeId { get; set; }

        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
    }
}
