using MS.DataLayer.Entities;
using System.Collections.Generic;

namespace MS.DataLayer.Abstract
{
    public interface IPaymentRepository
    {
        Payment AddNewPayment(int subscriptionId, int clientId,Subscription subscription);
        Payment AddNewPayment(Subscription subscription, Client client);
        IEnumerable<Payment> GetAllPayments();
        IEnumerable<Payment> GetPaymentByClient(Client client);
        Payment GetPaymentById(int id);
        Payment GetPaymentBySubscriptionId(int subscriptionId);
        Payment ChangePaymentStatus(Payment payment, int statusId);
    }
}
