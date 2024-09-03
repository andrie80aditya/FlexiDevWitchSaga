using FlexiDevWitchSaga.Server.Models;

namespace FlexiDevWitchSaga.Server.Services
{
    public interface IWitchKillCalculator
    {
        KillCalculationResult CalculateAverageKilled(Person personA, Person personB);
    }
}
