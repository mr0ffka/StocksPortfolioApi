using Microsoft.AspNetCore.Mvc;
using StocksPortfolio.Application.Features.Portfolios.Dtos;
using StocksPortfolio.Application.Interfaces.Services;

namespace StocksPortfolio.Controllers
{
    [ApiController]
    [Route("api/portfolio/")]
    public class PortfolioController(IPortfolioService portfolioService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PortfolioDetailsDto>> Get()
        {
            var result = await portfolioService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<PortfolioDetailsDto>> GetById(string id, [FromQuery] string? currency)
        {
            var result = await portfolioService.GetByIdAsync(id, currency);
            return Ok(result);
        }

        [HttpGet("{id:length(24)}/value")]
        public async Task<ActionResult<decimal>> GetValues(string id, [FromQuery] string currency = "USD")
        {
            var result = await portfolioService.GetValueAsync(id, currency);
            return Ok(result);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            await portfolioService.DeleteAsync(id);
            return NoContent();
        }
    }
}
