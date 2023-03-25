using System.Collections.Generic;
using MS.BusinessLayer.IServices;
using MS.DataLayer.Abstract;
using MS.DataLayer.Entities;

namespace MS.BusinessLayer.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public Payment AddNewPayment(int subscriptionId, int clientId, Subscription subscription)
        {
            return _paymentRepository.AddNewPayment(subscriptionId, clientId, subscription);
        }

        public Payment AddNewPayment(Subscription subscription, Client client)
        {
            return _paymentRepository.AddNewPayment(subscription, client);
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return _paymentRepository.GetAllPayments();
        }

        public IEnumerable<Payment> GetPaymentByClient(Client client)
        {
            return _paymentRepository.GetPaymentByClient(client);
        }

        public Payment GetPaymentById(int id)
        {
            return _paymentRepository.GetPaymentById(id);
        }
    }
}
