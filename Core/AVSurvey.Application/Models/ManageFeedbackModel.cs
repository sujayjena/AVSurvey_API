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
    public class ManageFeedbackModel
    {
    }

    #region Feedback Question
    public class FeedbackQuestion_Request : BaseEntity
    {
        public int? CategoryId { get; set; }

        [DefaultValue("")]
        public string? FBQuestion_Title { get; set; }

        [DefaultValue("")]
        public string? LanguageType { get; set; }

        [DefaultValue("")]
        public string? FBQuestion_Desc { get; set; }

        [DefaultValue("")]
        public string? FBQuestion_json_format { get; set; }

        [DefaultValue("")]
        public string? FBQuestion_audio_file { get; set; }
        public int? StatusId { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsActive { get; set; }
    }

    public class FeedbackQuestion_Response : BaseResponseEntity
    {
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? FBQuestion_Title { get; set; }
        public string? LanguageType { get; set; }
        public string? FBQuestion_Desc { get; set; }
        public string? FBQuestion_json_format { get; set; }
        public string? FBQuestion_audio_file { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsActive { get; set; }
    }
    #endregion
}
