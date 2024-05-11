namespace TelegramTest;

public class ScheduledActions
{
    private List<(Func<Task> action, DateTime executionTime)> scheduledTasks = new List<(Func<Task> action, DateTime executionTime)>();

    public void AddScheduledAction(Func<Task> action, DateTime executionTime)
    {
        scheduledTasks.Add((action, executionTime));
    }

    public async Task StartScheduler()
    {
        while (true)
        {
            foreach (var task in scheduledTasks.ToArray())
            {
                if (task.executionTime <= DateTime.Now)
                {
                    await task.action(); // Выполнение асинхронного действия
                    scheduledTasks.Remove(task);
                }
            }

            await Task.Delay(1000); // Проверяем запланированные действия каждую секунду
        }
    }
}