using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Output : IOutput
    {
        public void OnPowerPressed(string line)
        {
            System.Console.WriteLine(line);
        }
        
    }
}