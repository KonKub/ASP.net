using System.Diagnostics;

public interface INotifier
{
    void Notify();

    bool CanRun();
}

public class Notifier1 : INotifier
{
    public bool _canRun;

    public void Notify()
    {
        Debug.WriteLine("Debugging from Notifier 1");
    }

    public bool CanRun()
    {
        return _canRun;
    }
}