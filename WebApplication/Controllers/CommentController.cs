using DapperPart.Entities;
using DapperPart.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private IUnitOfWork _unitOfWork;

        public CommentController(ILogger<CommentController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllComments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllCommentsAsync()
        {
            try
            {
                var results = await _unitOfWork._commentRepository.GetAllAsync();
                _unitOfWork.Commit();
                _logger.LogInformation("Returned all comments from database.");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction Failed! Something went wrong inside GetAllCommentsAsync(): {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Comment>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _unitOfWork._commentRepository.GetAsync(id);
                _unitOfWork.Commit();
                if (result == null)
                {
                    _logger.LogError($"Comment with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _logger.LogInformation($"Returned comment with id: {id}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByIdAsync: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost("PostComment")]
        public async Task<ActionResult> PostCommentAsync([FromBody] Comment newComment)
        {
            try
            {
                if (newComment == null)
                {
                    _logger.LogError("Comment object sent from client is null.");
                    return BadRequest("Comment object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid comment object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var createdId = await _unitOfWork._commentRepository.AddAsync(newComment);
                var createdComment = await _unitOfWork._commentRepository.GetAsync(createdId);
                _unitOfWork.Commit();
                return CreatedAtAction(nameof(GetByIdAsync), new { id = createdId }, createdComment);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PostCommentAsync: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("Put/{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Comment updatedComment)
        {
            try
            {
                if (updatedComment == null)
                {
                    _logger.LogError("Comment object sent from client is null.");
                    return BadRequest("Comment object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid comment object sent from client.");
                    return BadRequest("Invalid comment object");
                }
                var commentEntity = await _unitOfWork._commentRepository.GetAsync(id);
                if (commentEntity == null)
                {
                    _logger.LogError($"Comment with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitOfWork._commentRepository.ReplaceAsync(updatedComment);
                _unitOfWork.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PutAsync: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var commentEntity = await _unitOfWork._commentRepository.GetAsync(id);
                if (commentEntity == null)
                {
                    _logger.LogError($"Comment with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitOfWork._commentRepository.DeleteAsync(id);
                _unitOfWork.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteAsync: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }

}
