using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSurvey.Domain.Entities
{
    public abstract class BaseEntity
    {
        [DefaultValue(0)]
        public int Id { get; set; }
    }
}
