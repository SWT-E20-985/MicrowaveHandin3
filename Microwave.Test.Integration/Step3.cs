

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
        
        private IUserInterface  _userInterface;
        private CookController _uutCC;

        private IPowerTube      _powerTube;
        private IDisplay        _display;
        private ITimer          _timer;
        private IOutput         _output;

        ////Buttons
        //private IButton _btnPower;
        //private IButton _btnTimer;
        //private IButton _btnStartCancel;



        [SetUp]
        public void Setup()
        {

            _userInterface = Substitute.For<IUserInterface>();
            _display = Substitute.For<IDisplay>();
            _output = Substitute.For<IOutput>();

            _powerTube = new PowerTube(_output);
            _timer = new Timer();

            //_door = new Door();
            //_light = new Light(_output);

            //_btnPower = new Button();
            //_btnTimer = new Button();
            //_btnStartCancel = new Button();

            _uutCC = new CookController(_timer, _display, _powerTube, _userInterface);
           // _uutInterface = new UserInterface(_btnPower, _btnTimer, _btnStartCancel, _door, _display, _light, _uutCC);

        }


        //private void set_state_power()
        //{
        //    _btnPower.Press();
        //}

        //private void state_start_cooking()
        //{
        //    set_state_power();              //Power skal være slået til før man kan lave mad
        //    _btnPower.Press();
        //}

        [TestCase(60, 1, "Display shows: 00:59")]
        public void Testing_TimeRemaining_On_Ticking(int time, int tickTok, string screen)
        {
            _uutCC.StartCooking(50, time);
            _timer.TimeRemaining.Returns(time - tickTok);
            _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);
            _output.Received(1).OutputLine(screen);
        }


        public void Testing_Ticks(int time, int tickTok, string screen)
        {
            _uutCC.StartCooking(50, time);

            for (int i = 1; i < tickTok + 1; i++)
            {
                _timer.TimeRemaining.Returns(time - i);
                _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);
            }
            _output.Received(1).OutputLine(screen);
        }
    }
}