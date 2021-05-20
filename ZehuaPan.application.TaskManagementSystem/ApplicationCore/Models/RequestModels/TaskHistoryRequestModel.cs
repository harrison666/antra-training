using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.RequestModels
{
    public class TaskHistoryRequestModel
    {
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Completed { get; set; }
        public string Remarks { get; set; }
    }
}
