namespace InterviewExercises.InMemoryEventBus;

public sealed class EventBus
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new();

    public void Subscribe<TEvent>(Action<TEvent> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);

        var eventType = typeof(TEvent);
        if (!_handlers.TryGetValue(eventType, out var handlers))
        {
            handlers = new List<Delegate>();
            _handlers[eventType] = handlers;
        }

        handlers.Add(handler);
    }

    public void Publish<TEvent>(TEvent @event)
    {
        if (!_handlers.TryGetValue(typeof(TEvent), out var handlers))
        {
            return;
        }

        foreach (var handler in handlers.ToArray())
        {
            ((Action<TEvent>)handler)(@event);
        }
    }
}
