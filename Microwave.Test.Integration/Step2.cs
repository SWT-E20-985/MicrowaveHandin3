using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NSubstitute;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class Step2
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
            _timer = Substitute.For<ITimer>();
            _output = Substitute.For<Output>();

            _display = new Display(_output);
            _light = new Light(_output);
            _powerTube = Substitute.For<IPowerTube>();
            _cookController = new CookController(_timer, _display, _powerTube, _uut);
            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);



        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_DisplayTime_Output(int minutes)
        {
            for (int i = 0; i < minutes; i++)
            {
                _timeButton.Press();
            }

            _output.Received(1).OutputLine($"Display shows: {minutes}:{0}");
            
        }

        [TestCase(1,50)]
        [TestCase(2,100)]
        [TestCase(3,150)]
        public void Test_DisplayPower_Output(int press,int power)
        {
            for (int i = 0; i < press; i++)
            {
                _powerButton.Press();
            }

            _output.Received(1).OutputLine($"Display shows: {power} W");

        }

        [Test]
        public void Test_DisplayClear_DoorOpen_Output()
        {
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _door.Open();

            _output.Received(1).OutputLine($"Display cleared");

        }

        [Test]
        public void Test_DisplayClear_CancelButton_Output()
        {
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _startCancelButton.Press();

            _output.Received(1).OutputLine($"Display cleared");

        }



        //light


        [Test]
        public void Test_Light_DoorOpen_Output()
        {
            _door.Close();
            _door.Open();

            _output.Received(1).OutputLine("Light is turned on");


        }

        [Test]
        public void Test_Light_DoorClosed_Output()
        {
            _door.Open();
            _door.Close();

            _output.Received(1).OutputLine("Light is turned off");


        }

        [Test]
        public void Test_Light_StartButton_Output()
        {
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            

            _output.Received(1).OutputLine("Light is turned on");


        }


        [Test]
        public void Test_Light_CancelButton_Output()
        {
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _startCancelButton.Press();

            _output.Received(1).OutputLine("Light is turned off");


        }
    }
}