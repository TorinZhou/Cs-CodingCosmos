namespace _99_CleanCode;

public class DataAccess : IDataAccess
{
    public List<int> GetData()
    {
        //imagine it connect to a DB and reads the data
        return new List<int> { 1, 2, 3 };

    }
}

public class DataProcessor
{
    private readonly IDataAccess _dataAccess;

    public DataProcessor(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public int CalculateSum()
    {
        return _dataAccess.GetData().Sum();
    }
}

public interface IDataAccess
{
    List<int> GetData();
}
