using Colors = System.Drawing.Color;

namespace QuotesMaui;

public partial class MainPage : ContentPage
{
    private readonly List<string> _quotes = new();
    private readonly Random _random = new();

    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMauiAsset();
    }

    private async Task LoadMauiAsset()
    {
        await using var stream = await FileSystem.OpenAppPackageFileAsync("quotes.txt");
        using var reader = new StreamReader(stream);

        while (reader.Peek() != -1)
        {
            _quotes.Add(await reader.ReadLineAsync());
        }
    }

    private void btnGenerateQuote_Clicked(object sender, EventArgs e)
    {
        var startColor =
            Colors.FromArgb(
                _random.Next(0, 256),
                _random.Next(0, 256),
                _random.Next(0, 256));

        var endColor =
            Colors.FromArgb(
                _random.Next(0, 256),
                _random.Next(0, 256),
                _random.Next(0, 256));


        var colors = ColorUtility.ColorControls.GetColorGradient(startColor, endColor, 6);

        var stopOffset = .0f;
        var stops = new GradientStopCollection();
        foreach (var c in colors)
        {
            stops.Add(new GradientStop(Color.FromArgb(c.Name), stopOffset));
            stopOffset += .2f;
        }

        var gradient = new LinearGradientBrush(stops, new Point(0, 0), new Point(1, 1));

        background.Background = gradient;

        var index = _random.Next(_quotes.Count);
        quote.Text = _quotes[index];
    }
}

