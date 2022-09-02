using API.RequestModel;
using Domain.Abstraction;
using Domain.Implementation;
using CQRS.Query.Abstraction;
using Microsoft.AspNetCore.Mvc;
using CQRS.Command.Abstraction;
using Application.Queries.GetFilmByName;
using Application.Commands.AddFilmToWatchList;
using Application.Commands.ChangeWatchedStatus;
using Application.Queries.GetWatchlistByUserId;

namespace API.Controllers
{
    public class FilmController : BaseController
    {
        public FilmController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus) { }

        [HttpGet("SearchByName")]
        public async Task<IActionResult> SearchByName([FromQuery] GetFilmModel model, CancellationToken cancellationToken = default)
        {
            return await ProccessQueryAsync<GetFilmByName, GetFilmModel, IEnumerable<SearchedFilm>>(model, cancellationToken);
        }
        [HttpPost("AddInWatchList")]
        public async Task<IActionResult> AddInWatchList([FromBody] AddFilmInWatchListModel model, CancellationToken cancellationToken = default)
        {
            return await ProccessCommandAsync<AddFilmToWatchList, AddFilmInWatchListModel>(model, cancellationToken);
        }
        [HttpPost("ChangeFilmStatusInWatched")]
        public async Task<IActionResult> ChangeFilmStatusInWatched([FromBody] ChangeWatchedStatusModel model, CancellationToken cancellationToken = default)
        {
            return await ProccessCommandAsync<ChangeWatchedStatus, ChangeWatchedStatusModel>(model, cancellationToken);
        }
        [HttpGet("GetWatchListByUserId")]
        public async Task<IActionResult> GetWatchListByUserId([FromQuery] GetWatchlistByUserIdModel model, CancellationToken cancellationToken = default)
        {
            return await ProccessQueryAsync<GetWatchlistByUserId, GetWatchlistByUserIdModel, IEnumerable<IWatchlist>>(model, cancellationToken);
        }
    }
}