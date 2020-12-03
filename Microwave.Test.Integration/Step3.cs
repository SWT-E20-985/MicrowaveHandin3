using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class Step3
    {        
        private CookController _uutCC;

        private IUserInterface _userInterface;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ILight _light;
        private IOutput _output;
        private ITimer _timer;
        private IDoor _door;

        private IButton _BtnPower;
        private IButton _BtnTimer;
        private IButton _BtnStartCancel;


        [SetUp]
        public void Setup()
        {

            _powerTube = new PowerTube(_output);
            _display = new Display(_output);

            //Fake klasserne
            _userInterface = Substitute.For<IUserInterface>();
            _timer = Substitute.For<ITimer>();
            _output = Substitute.For<IOutput>();

            //controller
            _uutCC = new CookController(_timer, _display, _powerTube, _userInterface);

        }


        [TestCase(50, 7)]
        [TestCase(350, 50)]
        [TestCase(700, 100)]
        public void Testing_power_turnOn(int power, int percent)
        {
            //_uutCC.StartCooking(power)

        }
    }
}

