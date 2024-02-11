using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Models;
using AutoMapper;
using ASP.Server.Dtos;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;

namespace ASP.Server.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly IMapper _mapper;

        public BookController(LibraryDbContext libraryDbContext, IMapper mapper)
        {
            _libraryDbContext = libraryDbContext;
            _mapper = mapper;
        }

        // Récupère une liste paginée de livres avec la possibilité de filtrer par genres
        [HttpGet("GetBooks")]
        public ActionResult<IEnumerable<BookDto>> GetBooks([FromQuery] List<int> genreIds, [FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var query = _libraryDbContext.Books.AsQueryable();

            if (genreIds.Any())
            {
                query = query.Where(b => b.Genres.Any(g => genreIds.Contains(g.Id)));
            }

            var books = query
                .Include(b => b.Genres)
                .Skip(offset)
                .Take(limit)
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .ToList();

            return Ok(books);
        }

        // Récupère les détails complets d'un livre spécifié par son ID
        [HttpGet("GetBook/{id}")]
        public ActionResult<BookDto> GetBook(int id)
        {
            var book = _libraryDbContext.Books
                .Where(b => b.Id == id)
                .Include(b => b.Genres)
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // Récupère la liste de tous les genres disponibles
        [HttpGet("GetGenres")]
        public ActionResult<IEnumerable<GenreDto>> GetGenres()
        {
            var genres = _libraryDbContext.Genre
                .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
                .ToList();

            return Ok(genres);
        }
    }
}
