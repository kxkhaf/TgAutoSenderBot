namespace TelegramTest;

public class ScheduledTask
{
    private Func<Task> action;
    private double hour;

    public ScheduledTask(Func<Task> action, double hour)
    {
        this.action = action;
        this.hour = hour;
    }

    public void Start()
    {
        Task.Run(YourAction);
    }

    private async Task YourAction()
    {
        while (true)
        {
            var now = DateTime.Now;
            var nextTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            var nextExecutionTime = nextTime.AddHours(hour);

            if (nextExecutionTime > now) await Task.Delay(nextExecutionTime - now);

            await action.Invoke();

            await Task.Delay(TimeSpan.FromHours(hour));
        }
    }
}