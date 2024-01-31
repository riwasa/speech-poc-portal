public class RecognizedPhrase
{
    public int Channel { get; set; }

    public string? Duration { get; set; }

    public long DurationInTicks { get; set; }

    public NBest[]? NBest { get; set; }

    public string? Offset { get; set; }

    public long OffsetInTicks { get; set; }

    public string? RecognitionStatus { get; set; }

    public int Speaker { get; set; }
}