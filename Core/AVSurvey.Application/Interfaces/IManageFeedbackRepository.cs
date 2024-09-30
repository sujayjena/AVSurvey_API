using AVSurvey.Application.Models;
using AVSurvey.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSurvey.Application.Interfaces
{
    public interface IManageFeedbackRepository
    {
        #region Feedback question

        Task<int> SaveFeedbackQuestion(FeedbackQuestion_Request parameters);

        Task<IEnumerable<FeedbackQuestion_Response>> GetFeedbackQuestionList(FeedbackQuestionSearch_Request parameters);

        Task<FeedbackQuestion_Response?> GetFeedbackQuestionById(int Id);

        Task<int> FBQuestionActivate_Deactivate(FBQuestionActivate_Deactivate_Response parameters);

        #endregion

        #region Feedback question answer

        Task<int> SaveFeedbackQuestionAnswer(FeedbackQuestionAnswer_Request parameters);

        #endregion
    }
}
