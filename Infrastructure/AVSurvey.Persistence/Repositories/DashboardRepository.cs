﻿using AVSurvey.Application.Helpers;
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
    public class DashboardRepository : GenericRepository, IDashboardRepository
    {
        private IConfiguration _configuration;

        public DashboardRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Dashboard_SurveySummary_Response>> GetDashboard_SurveySummary(Dashboard_Search_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@FilterType", parameters.FilterType);
            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Dashboard_SurveySummary_Response>("GetDashboard_SurveySummary", queryParameters);

            return result;
        }
    }
}