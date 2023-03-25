using System;
using System.Linq;
using System.Web.Mvc;
using MS.DataLayer.Abstract;
using MS.DataLayer.Entities;
using MS.Common.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Collections.Specialized;
using MS.Models.Models;
using MS.Common.Constans;
using System.Configuration;
using MS.Common.Services;
using MS.Localization;
using System.Security.Policy;
using MS.WebSite.Infrastructure;
using System.Globalization;
using PagedList;

namespace MS.WebSite.Controllers
{
    public partial class UserAreaController : Controller
    {
        #region Constructor
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly IPaymentRepository _paymentRepository;
        private static User trainer = null;
        public UserAreaController(IClientRepository clientRepository, IUserRepository userRepository, ISubscriptionRepository subscriptionRepository, ITrainingRepository trainingRepository, IPaymentRepository paymentRepository)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _subscriptionRepository = subscriptionRepository;
            _trainingRepository = trainingRepository;
            _paymentRepository = paymentRepository;
        }
        [Authorize(Roles = "Trainer, Client")]
        public virtual ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Users info
        [ChildActionOnly]
        public virtual ActionResult ClientInfo()
        {
            ClientViewModel model = new ClientViewModel();
            Client client = _clientRepository.GetClientByEmail(User.Identity.Name);
            Subscription subscription = _subscriptionRepository.GetActiveSubscription(client.ClubCardId);
            string photoPath = Url.Content(ConfigurationManager.AppSettings["imagesPath"]) + client.PhotoPath;
            model.Name = client.Name;
            model.Surname = client.Surname;
            model.Gender = ((EGenderType)client.GenderId.Id).GetLocalizedValue();
            model.Email = client.Email;
            model.PhoneNumber = client.PhoneNumber;
            model.ClubCardNumber = client.ClubCardId.ClubCardNumber;
            model.PhotoPath = photoPath;
            if (subscription != null)
            {
                model.SubscriptionEndDate = subscription.EndDate.Value;
                model.SubscriptionFrozenDayLeft = subscription.LeftFrozenDays;
                model.SubscriptionStatus = ((ESubscriptionStatus)subscription.StatusId).GetLocalizedValue();
                model.SubscriptionType = subscription.SubscriptionType.Name;
                model.LeftFreezeDays = subscription.LeftFrozenDays;
                return View(model);
            }
            var nonActiveSubscription = _subscriptionRepository.GetSubscriptionsByClientCard(client.ClubCardId).FirstOrDefault();
            if (nonActiveSubscription != null)
                model.SubscriptionStatus = ((ESubscriptionStatus)nonActiveSubscription.StatusId).GetLocalizedValue();
            else
                model.SubscriptionStatus = Strings.NoSubscriptions;
            return View(model);
        }

        [ChildActionOnly]
        public virtual ActionResult TrainerInfo()
        {
            User trainer = _userRepository.GetUserByEmail(User.Identity.Name);
            string photoPath = Url.Content(ConfigurationManager.AppSettings["imagesPath"]) + trainer.PhotoPath;
            TrainerViewModel model = new TrainerViewModel
            {
                Name = trainer.Name,
                Surname = trainer.Surname,
                QuantityOfClientCurrentDay =
                    _trainingRepository.GetQuntityOfClientForTrainerCurrentDay(DateTime.Today, trainer.Id),
                QuantityOfClientThisWeek =
                    _trainingRepository.GetQuntityOfClientForTrainerCurrentWeek(DateTime.Today, trainer.Id),
                PhoneNumber = trainer.PhoneNumber,
                PhotoPath = photoPath
            };
            return View(model);
        }

