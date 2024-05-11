using System;
using System.Threading.Tasks;

public class JobScheduler
{
    private readonly IScheduledAction action;

    public JobScheduler(IScheduledAction action)
    {
        this.action = action;
    }

    public async Task StartScheduler()
    {
        while (true)
        {
            var now = DateTime.Now;
            var nextHour = now.AddHours(1).Date.AddHours(now.Hour + 1);

            // Ожидание до следующего часа
            await Task.Delay(nextHour - now);

            // Выполнение действия
            action.DoAction();
        }
    }
}