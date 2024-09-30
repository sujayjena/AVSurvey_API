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
    public class ManageFeedbackRepository : GenericRepository, IManageFeedbackRepository
    {
        private IConfiguration _configuration;

        public ManageFeedbackRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        #region Feedback Question
        public async Task<int> SaveFeedbackQuestion(FeedbackQuestion_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@CategoryId", parameters.CategoryId);
            queryParameters.Add("@FBQuestion_Title", parameters.FBQuestion_Title.SanitizeValue());
            queryParameters.Add("@LanguageType", parameters.LanguageType);
            queryParameters.Add("@FBQuestion_Desc", parameters.FBQuestion_Desc);
            queryParameters.Add("@FBQuestion_json_format", parameters.FBQuestion_json_format);
            queryParameters.Add("@FBQuestion_audio_file", parameters.FBQuestion_audio_file);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@IsPublished", parameters.IsPublished);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveFeedbackQuestion", queryParameters);
        }

        public async Task<IEnumerable<FeedbackQuestion_Response>> GetFeedbackQuestionList(FeedbackQuestionSearch_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@CategoryId", parameters.CategoryId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<FeedbackQuestion_Response>("GetFeedbackQuestionList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<FeedbackQuestion_Response?> GetFeedbackQuestionById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<FeedbackQuestion_Response>("GetFeedbackQuestionById", queryParameters)).FirstOrDefault();
        }

        public async Task<int> FBQuestionActivate_Deactivate(FBQuestionActivate_Deactivate_Response parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("FBQuestionActivate_Deactivate", queryParameters);
        }

        #endregion

        #region Feedback Question Answer
        public async Task<int> SaveFeedbackQuestionAnswer(FeedbackQuestionAnswer_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@FBQuestionId", parameters.FBQuestionId);
            queryParameters.Add("@RegistrationNo", parameters.RegistrationNo);
            queryParameters.Add("@MobileNo", parameters.MobileNo);
            queryParameters.Add("@FBQuestion_answer_json_format", parameters.FBQuestion_answer_json_format);
            queryParameters.Add("@TimeTakenToFinish", parameters.TimeTakenToFinish);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveFeedbackQuestionAnswer", queryParameters);
        }
        #endregion
    }
}
