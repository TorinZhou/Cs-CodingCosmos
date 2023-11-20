using System.Text;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using System.Globalization;

string folderPath = "D:\\CSharpPractices\\23_Assignment_Tickets_Data_Aggregator\\bin\\Debug\\net7.0\\Tickets";
var app = new TicketsDataAggregatorApp(new PdfPigReader(), folderPath, new TextTicketDataRepository());
app.Read();
app.Convert();

Console.ReadKey();

public class TicketsDataAggregatorApp
{
    public IEnumerable<TicketData> TicketDatas { get; set; }
    private readonly IPdfReader _reader;
    private readonly ITicketDataRepository _dataRepository;
    public string DirectoryPath { get; init; }


    public TicketsDataAggregatorApp(IPdfReader reader, string directoryPath, ITicketDataRepository ticketDataRepository)
    {
        _reader = reader;
        DirectoryPath = directoryPath;
        _dataRepository = ticketDataRepository;
    }

    public void Convert()
    {
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string outputFileName = "FormattedTicketData.txt";
        string outputFilePath = Path.Combine(currentDirectory, outputFileName);
        _dataRepository.Save(TicketDatas, outputFilePath);
    }
    public void Read()
    {
        TicketDatas = _reader.ReadAllFile(DirectoryPath);
    }

    public void Show()
    {
        _reader.Display();
    }
}

public interface IPdfReader
{
    List<TicketData> ReadAllFile(string directoryPath);
    void ReadSingleFile(string filePath);
    void Display();
}

public class PdfPigReader : IPdfReader
{
    private readonly List<TicketData> _ticketsData = new List<TicketData>();

    public void Display()
    {
        foreach (var ticketData in _ticketsData)
        {
            // Using string interpolation with alignment
            // -30 indicates left-aligned within 30 character space
            // 10 for date and 5 for time should be sufficient based on your format
            string formattedLine = $"{ticketData.Title,-30} | {ticketData.Date:dd/MM/yyyy} | {ticketData.Date:HH:mm}";
            Console.WriteLine(formattedLine);
        }
    }
    public List<TicketData> ReadAllFile(string directoryPath)
    {
        string[] pdfFiles = Directory.GetFiles(directoryPath, "*.pdf");
        foreach (string filePath in pdfFiles)
        {
            ReadSingleFile(filePath);
        }
        return _ticketsData;
    }

    public void ReadSingleFile(string filePath)
    {
        //using (PdfDocument document = PdfDocument.Open("D:\\CSharpPractices\\23_Assignment_Tickets_Data_Aggregator\\Tickets\\Tickets1.pdf"))
        using (PdfDocument document = PdfDocument.Open(filePath))
        {
            foreach (Page page in document.GetPages())
            {
                string pageText = page.Text;
                ExtractTicketInformation(pageText);
            }
        }
    }

    private void ExtractTicketInformation(string text)
    {
        //var regex = new Regex(@"Title:(?<Title>.+?)Date:(?<Date>.+?)Time:(?<Time>.+?)(Title|$)");
        var regex = new Regex(@"Title:(?<Title>.+?)Date:(?<Date>.+?)Time:(?<Time>.+?)(?=Title|Visit us|$)");
        var matches = regex.Matches(text);

        var websiteRegex = new Regex(@"Visit us:(www\..+?)\s*$");
        var websiteMatch = websiteRegex.Match(text);
        var websiteEnum = CinemaWebsite.Unknown;

        if (websiteMatch.Success)
        {
            websiteEnum = MapWebsiteToEnum(websiteMatch.Groups[1].Value);
        }

        foreach (Match match in matches)
        {
            if (match.Success)
            {
                string timeString = match.Groups["Time"].Value.Trim();
                // Remove any trailing non-time characters
                //timeString = Regex.Replace(timeString, @"[^\d:AMPMapm]+", "").Trim();
                timeString = Regex.Replace(timeString, @"(Visit us:.+)$", "").Trim();

                _ticketsData.Add(new TicketData
                {
                    Title = match.Groups["Title"].Value.Trim(),
                    //Date = ParseDate(match.Groups["Date"].Value.Trim(), match.Groups["Time"].Value.Trim(), websiteEnum),
                    Date = ParseDate(match.Groups["Date"].Value.Trim(), timeString, websiteEnum),
                    //Time = match.Groups["Time"].Value.Trim(),
                    Website = websiteEnum,
                });
            }
        }
    }
    
    private DateTime ParseDate(string dateString, string timeString, CinemaWebsite website)
    {
        CultureInfo cultureInfo;
        string dataFormat;

        switch(website)
        {
            case CinemaWebsite.OurCinemaCom:
                cultureInfo = new CultureInfo("en-US");
                dataFormat = "M/d/yyyy h:mm tt";
                break;
            case CinemaWebsite.OurCinemaJp:
                cultureInfo = new CultureInfo("ja-JP");
                dataFormat = "yyyy/MM/dd HH:mm";
                break;
            case CinemaWebsite.OurCinemaFr:
                cultureInfo = new CultureInfo("fr-FR");
                dataFormat = "dd/MM/yyyy HH:mm";
                break;
            default:
                throw new InvalidOperationException("Unsupported website format");
        }
        string combinedDataTimeString = $"{dateString} {timeString}";
        return DateTime.ParseExact(combinedDataTimeString, dataFormat, cultureInfo);
    }
    private CinemaWebsite MapWebsiteToEnum(string website)
    {
        return website switch
        {
            "www.ourCinema.com" => CinemaWebsite.OurCinemaCom,
            "www.ourCinema.jp" => CinemaWebsite.OurCinemaJp,
            "www.ourCinema.fr" => CinemaWebsite.OurCinemaFr,
            _ => CinemaWebsite.Unknown,
        };
    }
}


public interface IUserInteraction
{
    void DisplayMessage(string? message);
}

public class ConsoleUserInteraction
{
    void DisplayMessage(string? message)
    {
        Console.WriteLine(message);
    }
}

public record TicketData
{
    public string Title { get; set; }
    public DateTime Date { get; set; }
    //public string Time { get; set; }

    public CinemaWebsite Website { get; init; }

}

public enum CinemaWebsite
{
    OurCinemaCom,
    OurCinemaJp,
    OurCinemaFr,
    Unknown,
}

public interface ITicketDataFormatter
{
    string Format(TicketData ticketData);
}

public class ITicketDataStringFormatter : ITicketDataFormatter
{
    public string Format(TicketData ticketData)
    {
        throw new NotImplementedException();
    }
}
public interface ITicketDataRepository
{
    void Save(IEnumerable<TicketData> ticketDatas, string filePath);
}

public class TextTicketDataRepository : ITicketDataRepository
{
    public void Save(IEnumerable<TicketData> ticketDatas, string filePath)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var ticketData in ticketDatas)
        {
            string formattedTicketDataString = $"{ticketData.Title,-30} | {ticketData.Date:dd/MM/yyyy} | {ticketData.Date:HH:mm}";
            sb.AppendLine(formattedTicketDataString);
        }
        File.WriteAllText(filePath, sb.ToString());
    }
    
}