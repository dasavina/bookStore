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
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private IUnitOfWork _unitOfWork;

        public BookController(ILogger<BookController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllBooks")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooksAsync()
        {
            try
            {
                var results = await _unitOfWork._bookRepository.GetAllAsync();
                _unitOfWork.Commit();
                _logger.LogInformation("Returned all books from database.");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction Failed! Something went wrong inside GetAllBooksAsync(): {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Book>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _unitOfWork._bookRepository.GetAsync(id);
                _unitOfWork.Commit();
                if (result == null)
                {
                    _logger.LogError($"Book with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _logger.LogInformation($"Returned book with id: {id}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetByIdAsync: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost("PostBook")]
        public async Task<ActionResult> PostBookAsync([FromBody] Book newBook)
        {
            try
            {
                if (newBook == null)
                {
                    _logger.LogError("Book object sent from client is null.");
                    return BadRequest("Book object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid book object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var createdId = await _unitOfWork._bookRepository.AddAsync(newBook);
                var createdBook = await _unitOfWork._bookRepository.GetAsync(createdId);
                _unitOfWork.Commit();
                return CreatedAtAction(nameof(GetByIdAsync), new { id = createdId }, createdBook);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PostBookAsync: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("Put/{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Book updatedBook)
        {
            try
            {
                if (updatedBook == null)
                {
                    _logger.LogError("Book object sent from client is null.");
                    return BadRequest("Book object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid book object sent from client.");
                    return BadRequest("Invalid book object");
                }
                var bookEntity = await _unitOfWork._bookRepository.GetAsync(id);
                if (bookEntity == null)
                {
                    _logger.LogError($"Book with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitOfWork._bookRepository.ReplaceAsync(updatedBook);
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
                var bookEntity = await _unitOfWork._bookRepository.GetAsync(id);
                if (bookEntity == null)
                {
                    _logger.LogError($"Book with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                await _unitOfWork._bookRepository.DeleteAsync(id);
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
