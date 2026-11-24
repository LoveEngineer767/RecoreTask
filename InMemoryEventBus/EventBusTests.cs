namespace InterviewExercises.InMemoryEventBus;

public class EventBusTests
{
    [Fact]
    public void Publish_InvokesTheHandlerRegisteredForTheEventType()
    {
        var eventBus = new EventBus();
        var receivedEvents = new List<OrderPlaced>();

        eventBus.Subscribe<OrderPlaced>(receivedEvents.Add);

        var orderPlaced = new OrderPlaced(42);
        eventBus.Publish(orderPlaced);

        var received = Assert.Single(receivedEvents);
        Assert.Same(orderPlaced, received);
    }

    [Fact]
    public void Publish_DoesNotInvokeHandlersSubscribedToAnotherEventType()
    {
        var eventBus = new EventBus();
        var orders = new List<OrderPlaced>();
        var shipments = new List<ShipmentDispatched>();

        eventBus.Subscribe<OrderPlaced>(orders.Add);
        eventBus.Subscribe<ShipmentDispatched>(shipments.Add);

        eventBus.Publish(new OrderPlaced(7));

        Assert.Single(orders);
        Assert.Empty(shipments);
    }

    [Fact]
    public void Publish_InvokesEveryHandlerForTheEventType()
    {
        var eventBus = new EventBus();
        var received = new List<int>();

        eventBus.Subscribe<OrderPlaced>(order => received.Add(order.OrderId));
        eventBus.Subscribe<OrderPlaced>(order => received.Add(order.OrderId + 1));

        eventBus.Publish(new OrderPlaced(10));

        Assert.Equal([10, 11], received);
    }

    [Fact]
    public void Publish_InvokesHandlersInSubscriptionOrder()
    {
        var eventBus = new EventBus();
        var order = new List<string>();

        eventBus.Subscribe<OrderPlaced>(_ => order.Add("first"));
        eventBus.Subscribe<OrderPlaced>(_ => order.Add("second"));
        eventBus.Subscribe<OrderPlaced>(_ => order.Add("third"));

        eventBus.Publish(new OrderPlaced(1));

        Assert.Equal(["first", "second", "third"], order);
    }

    [Fact]
    public void Publish_DoesNothingWhenThereAreNoSubscribers()
    {
        var eventBus = new EventBus();

        var exception = Record.Exception(() => eventBus.Publish(new OrderPlaced(1)));

        Assert.Null(exception);
    }

    [Fact]
    public void Subscribe_ThrowsForANullHandler()
    {
        var eventBus = new EventBus();

        Assert.Throws<ArgumentNullException>(() => eventBus.Subscribe<OrderPlaced>(null!));
    }

    private sealed record OrderPlaced(int OrderId);

    private sealed record ShipmentDispatched(int ShipmentId);
}
