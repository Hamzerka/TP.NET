using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Dtos;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.Server.Api
{
    [Route("/api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly IMapper _mapper; // Assurez-vous d'injecter AutoMapper

        public GenreController(LibraryDbContext libraryDbContext, IMapper mapper)
        {
            _libraryDbContext = libraryDbContext;
            _mapper = mapper;
        }

        // Utilisez le verbe HTTP Get pour cette action
        [HttpGet]
        public async Task<ActionResult<List<GenreDto>>> GetGenres()
        {
            var listGenres = await _libraryDbContext.Genre
                                .Include(g => g.Books)
                                .ToListAsync();

            // Utilisez AutoMapper pour simplifier la conversion
            var genreDtos = _mapper.Map<List<GenreDto>>(listGenres);

            return genreDtos;
        }
    }
}
