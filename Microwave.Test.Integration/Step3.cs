using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

 // jenkins push
namespace Microwave.Test.Integration
{
    [TestFixture]
    public class Step3
    {
        private UserInterface _uut;
        private ICookController _cookController;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ILight _light;
        private Output _output;


        [SetUp]
        public void Setup()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _door = new Door();
            _timer = new Timer();
            _output = Substitute.For<Output>();

            _display = new Display(_output);
            _light = new Light(_output);
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer, _display, _powerTube, _uut);
            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);



        }

        [TestCase(50,60)]
        [TestCase(80, 60)]
        public void Test_PowerTube_TurnsOn(int power, int time)
        {
            _cookController.StartCooking(power,time);
            
            _output.Received(1).OutputLine($"PowerTube works with {power}"); //uden at kunne forklare det, bliver dette printet to gange.

           

        }

        [Test]
        public void Test_PowerTube_TurnsOff()
        {
            _door.Close();
            
            _powerButton.Press();
            
            _timeButton.Press();
            _startCancelButton.Press();

            _startCancelButton.Press();

            _output.Received(1).OutputLine($"PowerTube turned off");

        }


        [TestCase(2)]
        [TestCase(4)]
        [TestCase(6)]
        public void Test_TimerCountDown(int seconds)
        {
            _door.Close();
            
            _powerButton.Press();
            
            _timeButton.Press();
            _startCancelButton.Press();

            
            System.Threading.Thread.Sleep(1000*seconds);

            Assert.AreEqual(_timer.TimeRemaining, 61 - seconds); //denne test gav en uventet fejl i første omgang, af denne grund ændrer vi i Timer klassen
        }


        [Test]
        public void Test_Timer_On()
        {
            _door.Close();

            _powerButton.Press();

            _timeButton.Press();
            _startCancelButton.Press();

            _output.Received(1).OutputLine($"PowerTube turned off");

        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void Test_Timer_On(int minutes)
        {
            _door.Close();

            _powerButton.Press();

            _timeButton.Press();
            _startCancelButton.Press();

            _output.Received(1).OutputLine($"Display shows: {minutes}:0");

        }

        public void Test_Timer_Off()
        {
            _door.Close();

            _powerButton.Press();

            _timeButton.Press();
            _startCancelButton.Press();

            _startCancelButton.Press();

            _output.Received(1).OutputLine($"Display cleared");

        }








    }
}

