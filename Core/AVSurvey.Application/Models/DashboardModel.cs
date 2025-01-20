using AVSurvey.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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

        [DefaultValue("")]
        public string? BranchId { get; set; }
    }

    public class DashboardNPS_Search_Request
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? EmployeeId { get; set; }

        [DefaultValue("")]
        public string? BranchId { get; set; }

        public int? CategoryId { get; set; }

        [JsonIgnore]
        public int Total { get; set; }
    }

    public class Dashboard_SurveyNPSSummary_Response
    {
        public string? QuestionName { get; set; }

        [JsonIgnore]
        public long? NPS_Without_Perct { get; set; }
        public string? NPS { get; set; }
    }
}