        [Authorize]
        public virtual ActionResult EditInfo()
        {
            Client client = _clientRepository.GetClientByEmail(User.Identity.Name);
            EditInfoViewModel model = new EditInfoViewModel();
            string photoPath = Url.Content(ConfigurationManager.AppSettings["imagesPath"]);
            if (client != null)
            {
                photoPath += client.PhotoPath;
                model = new EditInfoViewModel() { Id = client.Id, Name = client.Name, Surname = client.Surname, Email = client.Email, PhoneNumber = client.PhoneNumber, DateOfBirth = DateTime.Now, PhotoPath = photoPath, UserTypeId = (int)EUserTypes.Client };
            }
            User trainer = _userRepository.GetUserByEmail(User.Identity.Name);
            if (trainer != null)
            {
                photoPath += trainer.PhotoPath;
                model = new EditInfoViewModel() { Id = trainer.Id, Name = trainer.Name, Surname = trainer.Surname, Email = trainer.Email, PhoneNumber = trainer.PhoneNumber, DateOfBirth = DateTime.Now, PhotoPath = photoPath, UserTypeId = (int)EUserTypes.Trainer };
            }
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult EditInfo(EditInfoViewModel model)
        {
            string path = Server.MapPath(ConfigurationManager.AppSettings["imagesPath"]);
            if (model.UserTypeId == (int)EUserTypes.Client)
            {
                Client client = _clientRepository.GetClientByEmail(model.Email);
                if (client.Id != model.Id)
                    ModelState.AddModelError("",Strings.EmailExists);
                _clientRepository.EditClientInfo(model.Id, model.Name, model.Surname, model.PhoneNumber, model.Email, model.Photo, path);
            }
            else
            {
                User user = _userRepository.GetTrainerByEmail(model.Email);
                if (user.Id != model.Id)
                    ModelState.AddModelError("", Strings.EmailExists);
                _userRepository.EditUserInfo(model.Id, model.Name, model.Surname, model.PhoneNumber, model.Email, model.Photo, path);
            }
            return RedirectToAction(MVC.UserArea.Index());
        }
        #endregion

        #region Trainer functions 
        //[Authorize(Roles = Constants.Trainer)] 
        public virtual ActionResult Records(int? page,string filter = null)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if(trainer==null)
                trainer = _userRepository.GetUserByEmail(User.Identity.Name);
            var items = from data in filter==null ? _trainingRepository.GetRecordsByTrainerid(trainer.Id) : _trainingRepository.FilterTrainingRecords(filter,trainer.Id)
                        select new RecordViewModel
                        { Name = data.Client.Name, Surname = data.Client.Surname, Email = data.Client.Email, PhoneNumber = data.Client.PhoneNumber, Time = data.TrainingDate.ToString("dd.MM.yyyy HH:mm"), StatusId = data.StatusId, RecordId = data.Id };
            if (filter != null)
            {
                pageSize = 50;
                pageNumber = 1;
                items = from data in _trainingRepository.FilterTrainingRecords(filter, trainer.Id)
                        select new RecordViewModel
                        { Email = data.Client.Email, Name = data.Client.Name, PhoneNumber = data.Client.PhoneNumber, RecordId = data.Id, StatusId = data.StatusId, Surname = data.Client.Surname, Time = data.TrainingDate.ToString("dd-MM-yyyy HH:mm") };
                return View("TrainingRecordsSearch", items.ToPagedList(pageNumber, pageSize));
            }
            return View(items.ToPagedList(pageNumber, pageSize));
        }
        public virtual JsonResult ChangeTrainingStatus(string trainingId,string statusId)
        {
            int training, status;
            Int32.TryParse(trainingId, out training);
            Int32.TryParse(statusId, out status);
            _trainingRepository.ChangeTrainingStatus(training, status);
            return Json(true);
        }
        #endregion

        #region Payments
        [Authorize(Roles = Constants.Client)]
        public virtual ActionResult Buy()
        {
            Client client = _clientRepository.GetClientByEmail(User.Identity.Name);
            if (_subscriptionRepository.GetCountNonExpiredSubscriptions(client.ClubCardId)>=3)
            {
                ViewBag.ErrorMessage = Strings.BySubscriptionError;
                return View();
            }
            BuyViewModel model = new BuyViewModel();            
            ViewBag.ListType = GetLocalizedSubscriptionTypes(_subscriptionRepository.GetAllSubscriptionTypes());
            return View(model);
        }
        [Authorize(Roles = Constants.Client)]
        [HttpPost]
        public virtual ActionResult Buy(BuyViewModel model)
        {
            if (ModelState.IsValid)
            {
                Client client = _clientRepository.GetClientByEmail(User.Identity.Name);
                Subscription subscription = _subscriptionRepository.AddNewSubscription(client.ClubCardId.Id, model.SubscriptionTypeId);
                if (subscription != null)
                {
                    Payment payment = _paymentRepository.AddNewPayment(subscription.SubscriptionType.Id, client.Id, subscription);
                    return RedirectToAction(MVC.UserArea.Pay(payment.Id));
                }
            }
            ViewBag.ListType = GetLocalizedSubscriptionTypes(_subscriptionRepository.GetAllSubscriptionTypes());
            return View(model);
        }
        [Authorize(Roles = Constants.Client)]
        public virtual ActionResult Pay(int id)
        {
            Payment payment = _paymentRepository.GetPaymentById(id);
            PayViewModel model = new PayViewModel() { Amount = payment.SubscriptionType.Price, Description = payment.SubscriptionType.Description, OrderNumber = payment.Id };
            return View(model);
        }
        public virtual ActionResult PayBySubscriptionId(int subscriptionId)
        {
            Payment payment = _paymentRepository.GetPaymentBySubscriptionId(subscriptionId);
            return RedirectToAction(MVC.UserArea.Pay(payment.Id));
        }
        [HttpPost]
        public virtual ActionResult Success()
        {
            var f = Request.Params["SHOPORDERNUMBER"];
            int id;
            Int32.TryParse(f, out id);
            Payment payment = _paymentRepository.GetPaymentById(id);
            _paymentRepository.ChangePaymentStatus(payment, (int)EPaymentStatus.Success);
            _subscriptionRepository.ChangeSubscriptionStatus(payment.Subscription.Id, ESubscriptionStatus.Payed);
            ViewBag.Status = Strings.SuccessfulPayment;
            return View(MVC.UserArea.Views.Status);
        }
        [HttpPost]
        public virtual ActionResult Error()
        {
            var f = Request.Params["SHOPORDERNUMBER"];
            int id;
            Int32.TryParse(f, out id);
            Payment payment = _paymentRepository.GetPaymentById(id);
            _paymentRepository.ChangePaymentStatus(payment, (int)EPaymentStatus.Error);
            ViewBag.Status = Strings.UnsuccessfulPayment;
            return View(MVC.UserArea.Views.Status);
        }

