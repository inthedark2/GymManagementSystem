using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.DataLayer.Abstract;
using MS.DataLayer.Entities;
using MS.Common.Enums;

namespace MS.DataLayer.Concrete
{
    public class SubscriptionRepository : BaseRepository, ISubscriptionRepository
    {
        private readonly ManagmentSystemContext _context;

        public SubscriptionRepository(ManagmentSystemContext context)
        {
            _context = context;
        }
        public Subscription GetByEndDate(DateTime date)
        {
            return _context.Subscriptions.SingleOrDefault(x => x.EndDate == date);
        }

        public Subscription GetBySubscriptionTypeId(int subscriptionTypeId)
        {
            return _context.Subscriptions.SingleOrDefault(x => x.SubscriptionType.Id == subscriptionTypeId);
        }

        public IEnumerable<Subscription> GetSubscriptions()
        {
            return _context.Subscriptions.ToList();
        }

        //public Subscription AddNewSubscription(Subscription subscription, int cardId, int subscriptionTypeId)
        //{
        //    if (subscriptionTypeId == subscription.SubscriptionType.Id) return null;

        //    subscription.ClubCard.Id = cardId;
        //    subscription.SubscriptionType.Id = subscriptionTypeId;
        //    subscription.IsVisible = true;
        //    _context.Subscriptions.Add(subscription);
        //    _context.SaveChanges();
        //    return subscription;
        //}

