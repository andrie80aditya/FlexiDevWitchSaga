using FlexiDevWitchSaga.Server.Models;
using FlexiDevWitchSaga.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexiDevWitchSaga.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WitchController : ControllerBase
    {
        private readonly IWitchKillCalculator _calculator;

        public WitchController(IWitchKillCalculator calculator)
        {
            _calculator = calculator;
        }

        [HttpPost("calculate")]
        public ActionResult<KillCalculationResult> CalculateAverageKilled([FromBody] Person[] people)
        {
            if (people.Length != 2)
            {
                return BadRequest("Exactly two people are required.");
            }

            var result = _calculator.CalculateAverageKilled(people[0], people[1]);
            return Ok(result);
        }
    }
}
