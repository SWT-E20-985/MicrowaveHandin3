
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Microwave.Test.Integration
{

    [TestFixture]
    public class Step4
    {
        //Display for cookController
        private UserInterface _userInterface;
        private CookController _uutCC;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ITimer _timer;
        private IOutput _output;

        private IButton _BtnTimer;
        private IButton _BtnStartCancel;
        private IButton _BtnPower;
        private IDoor _door;
        private ILight _light;




        [SetUp]
        public void Setup()
        {

            //Display for cookController setup
            _display = Substitute.For<IDisplay>();
            _output = Substitute.For<IOutput>();
            _timer = new Timer();
            _uutCC = new CookController(_timer, _display, _powerTube, _userInterface);


            //Display for userInterface setup
            _powerTube = Substitute.For<IPowerTube>();
            _BtnPower = new Button();
            _BtnStartCancel = new Button();
            _BtnTimer = new Button();
            _light = new Light(_output);
            _display = new Display(_output);
            _userInterface = new UserInterface(_BtnPower, _BtnTimer, _BtnStartCancel, _door, _display, _light, _uutCC);

        }


        #region //diplaying cookController
        [TestCase(60, 1, "Display shows: 00:59")]
        public void Testing_RemainingTime_OnTimerTick(int time, int spendTime, string screen)
        {
            _uutCC.StartCooking(50, time);
            _timer.TimeRemaining.Returns(time - spendTime);        //For hver tick trækkes et sekund fra fra tiden der er tilbage

            _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);
            _output.Received(1).OutputLine(screen);
        }


        public void Testing_Ticks(int time, int spendTime, string screen)
        {
            _uutCC.StartCooking(50, time);


            for (int i = 1; i < spendTime + 1; i++)
            {
                _timer.TimeRemaining.Returns(time - i);
                _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);
            }
            _output.Received(1).OutputLine(screen);
        }
        #endregion
    }
}