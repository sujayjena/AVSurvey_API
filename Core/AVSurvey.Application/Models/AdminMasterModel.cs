using AVSurvey.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSurvey.Application.Models
{
    public class AdminMasterModel
    {
    }

    #region Gender
    public class Gender_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? GenderName { get; set; }

        public bool? IsActive { get; set; }
    }

    public class Gender_Response : BaseResponseEntity
    {
        public string? GenderName { get; set; }

        public bool? IsActive { get; set; }
    }
    #endregion

    #region Category
    public class Category_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? CategoryName { get; set; }

        public bool? IsActive { get; set; }
    }

    public class Category_Response : BaseResponseEntity
    {
        public string? CategoryName { get; set; }

        public bool? IsActive { get; set; }
    }
    #endregion

    #region Branch
    public class Branch_Request : BaseEntity
    {
        [DefaultValue("")]
        public string? BranchName { get; set; }

        [DefaultValue("")]
        public string? EmailId { get; set; }

        [DefaultValue("")]
        public string? MobileNo { get; set; }

        [DefaultValue("")]
        public string? Address { get; set; }

        [DefaultValue("")]
        public string? StateName { get; set; }

        [DefaultValue("")]
        public string? CityName { get; set; }

        [DefaultValue("")]
        public string? AreaName { get; set; }

        [DefaultValue("")]
        public string? Pincode { get; set; }
        public bool? IsActive { get; set; }
    }

    public class Branch_Response : BaseResponseEntity
    {
        public string? BranchName { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
        public string? StateName { get; set; }
        public string? CityName { get; set; }
        public string? AreaName { get; set; }
        public string? Pincode { get; set; }
        public bool? IsActive { get; set; }
    }
    #endregion
}
