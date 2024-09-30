using AVSurvey.Application.Enums;
using AVSurvey.Application.Helpers;
using AVSurvey.Application.Interfaces;
using AVSurvey.Application.Models;
using AVSurvey.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AVSurvey.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageFeedbackController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IManageFeedbackRepository _manageFeedbackRepository;

        public ManageFeedbackController(IManageFeedbackRepository manageFeedbackRepository)
        {
            _manageFeedbackRepository = manageFeedbackRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Feedback Question

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveFeedbackQuestion(FeedbackQuestion_Request parameters)
        {
            int result = await _manageFeedbackRepository.SaveFeedbackQuestion(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";
            }
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetFeedbackQuestionList(FeedbackQuestionSearch_Request parameters)
        {
            IEnumerable<FeedbackQuestion_Response> lstRoles = await _manageFeedbackRepository.GetFeedbackQuestionList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetFeedbackQuestionById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageFeedbackRepository.GetFeedbackQuestionById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> FBQuestionActivate_Deactivate(FBQuestionActivate_Deactivate_Response parameters)
        {
            if (parameters.Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                int resultDailyTravelExpense = await _manageFeedbackRepository.FBQuestionActivate_Deactivate(parameters);

                if (resultDailyTravelExpense == (int)SaveOperationEnums.NoRecordExists)
                {
                    _response.Message = "No record exists";
                }
                else if (resultDailyTravelExpense == (int)SaveOperationEnums.ReocrdExists)
                {
                    _response.Message = "Record already exists";
                }
                else if (resultDailyTravelExpense == (int)SaveOperationEnums.NoResult)
                {
                    _response.Message = "Something went wrong, please try again";
                }
                else
                {
                    _response.Message = "Record details saved sucessfully";
                }
            }
            return _response;
        }

        #endregion

        #region Feedback Question

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveFeedbackQuestionAnswer(FeedbackQuestionAnswer_Request parameters)
        {
            int result = await _manageFeedbackRepository.SaveFeedbackQuestionAnswer(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";
            }
            return _response;
        }

        #endregion
    }
}
