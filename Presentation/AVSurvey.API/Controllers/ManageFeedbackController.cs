using AVSurvey.Application.Enums;
using AVSurvey.Application.Helpers;
using AVSurvey.Application.Interfaces;
using AVSurvey.Application.Models;
using AVSurvey.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Globalization;
using Newtonsoft.Json;

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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportFeedbackQuestionAnswerData(FeedbackQuestionAnswerSearch_Request request)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex = 1;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<FeedbackQuestionAnswer_Response> lstObj = await _manageFeedbackRepository.GetFeedbackQuestionAnswerList(request);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("FeedbackQuestionAnswer");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    foreach (var items in lstObj)
                    {
                        var jsonData = JsonConvert.DeserializeObject<dynamic>(items.FBQuestion_answer_json_format); 

                        if (recordIndex == 1)
                        {
                            //Header of table
                            WorkSheet1.Row(1).Height = 20;
                            WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            WorkSheet1.Row(1).Style.Font.Bold = true;

                            WorkSheet1.Cells[1, 1].Value = "Registration No";
                            WorkSheet1.Cells[1, 2].Value = "Mobile No";

                            int a = 3;
                            foreach (var jItem in jsonData)
                            {
                                WorkSheet1.Cells[1, a].Value = "Question" + jItem.qno;
                                a++;

                                WorkSheet1.Cells[1, a].Value = "Answer" + jItem.qno;
                                a++;
                            }

                            WorkSheet1.Cells[1, a].Value = "Created Date";
                            a++;

                            WorkSheet1.Cells[1, a].Value = "Creator Name";

                            recordIndex = 2;
                        }

                        WorkSheet1.Cells[recordIndex, 1].Value = items.RegistrationNo;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.MobileNo;

                        int b = 3;
                        foreach (var jItem in jsonData)
                        {
                            WorkSheet1.Cells[recordIndex, b].Value = jItem.q.ToString();
                            b++;

                            WorkSheet1.Cells[recordIndex, b].Value = jItem.given_answer.ToString();
                            b++;
                        }

                        WorkSheet1.Cells[recordIndex, b].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, b].Value = items.CreatedDate;
                        b++;

                        WorkSheet1.Cells[recordIndex, b].Value = items.CreatorName;

                        recordIndex += 1;
                    }

                    WorkSheet1.Columns.AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }

            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Record Exported successfully";
            }

            return _response;
        }
        #endregion
    }
}
