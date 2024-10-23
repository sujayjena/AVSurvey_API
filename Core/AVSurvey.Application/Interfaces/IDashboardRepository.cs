using AVSurvey.Application.Models;
using AVSurvey.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSurvey.Application.Interfaces
{
    public interface IDashboardRepository
    {
        Task<IEnumerable<Dashboard_SurveySummary_Response>> GetDashboard_SurveySummary(Dashboard_Search_Request parameters);
        Task<IEnumerable<Dashboard_SurveyNPSSummary_Response>> GetDashboard_SurveyNPSSummary(DashboardNPS_Search_Request parameters);
    }
}
