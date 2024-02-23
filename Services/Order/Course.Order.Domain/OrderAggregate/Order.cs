﻿using Course.Order.Core;

namespace Course.Order.Domain.OrderAggregate;
public class Order : Entity, IAggregateRoot
{
    public Order()
    {
        
    }

    public Order(Address address, string buyerId)
    {
        Address = address;
        BuyerId = buyerId;
    }

    public DateTime CreatedDate { get; private set; } = DateTime.Now;
    public Address Address { get; private set; }
    public string BuyerId { get; private set; }
    //Shadow Property => not setted OrderId

    //backing field
    private readonly List<OrderItem> _orderItems = [];

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public void AddOrderItem(string productId,string productName,string imageUrl,decimal price)
    {
        var isExists = _orderItems.Exists(x=>x.ProductId == productId);

        if(!isExists)
        {
            var orderItem = new OrderItem(productId, productName, imageUrl, price);
            _orderItems.Add(orderItem);
        }
    }

    public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
}