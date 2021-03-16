using Application.BookLists;
using Application.BookLists.Commands.CreateBookList;
using Application.BookLists.Commands.DeleteBookList;
using Application.BookLists.Queries.GetBookList;
using Application.BookLists.Queries.GetOwnedBookList;
using Application.BookLists.Queries.GetWishlist;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookManagement.Controllers
{
    [ApiController]
    [Route("api/booklists")]
    public class BookListsController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookListDto>> GetList(int id)
        {
            try
            {
                var response = await Mediator.Send(new GetBookListQuery { Id = id });
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("owned/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookListDto>> GetOwned(int id)
        {
            try
            {
                var response = await Mediator.Send(new GetOwnedBookListQuery { Id = id });
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("wishlist/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookListDto>> GetWishlist(int id)
        {
            try
            {
                var response = await Mediator.Send(new GetWishlistQuery { Id = id });
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> Create()
        {
            return await Mediator.Send(new CreateBookListCommand());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteBookListCommand { Id = id });
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
