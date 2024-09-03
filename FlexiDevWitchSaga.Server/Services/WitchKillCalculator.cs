using FlexiDevWitchSaga.Server.Models;

namespace FlexiDevWitchSaga.Server.Services
{
    public class WitchKillCalculator : IWitchKillCalculator
    {
        public KillCalculationResult CalculateAverageKilled(Person personA, Person personB)
        {
            int birthYearA = personA.YearOfDeath - personA.AgeOfDeath;
            int birthYearB = personB.YearOfDeath - personB.AgeOfDeath;

            if (birthYearA <= 0 || birthYearB <= 0 || personA.AgeOfDeath < 0 || personB.AgeOfDeath < 0)
            {
                return new KillCalculationResult { AverageKilled = -1, IsValid = false };
            }

            int killedA = CalculateKilledForYear(birthYearA);
            int killedB = CalculateKilledForYear(birthYearB);

            double average = (killedA + killedB) / 2.0;

            return new KillCalculationResult { AverageKilled = average, IsValid = true };
        }

        private int CalculateKilledForYear(int year)
        {
            if (year == 1) return 1;
            if (year == 2) return 2;

            int a = 1, b = 1, sum = 2;
            for (int i = 3; i <= year; i++)
            {
                int c = a + b;
                sum += c;
                a = b;
                b = c;
            }

            return sum;
        }
    }
}
