using System;
using System.Collections.Generic;
using MS.DataLayer.Entities;
using MS.Common.Enums;

namespace MS.BusinessLayer.IServices
{
    public interface ISubscriptionService
    {
        Subscription GetByEndDate(DateTime date);
        Subscription GetBySubscriptionTypeId(int subscriptionTypeId);
        IEnumerable<Subscription> GetSubscriptions();
        //Subscription AddNewSubscription(Subscription subscription, int cardId, int subscriptionTypeId);
        bool RemoveSubscriptionById(int subscriptionId);
        bool ExtendSubscription(DateTime endDate, int subscriptionId);
        void ChangeSubscriptionStatus();
        void ChangeStatusIfFrozenDayPassed();
        List<string> GetSubscriptionTypes();
        SubscriptionType GetSubscriptionTypeByName(string subscriptionType);
        Subscription AddNewSubscription(int cardId, int subscriptionTypeId, ESubscriptionStatus status);
        void StartSubscriptionForApiByCardId(int clubCardId);
    }
}
