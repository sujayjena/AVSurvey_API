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

        Task<IEnumerable<FeedbackQuestion_Response>> GetFeedbackQuestionList(BaseSearchEntity parameters);

        Task<FeedbackQuestion_Response?> GetFeedbackQuestionById(int Id);

        #endregion
    }
}
