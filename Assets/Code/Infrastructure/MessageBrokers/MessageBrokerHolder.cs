using UniRx;

namespace Code.Infrastructure.MessageBrokers
{
    public static class MessageBrokerHolder
    {
        public static IMessageBroker Camera { get; private set; } = new UniRx.MessageBroker();
        public static IMessageBroker Game { get; private set; } = new UniRx.MessageBroker();
        public static IMessageBroker Audio { get; private set; } = new UniRx.MessageBroker();
    }
}