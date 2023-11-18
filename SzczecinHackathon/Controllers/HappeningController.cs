using Microsoft.AspNetCore.Mvc;
using SzczecinHackathon.Models;
using SzczecinHackathon.Services;
using SzczecinHackathon.Services.Interfaces;
using SzczecinHackathon.Shared;

namespace SzczecinHackathon.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HappeningController : ControllerBase
    {
        private readonly IHappeningService _happeningService;

        public HappeningController(IHappeningService happeningService)
        {
            _happeningService = happeningService;
        }

        [HttpPost(Name = "CreateHappening")]
        public async Task<ActionResult<ServiceResponse<bool>>> CreateHappening([FromBody] Happening happening)
        {
            var response = await _happeningService.CreateHappening(happening);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("RemoveHappening/{happeningId}", Name = "RemoveHappening")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveHappening(int happeningId)
        {
            var response = await _happeningService.RemoveHappening(happeningId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("AttendHappening/{happeningId}/{userId}", Name = "AttendHappening")]
        public async Task<ActionResult<ServiceResponse<bool>>> AttendHappening(int happeningId, string userId)
        {
            var response = await _happeningService.AttendHappening(happeningId, userId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("UnattendHappening/{happeningId}/{userId}", Name = "UnattendHappening")]
        public async Task<ActionResult<ServiceResponse<bool>>> UnattendHappening(int happeningId, string userId)
        {
            var response = await _happeningService.UnattendHappening(happeningId, userId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("HideHappening/{happeningId}/{userId}", Name = "HideHappening")]
        public async Task<ActionResult<ServiceResponse<bool>>> HideHappening(int happeningId, string userId)
        {
            var response = await _happeningService.HideHappening(happeningId, userId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("GetHappeningWithParticipants/{happeningId}", Name = "GetHappeningWithParticipants")]
        public async Task<ActionResult<ServiceResponse<Happening2>>> GetHappeningWithParticipants(int happeningId)
        {
            var response = await _happeningService.GetHappeningWithParticipants(happeningId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
