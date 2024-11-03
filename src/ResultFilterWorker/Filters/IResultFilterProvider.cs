namespace ResultFilterWorker.Filters;

public interface IResultFilterProvider
{
    IResultFilter GetResultFilter(string activityName);
}



