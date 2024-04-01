using AuctionsAPI.Cache;
using AuctionsAPI.Contracts.Queries;
using AuctionsAPI.Contracts.Responses;
using AuctionsAPI.Helpers;
using AuctionsAPI.Interfaces;
using AuctionsAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuctionsAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auctions")]
    [ApiVersion("0.1")]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionsService _auctionsService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly ILogger<AuctionsController> _logger;

        public AuctionsController(IAuctionsService auctionsService, IMapper mapper, IUriService uriService, ILogger<AuctionsController> logger) 
        {
            _auctionsService = auctionsService;
            _mapper = mapper;
            _uriService = uriService;
            _logger = logger;
        }

        [HttpGet()]
        [Cached(600)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAuctionsQuery query, [FromQuery] PaginationQuery paginationQuery, CancellationToken cancellation) 
        {
            try
            {
                _logger.LogInformation("GetAll Enpoint executing");

                var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
                var filter = _mapper.Map<GetAllAuctionsFilter>(query);
                var items = await _auctionsService.GetAllAsync(cancellation, filter, pagination);
                var itemsResponce = _mapper.Map<IEnumerable<AuctionsResponse>>(items);

                if (pagination == null || pagination.Cursor < 1 || pagination.PageSize < 1)
                {
                    return Ok(new PagedResponce<AuctionsResponse>(itemsResponce));
                }

                var paginationResponce = PaginationHelpers.CreatePaginatedResponce(_uriService, pagination, itemsResponce);

                return Ok(paginationResponce);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
            catch (TaskCanceledException) 
            {
                _logger.LogInformation("Task canceled");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while user tried to get all endpont");
                throw;
            }
        }

        [HttpGet("{id}")]
        [Cached(600)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery]int id, CancellationToken cancellation) 
        {
            try
            {
                _logger.LogInformation("Get Enpoint executing");

                var iAptem = await _auctionsService.GetAsync(id, cancellation);
                var itemResponce = _mapper.Map<AuctionsResponse>(item);
                
                return Ok(itemResponce);
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("Task canceled");
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while user tried to get endpoint");
                throw;
            }
        }
    }
}
