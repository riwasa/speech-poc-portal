public class NBest
{
    public decimal Confidence { get; set; }

    public string? Display { get; set; }

    public string? ITN { get; set; }

    public string? Lexical { get; set; }

    public string? MaskedITN { get; set; }

    public Sentiment? Sentiment { get; set; }
}