using System.Linq;
using AutoMapper;
using backend.src.GOD.Domain.Models;
namespace backend.src.GOD.Api.Models
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            this.CreateMap<Player, PlayerDto>().ReverseMap();
            this.CreateMap<Domain.Models.Round, RoundDto>().ReverseMap();
            this.CreateMap<NewRoundDto, Domain.Models.Round>().AfterMap((src, dest) =>
            {
                dest.Player1Move = (Move)src.Move;
                dest.GameId = src.GameId;
            }).ReverseMap();
            this.CreateMap<Game, GameDto>().ReverseMap();
        }
    }
}