        public bool RemoveSubscriptionById(int subscriptionId)
        {
            var subscription = GetBySubscriptionTypeId(subscriptionId);
            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ExtendSubscription(DateTime endDate, int subscriptionId)
        {
            var subscription = _context.Subscriptions.FirstOrDefault(x => x.SubscriptionType.Id == subscriptionId);
            if (subscription != null)
            {
                subscription.EndDate = endDate;
                //todo update no save item
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Subscription GetActiveSubscription(ClubCard cardId)
        {
            return this.GetSubscriptions().SingleOrDefault(x => x.StatusId == (int)ESubscriptionStatus.Active&&x.ClubCard.Id==cardId.Id);
        }
        public SubscriptionType GetSubscriptionTypeById(ESubscriptionType type)
        {
            return _context.SubscriptionTypes.SingleOrDefault(x => x.Id == (int)type);
        }

        public List<string> GetSubscriptionTypes()
        {
            return _context.SubscriptionTypes.Select(X => X.Name).ToList();
        }

        public IQueryable<SubscriptionType> GetAllSubscriptionTypes()
        {
            return from data in _context.SubscriptionTypes select data;
        }

        public Subscription AddNewSubscription(int cardId, int subscriptionTypeId, ESubscriptionStatus status = ESubscriptionStatus.New)
        {
            ClubCard card = _context.ClubCards.Where(x => x.Id == cardId).FirstOrDefault();
            SubscriptionType type = GetSubscriptionTypeById((ESubscriptionType)subscriptionTypeId);
            Subscription subscription = null;
            if (card != null && type != null)
            {
                subscription = new Subscription() { ClubCard = card, StatusId = (int)status, SubscriptionType = type, LeftFrozenDays = type.MaxFrozenDays, IsVisible = true };
                _context.Subscriptions.Add(subscription);
                _context.SaveChanges();
            }
            return subscription;
        }

        public bool ChangeSubscriptionStatus(int subscriptionId, ESubscriptionStatus status)
        {
            Subscription subscription = GetSubscriptions().Where(x => x.Id == subscriptionId).FirstOrDefault();
            if (subscription == null)
                return false;
            subscription.StatusId = (int)status;
            _context.SaveChanges();
            return true;
        }

        public List<Subscription> GetSubscriptionsByClientCard(ClubCard card)
        {
            return GetSubscriptions().Where(x => x.ClubCard.Id == card.Id).OrderBy(x => x.StatusId).ToList();
        }
        public Subscription GetSubscriptionById(int subscriptionId)
        {
            return _context.Subscriptions.SingleOrDefault(x => x.Id == subscriptionId);
        }

        public void FreezeSubscription(int subscriptionId, DateTime from, DateTime to)
        {
            Subscription subscription = GetSubscriptionById(subscriptionId);
            try
            {
                subscription.FrozenFrom = from;
                subscription.FrozenTo = to;
                subscription.StatusId = (int)ESubscriptionStatus.Frozen;
                int quantityOfDays = Convert.ToInt32((to - from).TotalDays) + 1;
                DateTime endDate = subscription.EndDate.Value.AddDays(quantityOfDays);
                subscription.LeftFrozenDays = subscription.LeftFrozenDays - quantityOfDays;
                subscription.EndDate = endDate;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "Subscription: " + subscription.Id + "From: " + from + " To: " + to);
            }
        }

        public void ChangeSubscriptionStatus()
        {
            var expireSubscription = _context.Subscriptions.Where(x => x.EndDate.Value < DateTime.Today).ToList();
            foreach (var subscription in expireSubscription)
            {
                subscription.StatusId = (int)ESubscriptionStatus.Expired;
                _context.SaveChanges();
            }
        }

        public void ResumeSubscription(int subscriptionId)
        {
            Subscription subscription = GetSubscriptionById(subscriptionId);
            if (subscription != null && subscription.StatusId == (int)ESubscriptionStatus.Frozen)
            {
                TimeSpan difference = subscription.FrozenTo.Value - subscription.FrozenFrom.Value;
                var days = difference.TotalDays;
                int frozenDays = subscription.FrozenTo.Value.Date.Subtract(subscription.FrozenFrom.Value.Date).Days + 1;
                int realFrozenDays = (DateTime.Today - subscription.FrozenFrom.Value).Days;
                if (DateTime.Today < subscription.FrozenFrom.Value)
                    realFrozenDays = 0;
                subscription.FrozenFrom = null;
                subscription.FrozenTo = null;
                subscription.StatusId = (int)ESubscriptionStatus.Active;
                subscription.LeftFrozenDays += (frozenDays - realFrozenDays);
                subscription.EndDate = subscription.EndDate.Value.AddDays(-(frozenDays - realFrozenDays));
                _context.SaveChanges();
            }
        }

        public int GetQuantityOfActiveSubscriptions(int clientId)
        {
            Client client = _context.Clients.SingleOrDefault(x => x.Id == clientId);
            var subscriptions = GetSubscriptionsByClientCard(client.ClubCardId);
            return subscriptions.Where(x => x.StatusId != (int)ESubscriptionStatus.Expired && x.StatusId != (int)ESubscriptionStatus.New && x.StatusId != (int)ESubscriptionStatus.Payed).Count();
        }

        public bool ActivateSubscription(int subscriptionId, int clientId)
        {
            Client client = _context.Clients.SingleOrDefault(x => x.Id == clientId);
            var subscriptions = GetSubscriptionsByClientCard(client.ClubCardId);
            if (subscriptions.Where(x => x.StatusId == (int)ESubscriptionStatus.Active).Count() == 0)
            {
                var subsc = GetSubscriptionById(subscriptionId);
                subsc.StatusId = (int)ESubscriptionStatus.Active;
                subsc.StartDate = DateTime.Today;
                subsc.EndDate = DateTime.Today.AddMonths(GetAllSubscriptionTypes().SingleOrDefault(x => x.Id == subsc.SubscriptionType.Id).MonthCount);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void ChangeStatusIfFrozenDayPassed()
        {
            var subscriptions = GetSubscriptions().Where(x=>x.FrozenTo!=null&&x.StatusId== (int)ESubscriptionStatus.Frozen);
            foreach (var item in subscriptions)
            {
                if (item.FrozenTo.Value.Date < DateTime.Today)
                {
                    item.StatusId = (int)ESubscriptionStatus.Active;
                    _context.SaveChanges();
                }
            }
        }

        public bool RemoveExpiredSubscription(int subscriptionId)
        {
            var subscription = GetSubscriptionById(subscriptionId);
            if (subscription.StatusId == (int)ESubscriptionStatus.Expired || subscription.StatusId == (int)ESubscriptionStatus.New)
            {
                subscription.IsVisible = false;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Subscription> GetVisibleSubscriptions()
        {
            return _context.Subscriptions.Where(x => x.IsVisible == true).ToList();
        }

        public List<Subscription> GetVisibleSubscriptionsByClubCard(int clubCardId)
        {
            return GetVisibleSubscriptions().Where(x => x.ClubCard.Id == clubCardId).ToList();
        }

        public int GetCountNonExpiredSubscriptions(ClubCard card)
        {
            var subscriptions = GetSubscriptionsByClientCard(card);
            return subscriptions.Where(x => x.StatusId != (int)ESubscriptionStatus.Expired).Count();
        }

        public SubscriptionType GetSubscriptionTypeByName(string subscriptionType)
        {
            return _context.SubscriptionTypes.Where(x => x.Name == subscriptionType).FirstOrDefault();
        }

        public void AddNewSubscription(Subscription subscription)
        {
            subscription.IsVisible = true;
            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();
        }

        public void StartSubscriptionForApiByCardId(int clubCardId)
        {
            var subscription = GetSubscriptionById(clubCardId);
            subscription.StartDate = DateTime.Now;
            subscription.EndDate = DateTime.Today.AddMonths(GetAllSubscriptionTypes().SingleOrDefault(x => x.Id == subscription.SubscriptionType.Id).MonthCount);

            _context.SaveChanges();
        }
    }
}
