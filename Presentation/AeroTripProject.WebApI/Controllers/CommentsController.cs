using AeroTripProject.Application.Dtos.Comment;
using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AeroTripProject.WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IRepostory<Comment> _repostory;
        private readonly IValidator<Comment> _validator;

        public CommentsController(IRepostory<Comment> repostory, IValidator<Comment> validator)
        {
            _repostory = repostory;
            _validator = validator;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var values = await _repostory.GetListIncludeAsync(x => x.AppUser,x=>x.Destination);
            var result = values

                .Select(x => new ResultComment
                {
                    Id = x.Id,
                    AppUserId = x.AppUserId,
                    CommentDate = x.CommentDate,
                    CommentContent = x.CommentContent,
                    CommentState = x.CommentState,
                    DestinationID = x.DestinationID,
                    Destination = x.Destination.City,
                    UserName = x.AppUser.UserName,
                    ImageUrl = x.AppUser.ImageUrl
                });
                

            return Ok(result);

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var Comment = await _repostory.GetByIdAsync(Id);
            return Ok(Comment);

        }
        [HttpGet("GetByIdCommentUser/{appuserId}")]
        public async Task<IActionResult> GetByIdCommentUser(int appuserId)
        {
            var values = await _repostory.GetListIncludeAsync(
                x => x.AppUser,
                x => x.Destination
            );

            var result = values
                .Where(x => x.AppUserId == appuserId)
                .Select(x => new ResultComment
                {
                    Id = x.Id,
                    AppUserId = x.AppUserId,
                    CommentDate = x.CommentDate,
                    CommentContent = x.CommentContent,
                    CommentState = x.CommentState,
                    DestinationID = x.DestinationID,

                    Destination = x.Destination.City,
                    UserName = x.AppUser.UserName,
                    ImageUrl = x.AppUser.ImageUrl
                })
                .ToList();

            return Ok(result);
        }

        [HttpGet("GetListCommentById/{Id}")]
        public async Task<IActionResult> GetListCommentById(int Id)
        {
            var values = await _repostory.GetListIncludeAsync(x => x.AppUser);

            var result = values
                .Where(x => x.DestinationID == Id)
                .Select(x => new ResultComment
                {
                    Id = x.Id,
                    AppUserId = x.AppUserId,
                    CommentDate = x.CommentDate,
                    CommentContent = x.CommentContent,
                    CommentState = x.CommentState,
                    DestinationID = x.DestinationID,

                    UserName = x.AppUser.UserName,
                    ImageUrl = x.AppUser.ImageUrl
                })
                .ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateComment createComment)
        {
            var Comment = new Comment
            {
               AppUserId=createComment.AppUserId,
               CommentDate=createComment.CommentDate,
               CommentContent=createComment.CommentContent,
               CommentState=createComment.CommentState,
               DestinationID=createComment.DestinationID
            };
            var validationresult = _validator.Validate(Comment);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(x => new
                {
                    PropertyName=x.PropertyName,
                    ErrorMessage=x.ErrorMessage
                }));
            }
            await _repostory.InsertAsync(Comment);
            return Ok("Melumat elave olundu");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateComment updateComment)
        {
            var Comment = new Comment
            {
               Id=updateComment.Id,
               AppUserId=updateComment.AppUserId,
               CommentDate=updateComment.CommentDate,
               CommentContent=updateComment.CommentContent,
               CommentState=updateComment.CommentState,
               DestinationID=updateComment.DestinationID

            };


            var validationresult = _validator.Validate(Comment);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult.Errors.Select(e => e.ErrorMessage));
            }
            await _repostory.UpdateAsync(Comment);
            return Ok("Melumat deyisdirildi");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _repostory.DeleteAsync(Id);
            return Ok("Melumat silindi");
        }

        [HttpGet("Count")]
        public async Task<IActionResult> GetCount()
        {
            return Ok(await _repostory.CountAsync());

        }

    }
}
