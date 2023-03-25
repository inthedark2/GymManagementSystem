using System;
using System.Collections.Generic;
using MS.BusinessLayer.IServices;
using MS.DataLayer.Entities;
using MS.DataLayer.Abstract;
using MS.Common.Enums;

namespace MS.BusinessLayer.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public Subscription GetByEndDate(DateTime date)
        {
            return _subscriptionRepository.GetByEndDate(date);
        }

        public Subscription GetBySubscriptionTypeId(int subscriptionTypeId)
        {
            return _subscriptionRepository.GetBySubscriptionTypeId(subscriptionTypeId);
        }

        public IEnumerable<Subscription> GetSubscriptions()
        {
            return _subscriptionRepository.GetSubscriptions();
        }

        //public Subscription AddNewSubscription(Subscription subscription, int cardId, int subscriptionTypeId)
        //{
        //    return _subscriptionRepository.AddNewSubscription(subscription, cardId, subscriptionTypeId);
        //}

        public bool RemoveSubscriptionById(int subscriptionId)
        {
            return _subscriptionRepository.RemoveSubscriptionById(subscriptionId);
        }

        public bool ExtendSubscription(DateTime endDate, int subscriptionId)
        {
            return _subscriptionRepository.ExtendSubscription(endDate, subscriptionId);
        }

        public void ChangeSubscriptionStatus()
        {
            _subscriptionRepository.ChangeSubscriptionStatus();
        }

        public void ChangeStatusIfFrozenDayPassed()
        {
            _subscriptionRepository.ChangeStatusIfFrozenDayPassed();
        }

        public List<string> GetSubscriptionTypes()
        {
            return _subscriptionRepository.GetSubscriptionTypes();
        }

        public SubscriptionType GetSubscriptionTypeByName(string subscriptionType)
        {
            return _subscriptionRepository.GetSubscriptionTypeByName(subscriptionType);
        }

        public Subscription AddNewSubscription(int cardId, int subscriptionTypeId,ESubscriptionStatus status)
        {
            return _subscriptionRepository.AddNewSubscription(cardId, subscriptionTypeId, status);
        }

        public void AddNewSubscription(Subscription subscription)
        {
            _subscriptionRepository.AddNewSubscription(subscription);
        }

        public void StartSubscriptionForApiByCardId(int clubCardId)
        {
            _subscriptionRepository.StartSubscriptionForApiByCardId(clubCardId);
        }
    }
}
