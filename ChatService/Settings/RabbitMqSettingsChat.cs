using ChatBoxSharedObjects.Settings;

public class RabbitMqSettingsChat
{
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    static Random random = new Random();
    const int QueueLength = 16;
    public new string QueueName
    {
        get
        {
            return new string(Enumerable.Repeat(chars, QueueLength)
         .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}