using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

//
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
            _light = new Light(_output);
            _timer = new Timer();
            _BtnPower = new Button();
            _BtnStartCancel = new Button();
            _BtnTimer = new Button();

            //Fake klasserne
            _output = Substitute.For<IOutput>();

            //controller
            _uutCC = new CookController(_timer, _display, _powerTube, _userInterface);

            _userInterface = new UserInterface(_BtnPower, _BtnTimer, _BtnStartCancel, _door, _display, _light, _uutCC);


        }

        

    }
}

