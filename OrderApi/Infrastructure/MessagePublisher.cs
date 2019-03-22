﻿using System;
using EasyNetQ;
using SharedModels;

namespace OrderApi.Infrastructure
{
    public class MessagePublisher : IMessagePublisher, IDisposable
    {
        IBus bus;

        public MessagePublisher(string connectionString)
        {
            bus = RabbitHutch.CreateBus(connectionString);
        }

        public void Dispose()
        {
            bus.Dispose();
        }

        public void PublishOrderStatusChangedMessage(int productId, int quantity, string topic)
        {
            var message = new OrderStatusChangedMessage
            { ProductId = productId, Quantity = quantity };

            bus.Publish(message, topic);
        }

        public void PublishCustomerActivity(int customerId, int orderId, string topic)
        {
            var message = "Your order has been made with Order ID: " + orderId.ToString() + "to the Costumer ID: " + customerId.ToString();

            bus.Publish(message, topic);
        }

        public void PublishCustomerExist(string topic)
        {
            var message = "Your order has been canceled because your customer didn´t exist";

            bus.Publish(message, topic);
        }
    }
}
