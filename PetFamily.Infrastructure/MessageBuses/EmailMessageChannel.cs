using System.Threading.Channels;
using PetFamily.Application.Messages;

namespace PetFamily.Infrastructure.MessageBuses;

public class EmailMessageChannel
{
    private readonly Channel<EmailNotification> _channel = Channel.CreateUnbounded<EmailNotification>();

    public ChannelWriter<EmailNotification> Writer => _channel.Writer;

    public ChannelReader<EmailNotification> Reader => _channel.Reader;
}