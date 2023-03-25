using System.Web.Mvc;
using MS.DataLayer.Entities;
using MS.WebSite.Services;
using System.Web.Security;
using MS.DataLayer.Abstract;
using MS.Localization;
using MS.Models.Models;

namespace MS.WebSite.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;

        public HomeController(IClientRepository clientRepository, IUserRepository userRepository)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
        }

        public virtual ActionResult Index()
        {
            ViewBag.Title = Strings.Home;
            return View();
        }

        public virtual ActionResult ClientRegister()
        {
            ViewBag.Title = Strings.Registration;
            return View();
        }

        [HttpPost]
        public virtual ActionResult ClientRegister(CheckClientModel model)
        {
            Client client = _clientRepository.GetClientByEmailAndCard(model.Email, model.ClubCard);
            if (client != null)
            {
                if (model.VerivicationCode != null || model.Password != null)
                {
                    if (model.Password != null)
                    {
                        _clientRepository.RegisterClient(model.Email, model.Password);
                    }
                    if (client.ValidationCode == model.VerivicationCode)
                    {
                        ViewBag.ShowVerificationField = false;
                        ViewBag.ShowPasswordField = true;
                        return View(model);
                    }
                }
                else
                {
                    var validationCode = MailSender.GenerateValidationCode();
                    if (_clientRepository.SetVerificationCode(model.Email, validationCode))
                    {
                        MailSender.Send(model.Email, validationCode);
                    }
                    else
                    {
                        ViewBag.Message = Strings.Error_SomethingWrong;
                        return View(model);
                    }
                    ViewBag.ShowVerificationField = true;
                    return View(model);
                }
            }
            return RedirectToAction(MVC.Home.Login());
            ViewBag.Message = Strings.Error_inputErrorData;
            return View(model);
        }
        public virtual ActionResult Login()
        {
            ViewBag.Title = Strings.Login;
            LoginViewModel model = new LoginViewModel { IsClient = true };
            return View(model);
        }
        [HttpPost]
        public virtual ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsClient)
                {
                    if (_clientRepository.CheckClientPassword(model.Email, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        return RedirectToAction(MVC.UserArea.Index());
                    }
                    else
                        ModelState.AddModelError("", Strings.Error_LoginAndPasswordIncorect);
                }
                else
                {
                    if (_userRepository.CheckUserPassword(model.Email, model.Password) && _userRepository.CheckIsTrainer(model.Email))
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        return RedirectToAction(MVC.UserArea.Index());
                    }
                    else
                        ModelState.AddModelError("", Strings.Error_LoginAndPasswordIncorect);
                }           
            }
            return View(model);
        }

        [Authorize]
        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(MVC.Home.Index());
        }   
    }
}