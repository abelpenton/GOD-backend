﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.src.GOD.Api.Models;
using backend.src.GOD.Api.Models.Round;
using backend.src.GOD.BussineServices.Services.Game;
using backend.src.GOD.BussineServices.Services.Player;
using backend.src.GOD.BussineServices.Services.Round;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace backend.src.GOD.Api
{
    [AllowAnonymous]
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/v1/[controller]/[action]")]
    public class GameController : Controller
    {
        protected IMapper _mapper { get; set; }

        protected IGameService _gameService { get; set; }

        protected IPlayerService _playerService { get; set; }

        protected IRoundService _roundService { get; set; }

        private ILogger _logger { get; set; }

        public GameController(IGameService gameService, IPlayerService playerService, IRoundService roundService, IMapper mapper)
        {
            _gameService = gameService;
            _playerService = playerService;
            _roundService = roundService;
            _mapper = mapper;
            _logger = Log.ForContext<GameController>();

        }


        [HttpGet("{player}")]
        public async Task<IActionResult> GetPlayer([FromRoute] int player)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_mapper.Map<Domain.Models.Player, PlayerDto>(await _playerService.GetPlayerByNumber(player)));
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGame([FromRoute] int gameId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_mapper.Map<Domain.Models.Game, GameDto>(await _gameService.SingleOrDefaultAsync(gameId)));
        }


        [HttpGet("{playerName}")]
        public async Task<IActionResult> GetStatics([FromRoute] string playerName)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var statistics = await _gameService.Statistics(playerName);
          
            return Ok(statistics.Select(g => _mapper.Map<Domain.Models.Game, GameDto>(g)));

        }

        [HttpPost]
        public async Task<IActionResult> NewGame([FromBody] NewGameDto newGame)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            await _playerService.AddPlayer(_mapper.Map<PlayerDto, Domain.Models.Player>(new PlayerDto
            {
                PlayerName = newGame.Player1,
                PlayerNumber = 1
            }));

            await _playerService.AddPlayer(_mapper.Map<PlayerDto, Domain.Models.Player>(new PlayerDto
            {
                PlayerName = newGame.Player2,
                PlayerNumber = 2
            }));


            var game = await _gameService.AddAsync(_mapper.Map<GameDto, Domain.Models.Game>(new GameDto
            {
                EndGame = false,
                PlayerGameWinnerName = ""
            }));

            return Ok(_mapper.Map<Domain.Models.Game, GameDto>(game));
        }


        [HttpPost]
        public async Task<IActionResult> NewRound([FromBody] NewRoundDto round)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_mapper.Map<Domain.Models.Round, RoundDto>(await _roundService.AddAsync(_mapper.Map<NewRoundDto, Domain.Models.Round>(round))));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRound([FromRoute] int id, [FromBody] EditRoundDto round)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var r = await _roundService.SingleOrDefaultAsync(id);

            if (r != null)
            {
                var result = _mapper.Map<Domain.Models.Game, GameDto>(await _roundService.CompleteRound(r, (Domain.Models.Move)round.lastMove));

                var roundsGame = (await _roundService.ReadAllAsync(ro => ro.GameId == result.Id)).ToList();


                result.Rounds = roundsGame.Select(ro => _mapper.Map<Domain.Models.Round, RoundDto>(ro)).ToList();

                return Ok(result);
            }
            return BadRequest();
        }
    }
}