        [HttpPost]
        public virtual JsonResult GetSubscriptionPrice(int id)
        {
            SubscriptionType type = _subscriptionRepository.GetAllSubscriptionTypes().Where(x => x.Id == id).FirstOrDefault();
            return this.Json(new { price = type.Price, description = type.Description }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Client functions
        [Authorize(Roles = Constants.Client)]
        public virtual ActionResult Subscriptions(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            Client client = _clientRepository.GetClientByEmail(User.Identity.Name);
            var model = from data in _subscriptionRepository.GetVisibleSubscriptionsByClubCard(client.ClubCardId.Id) select new  { SubscriptionId = data.Id, EndDate = data.EndDate, StartDate = data.StartDate, SubscriptionStatus = data.StatusId, SubscroptionType = data.SubscriptionType.Id };
            var model2 = model.AsEnumerable().
                Select(t => new SubscriptionsViewModel
                { SubscriptionId = t.SubscriptionId,
                    EndDate = t.EndDate==null?String.Empty:t.EndDate.Value.ToString("dd.MM.yyyy"),
                    StartDate = t.StartDate==null?String.Empty:t.StartDate.Value.ToString("dd.MM.yyyy"),
                    SubscriptionStatus = ((ESubscriptionStatus)t.SubscriptionStatus).GetLocalizedValue(),
                    SubscroptionType =  ((ESubscriptionType)t.SubscroptionType).GetLocalizedValue(),
                    SubscriptionStatusId = t.SubscriptionStatus });
            ViewBag.CountOfWorkedSubscriptions = _subscriptionRepository.GetQuantityOfActiveSubscriptions(client.Id);
            return View(model2.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = Constants.Client)]
        public virtual ActionResult NewTrainingRecord()
        {         
            NewTrainingModel model = new NewTrainingModel() { Trainers = SetTrainersModel() };
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult NewTrainingRecord(NewTrainingModel model)
        {
            if (ModelState.IsValid)
            {
                Client client = _clientRepository.GetClientByEmail(User.Identity.Name);
                User trainer = _userRepository.GetAllTrainers().SingleOrDefault(x => x.Id == model.TrainerId);
                DateTime trainingDate;
                DateTime.TryParseExact(model.TrainingDate, "dd/MM/yyyy", null, DateTimeStyles.None, out trainingDate);
                trainingDate = trainingDate.Date + model.TrainingTime;
                int maxClientPerHour = Int32.Parse(ConfigurationManager.AppSettings["MaxClientsPerHour"]);
                if(_trainingRepository.GetQuantityOfClientForTrainerSpecificHour(trainingDate,trainer.Id)>= maxClientPerHour || _trainingRepository.GetActiveSubscriptionsByClient(client.Id)>=1)
                {
                    if(_trainingRepository.GetActiveSubscriptionsByClient(client.Id) >= 1)
                    {
                        ModelState.AddModelError("", Strings.Error_ManyUserTrainingRecords);
                    }
                    if(_trainingRepository.GetQuantityOfClientForTrainerSpecificHour(trainingDate, trainer.Id) >= maxClientPerHour)
                        ModelState.AddModelError("", Strings.Error_ManyTrainerRecords);        
                    model.Trainers = SetTrainersModel();
                    return View(model);
                }
                _trainingRepository.AddRecord(client, trainer, trainingDate);
                return RedirectToAction(MVC.UserArea.Index());
            }
            model.Trainers = SetTrainersModel();
            return View(model);
        }

        [Authorize(Roles = Constants.Client)]
        public virtual ActionResult MyTrainingRecords(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            Client client = _clientRepository.GetClientByEmail(User.Identity.Name);
            var records = from data in _trainingRepository.GetRecordsByClientId(client.Id) select new { TrainerFullName = data.Trainer.Name + " " + data.Trainer.Surname, TrainingDate = data.TrainingDate, StatusId = data.StatusId,RecordId = data.Id };
            var model = records.AsEnumerable().
                Select(t => new ClientRecordViewModel
                { TrainerFullName = t.TrainerFullName, TrainingDate = t.TrainingDate.ToString("dd.MM.yyyy HH:mm"), StatusId = t.StatusId,StatusName = ((ETrainingRecordStatus)t.StatusId).GetLocalizedValue(),RecordId = t.RecordId });
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        
        [Authorize(Roles = Constants.Client)]
        public virtual ActionResult SubscriptionFreeze()
        {
            Client client = _clientRepository.GetClientByEmail(User.Identity.Name);
            Subscription subscription = _subscriptionRepository.GetActiveSubscription(client.ClubCardId);
            SubscriptionFreezeViewModel model = new SubscriptionFreezeViewModel();
            if (subscription != null)
            {
                ViewBag.IsAcriveSubscription = true;
                model.SubscriptionId = subscription.Id;
                model.From = DateTime.Today.ToString();
                model.To = DateTime.Today.ToString();
                model.LeftFrozenDays = subscription.LeftFrozenDays;
                ViewBag.IsActiveSubscription = true;
                return View(model);
            }
            ViewBag.IsActiveSubscription = false;
            return View();
        }

        [HttpPost]
        public virtual ActionResult SubscriptionFreeze(SubscriptionFreezeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            DateTime from;
            DateTime to;
            DateTime.TryParseExact(model.From, "dd/MM/yyyy", null, DateTimeStyles.None, out from);
            DateTime.TryParseExact(model.To, "dd/MM/yyyy", null, DateTimeStyles.None, out to);
            _subscriptionRepository.FreezeSubscription(model.SubscriptionId, from, to);
            return RedirectToAction(MVC.UserArea.Index());
        }
        public virtual ActionResult ECard()
        {
            Client client = _clientRepository.GetClientByEmail(User.Identity.Name);
            ViewBag.CardNumber = client.ClubCardId.ClubCardNumber;
            return View();
        }
        #endregion

        #region Helpers
        public IQueryable<SubscriptionType> GetLocalizedSubscriptionTypes(IQueryable<SubscriptionType> types)
        {
            foreach (var e in types)
            {
                e.Name = ((ESubscriptionType)e.Id).GetLocalizedValue();
            }
            return types;
        }

        public List<TrainerModelForNewTraining> SetTrainersModel()
        {
            var trainers = from data in _userRepository.GetAllTrainers() select new { Id = data.Id, Name = data.Name, Surname = data.Surname, PhoneNumber = data.PhoneNumber, PhotoPath = data.PhotoPath };
            var trainerModel = trainers.AsEnumerable().Select(t => new TrainerModelForNewTraining { Id = t.Id, Name = t.Name, Surname = t.Surname, PhoneNumber = t.PhoneNumber, PhotoPath = Url.Content(ConfigurationManager.AppSettings["imagesPath"]) + t.PhotoPath });
            return trainerModel.ToList();
        }

        public virtual JsonResult ResumeSubscription(int Id)
        {
            _subscriptionRepository.ResumeSubscription(Id);
            return Json(true);
        }
        public virtual JsonResult ActivateSubscription(int id)
        {
            Client client = _clientRepository.GetClientByEmail(User.Identity.Name);
            if (_subscriptionRepository.ActivateSubscription(id,client.Id))
            {
                return Json(true);
            }
            return Json(false);
        }

        public virtual JsonResult RemoveExpiredSubscription(int id)
        {
            if (_subscriptionRepository.RemoveExpiredSubscription(id))
            {
                return Json(true);
            }
            return Json(false);
        }
        [HttpPost]
        public virtual JsonResult CancelTrainingRecord(int id)
        {
            _trainingRepository.CancelTrainingRecord(id);
            return Json(true);
        }
        #endregion
    }
}