using AeroTripProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Application.Dtos.Comment
{
    public class ResultComment
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public bool CommentState { get; set; }
        public int DestinationID { get; set; }
        public string Destination {  get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
    }
}
