using BookstoreManagerAPI.Communication.Requests;
using BookstoreManagerAPI.Communication.Responses;
using BookstoreManagerAPI.Models;
using BookstoreManagerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreManagerAPI.Controllers;
public class BooksController : BookstoreManagerAPIBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult Create([FromBody] RequestRegisterBookJson request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(new ErrorResponse
            {
                Status = 400,
                Message = "Erro de validação.",
                Errors = errors
            });
        }

        try
        {
            var newBook = new Book
            {
                Title = request.Title,
                Author = request.Author,
                Genre = request.Genre,
                Price = request.Price,
                Stock = request.Stock   
            };

            BooksStore.Add(newBook);

            var response = new ResponseRegisteredBookJson
            {
                Id = newBook.Id,
                Name = newBook.Title
            };

            return Created(string.Empty, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ErrorResponse { Status = 400, Message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new ErrorResponse { Status = 409, Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse
            {
                Status = 500,
                Message = "Ocorreu um erro interno no servidor.",
                Errors = new List<string> { ex.Message }
            });
        }
    }
    [HttpGet]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
        try
        {
            var books = BooksStore.GetAll();
            return Ok(books);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse
            {
                Status = 500,
                Message = "Ocorreu um erro interno no servidor.",
                Errors = new List<string> { ex.Message }
            });
        }
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        try
        {
            var book = BooksStore.GetById(id);
            if (book == null)
            {
                return NotFound(new ErrorResponse
                {
                    Status = 404,
                    Message = "Livro não encontrado."
                });
            }
            return Ok(book);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse
            {
                Status = 500,
                Message = "Ocorreu um erro interno no servidor.",
                Errors = new List<string> { ex.Message }
            });
        }
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult Update([FromRoute] Guid id, [FromBody] RequestUpdateBookJson request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(new ErrorResponse
            {
                Status = 400,
                Message = "Erro de validação.",
                Errors = errors
            });
        }

        try
        {
            var bookToUpdate = new Book
            {
                Id = id, 
                Title = request.Title,
                Author = request.Author,
                Genre = request.Genre,
                Price = request.Price,
                Stock = request.Stock
            };

            BooksStore.Update(bookToUpdate);

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ErrorResponse { Status = 404, Message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ErrorResponse { Status = 400, Message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new ErrorResponse { Status = 409, Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse
            {
                Status = 500,
                Message = "Ocorreu um erro interno no servidor.",
                Errors = new List<string> { ex.Message }
            });
        }
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        try
        {
            BooksStore.Delete(id);

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ErrorResponse { Status = 404, Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse
            {
                Status = 500,
                Message = "Ocorreu um erro interno no servidor.",
                Errors = new List<string> { ex.Message }
            });
        }
    }
}