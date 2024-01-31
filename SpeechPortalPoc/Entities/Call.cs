public class Call
{
    public string? Category { get; set; }

    public CombinedRecognizedPhrase[]? CombinedRecognizedPhrases { get; set; }

    public string? Conversation { get; set; }

    public string? Duration { get; set; }

    public long DurationInTicks { get; set; }

    public string[]? key_items { get; set; }

    public NER[]? NER { get; set; }

    public RecognizedPhrase[]? RecognizedPhrases { get; set; }

    public string? Sentiment { get; set; }

    public string? Source { get; set; }

    public string? Summary { get; set; }

    public string? TimeStamp { get; set; }

    public string[]? Topic { get; set; }
}