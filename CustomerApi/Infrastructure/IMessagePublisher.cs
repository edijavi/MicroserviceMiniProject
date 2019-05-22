using System;
namespace CustomerApi.Infrastructure
{
    public interface IMessagePublisher
    {
        void PublishOrderStatusChangedMessage(int productId, int quantity, string topic);
    }
}
