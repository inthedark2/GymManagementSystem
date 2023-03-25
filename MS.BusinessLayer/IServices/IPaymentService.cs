using System.Collections.Generic;
using MS.DataLayer.Entities;

namespace MS.BusinessLayer.IServices
{
    public interface IPaymentService
    {
        Payment AddNewPayment(int subscriptionId, int clientId, Subscription subscription);
        Payment AddNewPayment(Subscription subscription, Client client);
        IEnumerable<Payment> GetAllPayments();
        IEnumerable<Payment> GetPaymentByClient(Client client);
        Payment GetPaymentById(int id);
    }
}
