using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Display : IDisplay
    {
        private IOutput myOutput;

        public Display(IOutput output)
        {
            myOutput = output;
        }

        public void ShowTime(int min, int sec)
        {
            myOutput.OnPowerPressed($"Display shows: {min:D2}:{sec:D2}");
        }

        public void ShowPower(int power)
        {
            myOutput.OnPowerPressed($"Display shows: {power} W");
        }

        public void Clear()
        {
            myOutput.OnPowerPressed($"Display cleared");
        }
    }
}