// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
const int threshold = 30_000;
var emailPriceChangeNotifier = new EmailPriceChangeNotifier(threshold);
var pushPriceChangeNotifier = new PushPriceChangeNotifier(threshold);
var goldPriceReader = new GoldPriceReader();

goldPriceReader.PirceRead += emailPriceChangeNotifier.Update;
goldPriceReader.PirceRead += pushPriceChangeNotifier.Update;
//goldPriceReader.AttachObserver(emailPriceChangeNotifier);
//goldPriceReader.AttachObserver(pushPriceChangeNotifier);

for (int i = 0; i < 3; i++)
{
    goldPriceReader.ReadCurrentPrice();
}
goldPriceReader.PirceRead -= emailPriceChangeNotifier.Update;
goldPriceReader.PirceRead -= pushPriceChangeNotifier.Update;

Console.ReadKey();
// -------------------------------------

public class PriceReadEventArgs : EventArgs
{
    public decimal Price { get; }
    public PriceReadEventArgs(decimal price)
    {
        Price = price;
    }   
}

//public delegate void PriceRead(decimal price);

public class GoldPriceReader /*: IObserveralble<decimal>*/
{
    //private int _currentGoldPrice;
    //private readonly List<IObserver<decimal>> _observers = new ();
    public event EventHandler<PriceReadEventArgs>? PirceRead;
    public void ReadCurrentPrice()
    {
        var currentGoldPrice = new Random().Next(20_000, 50_000);
        OnPriceRead(currentGoldPrice);
        //NotifyObservers();
    }

    private void OnPriceRead(int currentGoldPrice)
    {
        PirceRead?.Invoke(this, new PriceReadEventArgs(currentGoldPrice));
    }

    //public void AttachObserver(IObserver<decimal> observer)
    //{
    //    _observers.Add(observer);
    //}

    //public void DetachObserver(IObserver<decimal> observer)
    //{
    //    _observers.Remove(observer);
    //}

    //public void NotifyObservers()
    //{
    //    foreach(var observer in _observers)
    //    {
    //        observer.Update(_currentGoldPrice);
    //    }
    //}
}

public class EmailPriceChangeNotifier /*: IObserver<decimal>*/
{
    private readonly decimal _notificationThreshold;
    public EmailPriceChangeNotifier(decimal notificationThreshold)
    {
        _notificationThreshold = notificationThreshold;
    }
    public void Update(object? sender, PriceReadEventArgs eventArgs)
    {
        if (eventArgs.Price > _notificationThreshold)
        {
            Console.WriteLine(
                $"Email sent" +
                $"the gold price exceeded {_notificationThreshold}" +
                $"and is now {eventArgs.Price}");
        }
    }
}

public class PushPriceChangeNotifier /*: IObserver<decimal>*/
{
    private readonly decimal _notificationThreshold;
    public PushPriceChangeNotifier(
        decimal notificationThreshold)
    {
        _notificationThreshold = notificationThreshold;
    }
    public void Update(object? sender, PriceReadEventArgs eventArgs)
    {
        if (eventArgs.Price > _notificationThreshold)
        {
            Console.WriteLine(
                $"Sent a push notification saying that" +
                $"the gold price exceeded {_notificationThreshold}" +
                $"and is now {eventArgs.Price}");
        }
    }

}

//public interface IObserver<TData>
//{
//    void Update(TData data);
//}

//public interface IObserveralble<TData>
//{
//    void AttachObserver(IObserver<TData> observer);
//    void DetachObserver(IObserver<TData> observer);
//    void NotifyObservers();
//}