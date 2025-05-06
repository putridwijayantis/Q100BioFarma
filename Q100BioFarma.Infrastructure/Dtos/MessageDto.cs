namespace Q100BioFarma.Infrastructur.Dtos;

public class MessageDto
{
    public MessageDto(string title, string message)
    {
        Title = title;
        Message = message;
    }

    public MessageDto(string message)
    {
        Title = string.Empty;
        Message = message;
    }

    public MessageDto()
    {
    }

    public string Title { get; set; }

    public string Message { get; set; }
}