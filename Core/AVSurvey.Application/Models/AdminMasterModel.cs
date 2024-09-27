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
}
