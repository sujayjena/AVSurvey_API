﻿using AVSurvey.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSurvey.Application.Models
{
    public class ProfileModel
    {
    }

    #region Role

    public class Role_Request : BaseEntity
    {
        public string? RoleName { get; set; }
        public bool? IsActive { get; set; }
    }

    public class Role_Response : BaseResponseEntity
    {
        public string? RoleName { get; set; }
        public bool? IsActive { get; set; }
    }

    #endregion

    #region RoleHierarchy

    public class RoleHierarchy_Request : BaseEntity
    {
        [Required]
        public int? RoleId { get; set; }

        public int? ReportingTo { get; set; }

        public bool? IsActive { get; set; }
    }

    public class RoleHierarchy_Response : BaseResponseEntity
    {
        public int? RoleId { get; set; }

        public string? RoleName { get; set; }

        public int? ReportingTo { get; set; }

        public string? ReportingToName { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion
}
