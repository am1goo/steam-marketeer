using System.Threading.Tasks;

public static class TaskUtility
{
    public static async Task WaitAll(Task[] array)
    {
        while (true)
        {
            if (AreTasksCompleted(array))
            {
                break;
            }
            else
            {
                await Task.Delay(25);
            }
        }
    }

    private static bool AreTasksCompleted(Task[] array)
    {
        for (int i = 0; i < array.Length; ++i)
        {
            var task = array[i];
            if (!task.IsCompleted)
                return false;
        }
        return true;
    }
}
