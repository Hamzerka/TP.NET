using ASP.Server.Database;
using ASP.Server.Dtos;
using ASP.Server.Models;
using ASP.Server.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace ASP.Server
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mappage existant
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Genres,
                           opt => opt.MapFrom(src => src.Genres.Select(g => g.Name))); // Assumant que Genre a une propriété Name

            // Ajoutez ici d'autres mappages si nécessaire
            // Par exemple, si vous créez un GenreDto et souhaitez l'utiliser pour une représentation plus détaillée des genres:
            // CreateMap<Genre, GenreDto>();
        }
    }
}
