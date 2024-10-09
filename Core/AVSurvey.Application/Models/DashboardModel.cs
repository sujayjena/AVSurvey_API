using AVSurvey.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSurvey.Application.Models
{
    public class DashboardModel
    {
    }

    public class Dashboard_SurveySummary_Response
    {
        public int? TotalCompleteSurvey { get; set; }

        public int? TotalTodayCompleteSurvey { get; set; }
    }

    public class Dashboard_Search_Request
    {
        [DefaultValue("All")]
        public string? FilterType { get; set; }

        public int? EmployeeId { get; set; }
    }
}
