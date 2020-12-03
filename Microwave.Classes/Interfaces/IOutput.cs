using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Classes.Interfaces
{
    public interface IOutput
    {
        void OnPowerPressed(string line);
        void OutputLine(string output);
    }
}
