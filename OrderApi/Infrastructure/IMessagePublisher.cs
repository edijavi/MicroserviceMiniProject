using System;
namespace OrderApi.Infrastructure
{
    public interface IMessagePublisher
    {
        void PublishOrderStatusChangedMessage(int productId, int quantity, string topic);
        void PublishCustomerActivity(int customerId, int orderId, string topic);
        void PublishCustomerExist(string topic);
    }
}
