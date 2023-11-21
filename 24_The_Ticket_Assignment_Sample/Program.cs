using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;
using System.Globalization;
using System.Text;

const string TicketsFolder = @"D:\Desktop\Tickets";

try
{
    var ticketsAggregator = new TicketsAggregator(TicketsFolder);

    ticketsAggregator.Run();
}
catch (Exception ex)
{
    Console.WriteLine("An exception occured. " + "Exception message: " + ex.Message);
}
Console.WriteLine("Press any key to exit");
Console.ReadKey();


public class TicketsAggregator
{
    private readonly string _ticketsFolder;

    private readonly Dictionary<string, CultureInfo> _domainToCultureMapping = new ()
    {
        [".com"] = new CultureInfo("en-US"),
        [".fr"] = new CultureInfo("fr-FR"),
        [".jp"] = new CultureInfo("ja-JP"),
      
    };

    public TicketsAggregator(string ticketsFolder)
    {
        _ticketsFolder = ticketsFolder;
    }

    public void Run()
    {
        var sb = new StringBuilder();
        foreach (var filePath in Directory.GetFiles(_ticketsFolder, "*.pdf"))
        {

            using PdfDocument document = PdfDocument.Open(filePath);
            Page page = document.GetPage(1);
            var lines = ProcessPage(page);
            sb.AppendLine(string.Join(Environment.NewLine, lines));

        }
        //! better to use Path.Combine rather than do it manually, because this way, it will work regardless of system
        SaveTicketsData(sb);
    }

    private IEnumerable<string> ProcessPage(Page page)
    {
        string text = page.Text;

        //! Split string by spliter
        var split = text.Split(new[] { "Title:", "Date:", "Time:", "Visit us:" }, StringSplitOptions.None);

        //var domain = split[-1];
        var domain = ExtractDomain(split.Last());
        var ticketCulture = _domainToCultureMapping[domain];

        for (int i = 1; i < split.Length - 3; i += 3)
        {
            yield return BuildTicketData(split, i , ticketCulture);
        }
    }

    private static string BuildTicketData(string[] split, int i, CultureInfo ticketCulture)
    {
        var title = split[i];
        var dateAsString = split[i + 1];
        var timeAsString = split[i + 2];

        //! DateOnly and TimeOnly starts only from .net 6
        //! now that we have the date and time, we  need to parse them back to the users culture
        var date = DateOnly.Parse(dateAsString, ticketCulture);
        var time = TimeOnly.Parse(timeAsString, ticketCulture);
        //var date = DateOnly.Parse(dateAsString, new CultureInfo(ticketCulture));
        //var time = TimeOnly.Parse(timeAsString, new CultureInfo(ticketCulture));

        //! here we build two cultureInfo obj for each piece of data, we should store them in the dictionary
        var dateAsStringInvariant = date.ToString(CultureInfo.InvariantCulture);
        var timeAsStringInvariant = time.ToString(CultureInfo.InvariantCulture);

        var ticketData = $"{title,-40}| {dateAsStringInvariant} | {timeAsStringInvariant}";
        return ticketData;
    }

    private void SaveTicketsData(StringBuilder sb)
    {
        var resultPath = Path.Combine(_ticketsFolder, "aggregatedTickets.txt");
        File.WriteAllText(resultPath, sb.ToString());
        Console.WriteLine("Result saved to " + resultPath);
    }
    //private static string ExtractDomainUsingSplit(string webAddress)
    //{
    //    string domainEnding = webAddress.Split('.').Last();
    //    return domainEnding;
    //}
    private static string ExtractDomain(string webAddress)
    {
        var lastDotIndex = webAddress.LastIndexOf('.');
        return webAddress.Substring(lastDotIndex);
    }

}