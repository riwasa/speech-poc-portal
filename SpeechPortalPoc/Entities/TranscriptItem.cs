namespace SpeechPortalPoc.Entities;

public class TranscriptItem
{
    public double Offset { get; set; }

    public double Duration { get; set; }

    public string? EndTime { get; set; }

    public string? Speaker { get; set; }

    public string? StartTime { get; set; }

    public string? Text { get; set; }

}
