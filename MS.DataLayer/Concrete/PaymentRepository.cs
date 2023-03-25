using MS.DataLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using MS.DataLayer.Entities;
using MS.Common.Enums;

namespace MS.DataLayer.Concrete
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ManagmentSystemContext _context;

        public PaymentRepository(ManagmentSystemContext context)
        {
            _context = context;
        }

        public Payment AddNewPayment(int subscriptionId, int clientId, Subscription subscription)
        {
            var client = _context.Clients.FirstOrDefault(x => x.Id == clientId);
            var type = _context.SubscriptionTypes.FirstOrDefault(x => x.Id == subscriptionId);
            var subsc = _context.Subscriptions.FirstOrDefault(x => x.Id == subscription.Id);
            var payment = new Payment() { Client = client, PaymentDate = DateTime.Now, SubscriptionType = type,Subscription = subsc,PaymentStatusId = (int)EPaymentStatus.New };

            _context.Payments.Add(payment);
            _context.SaveChanges();
            return payment;
        }

        public Payment AddNewPayment(Subscription subscription, Client client)
        {
            var payment = new Payment() { Client = client, PaymentDate = DateTime.Now, SubscriptionType = subscription.SubscriptionType,Subscription = subscription,PaymentStatusId = (int)EPaymentStatus.New };

            _context.Payments.Add(payment);
            _context.SaveChanges();
            return payment;
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        public IEnumerable<Payment> GetPaymentByClient(Client client)
        {
            return GetAllPayments().Where(x => x.Client == client);
        }

        public Payment GetPaymentById(int id)
        {
            return GetAllPayments().FirstOrDefault(x => x.Id == id);
        }

        public Payment GetPaymentBySubscriptionId(int subscriptionId)
        {
            return _context.Payments.SingleOrDefault(x => x.Subscription.Id == subscriptionId);
        }

        public Payment ChangePaymentStatus(Payment payment,int statusId)
        {
            payment.PaymentStatusId = statusId;
            _context.SaveChanges();
            return payment;
        }

    }
}
