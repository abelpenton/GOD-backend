using System.Threading.Tasks;
using backend.src.GOD.BussineServices.Core;
using backend.src.GOD.BussineServices.Services.Game;
using backend.src.GOD.BussineServices.Services.Player;
using backend.src.GOD.DataAccess.Repositories.Core;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Round;
using backend.src.GOD.DataAccess.Repositories.UnitOfWork;
using backend.src.GOD.Domain.Models;

namespace backend.src.GOD.BussineServices.Services.Round
{
    public class RoundService : BaseBussinesService<Domain.Models.Round, int>, IRoundService
    {
        private IRoundRepository Repository { get; set; }

        private IPlayerService _playerService { get; set; }

        private IGameService _gameService { get; set; }

        public RoundService(IRoundRepository repository, IUnitOfWork unitOfWork, IPlayerService playerService, IGameService gameService) : base(repository, unitOfWork)
        {
            Repository = repository;
            _playerService = playerService;
            _gameService = gameService;
        }

        public async Task<Domain.Models.Game> CompleteRound(Domain.Models.Round round, Move lastMove)
        {
            var playerWinnerNumber = Rules.GetWinner(round.Player1Move, lastMove);
            var currentPlayerWinner = await _playerService.GetPlayerForNumber(playerWinnerNumber);
            var winner = "";

            round.PlayerRoundWinnerName = currentPlayerWinner.PlayerName;
            round.Player2Move = lastMove;
            round = await this.UpdateAsync(round);

            currentPlayerWinner.NumberOfRoundWinner += 1;
            await _playerService.UpdateAsync(currentPlayerWinner);

            var endGame = currentPlayerWinner.NumberOfRoundWinner == 3;
            if (endGame)
            {
                winner = currentPlayerWinner.PlayerName;
            }

            var game = await _gameService.SingleOrDefaultAsync(round.GameId);
            game.Rounds.Add(round);
            game.PlayerGameWinnerName = winner;
            game.EndGame = endGame;

            return await _gameService.UpdateAsync(game);
        }
    }
}
