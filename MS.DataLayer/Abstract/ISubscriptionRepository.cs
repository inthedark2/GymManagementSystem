using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.DataLayer.Entities;
using MS.Common.Enums;

namespace MS.DataLayer.Abstract
{
    public interface ISubscriptionRepository : IRepository
    {
        Subscription GetByEndDate(DateTime date);
        Subscription GetBySubscriptionTypeId(int subscriptionTypeId);
        IEnumerable<Subscription> GetSubscriptions();
        //Subscription AddNewSubscription(Subscription subscription, int cardId, int subscriptionTypeId);
        Subscription AddNewSubscription(int cardId, int subscriptionTypeId, ESubscriptionStatus status = ESubscriptionStatus.New);
        bool RemoveSubscriptionById(int subscriptionId);
        bool ExtendSubscription(DateTime endDate, int subscriptionId);
        Subscription GetActiveSubscription(ClubCard cardId);
        IQueryable<SubscriptionType> GetAllSubscriptionTypes();
        bool ChangeSubscriptionStatus(int subscriptionId, ESubscriptionStatus status);
        List<Subscription> GetSubscriptionsByClientCard(ClubCard card);
        void FreezeSubscription(int subscriptionId, DateTime from, DateTime to);
        Subscription GetSubscriptionById(int subscriptionId);
        void ChangeSubscriptionStatus();
        void ResumeSubscription(int subscriptionId);
        int GetQuantityOfActiveSubscriptions(int clientId);
        bool ActivateSubscription(int subscriptionId, int clientId);
        void ChangeStatusIfFrozenDayPassed();
        bool RemoveExpiredSubscription(int subscriptionId);
        List<Subscription> GetVisibleSubscriptions();
        List<Subscription> GetVisibleSubscriptionsByClubCard(int clubCardId);
        int GetCountNonExpiredSubscriptions(ClubCard card);
        List<string> GetSubscriptionTypes();
        SubscriptionType GetSubscriptionTypeByName(string subscriptionType);
        void AddNewSubscription(Subscription subscription);
        void StartSubscriptionForApiByCardId(int clubCardId);
    }
}
