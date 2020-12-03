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
    public class Step1
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
            _timeButton= new Button();
            _startCancelButton = new Button();
            _door = new Door(); 
            _timer = Substitute.For<ITimer>();
            _display = Substitute.For<IDisplay>();
            _output = Substitute.For<Output>();

            _light = Substitute.For<ILight>();
            _powerTube = Substitute.For<IPowerTube>();
            _cookController = new CookController(_timer,_display,_powerTube,_uut);
            _uut = new UserInterface(_powerButton,_timeButton,_startCancelButton,_door,_display,_light,_cookController);
        }

        //Door og Buttons

        [TestCase(1,50)]
        [TestCase(3, 150)]
        [TestCase(5, 250)]
        public void Test_PowerButton_DisplayPower(int press, int power)
        {
            for (int i = 0; i < press; i++)
            {
                _powerButton.Press();
            }
            _display.Received(1).ShowPower(power);
            
        }

        [TestCase(1, 1)]
        [TestCase(3, 3)]
        [TestCase(5, 5)]
        public void Test_SetTimer_DisplayTime(int press, int minutes)
        {
            _door.Close();
            _powerButton.Press();
            for (int i = 0; i < press; i++)
            {
                _timeButton.Press();
            }
            _startCancelButton.Press();

            _display.Received(1).ShowTime(minutes,0);

        }
        [Test]
        public void Test_StartButton_LightOn()
        {
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _light.Received(1).TurnOn();


        }

        [Test]
        public void Test_DoorClose_DoorOpen_LightOn()
        {
            _door.Close();
            _door.Open();
            _light.Received(1).TurnOn();

        }

        [Test]
        public void Test_DoorOpen_DoorClose_LightOff()
        {
            _door.Open();
            _door.Close();
            _light.Received(1).TurnOn();

        }

        //Cookcontroller

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_CookController_TimerStart(int minutes)
        {
            _door.Open();
            _door.Close();
            _powerButton.Press();
            for (int i = 0; i < minutes; i++)
            {
                _timeButton.Press();
            }
            _startCancelButton.Press();

            _timer.Received(1).Start(minutes * 60); //*60 fordi timer tæller ned i sekunder, ikke minutter

        }

        [Test]
        public void Test_CookController_TimerStop()
        {
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _door.Open();

            _timer.Received(1).Stop(); 

        }

        [Test]
        public void Test_CookController_PowerTubeOn()
        {
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();



            _powerTube.Received(1).TurnOn(50); //50 fordi der trykket én gang

        }

        [Test]
        public void Test_CookController_PowerTubeOff()
        {
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();


            _door.Open();


            _powerTube.Received(1).TurnOff(); 

        }


        //extensions

        [Test]
        public void Test_CancelButton_DisplayBlanked()
        {
            _door.Close();
            _powerButton.Press();
            _startCancelButton.Press();

            _display.Received(1).Clear();


        }

        [Test]
        public void Test_OpenDoor_DisplayBlanked()
        {
            _door.Close();
            _powerButton.Press();
            _door.Open();

            _display.Received(1).Clear();


        }


        [Test]
        public void Test_CancelButton__DuringCooking_DisplayBlanked()
        {
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();


            _startCancelButton.Press();

            _display.Received(1).Clear();

        }

        [Test]
        public void Test_OpenDoor__DuringCooking_DisplayBlanked()
        {
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();


            _door.Open();

            _display.Received(1).Clear();


        }

    }
}