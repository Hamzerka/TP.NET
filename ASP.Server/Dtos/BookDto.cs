using ASP.Server.Dtos; // Assurez-vous d'inclure la référence correcte
using System.Collections.Generic;

namespace ASP.Server.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public List<GenreDto> Genres { get; set; } = new List<GenreDto>();
    }
}
