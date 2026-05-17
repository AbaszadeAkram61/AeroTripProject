using AeroTripProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Domain.Entities
{
    public class Comment:BaseEntity
    {
        
        public DateTime CommentDate {  get; set; }
        public string CommentContent {  get; set; }
        public bool CommentState {  get; set; }
        public int AppUserId {  get; set; }
        public AppUser AppUser { get; set; }
        public int DestinationID {  get; set; }
        public Destination Destination { get; set; }
    }
}
