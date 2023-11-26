// See https://aka.ms/new-console-template for more information

public record struct WeatherData(int? Temperature, int? Humidity);

public class WeatherDataAggregator
{
    public IEnumerable<WeatherData> WeatherHistory => _weatherHistory;
    private List<WeatherData> _weatherHistory = new();

    public void GetNotifiedAboutNewData(/*your code goes here*/object? sender, WeatherDataEventArgs eventArgs)
    {
        //your code goes here
        _weatherHistory.Add(eventArgs.WeatherData);
    }
}


public class WeatherStation
{
    public event EventHandler<WeatherDataEventArgs>? WeatherMeasured;

    public void Measure()
    {
        int temperature = 25;

        OnWeatherMeasured(temperature);
    }

    private void OnWeatherMeasured(/*your code goes here*/int temperature)
    {
        //your code goes here
        WeatherData dataWithTempretureOnly = new WeatherData(temperature, null);

        WeatherMeasured?.Invoke(this, new WeatherDataEventArgs(dataWithTempretureOnly));
    }
}

public class WeatherBaloon
{
    public event EventHandler<WeatherDataEventArgs>? WeatherMeasured;

    public void Measure()
    {
        int humidity = 50;

        OnWeatherMeasured(humidity);
    }

    private void OnWeatherMeasured(/*your code goes here*/int humidity)
    {
        //your code goes here
        WeatherData dataWithHumidityOnly = new WeatherData(null, humidity);

        WeatherMeasured?.Invoke(this, new WeatherDataEventArgs(dataWithHumidityOnly));
    }
}

public class WeatherDataEventArgs : EventArgs
{
    public WeatherData WeatherData { get; }

    public WeatherDataEventArgs(WeatherData weatherData)
    {
        WeatherData = weatherData;
    }
}