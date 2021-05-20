using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.ResponseModels
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Mobileno { get; set; }

        public ICollection<TaskResponseModel> Tasks { get; set; }
        public ICollection<TaskHistoryResponseModel> TaskHistories { get; set; }
    }
}
