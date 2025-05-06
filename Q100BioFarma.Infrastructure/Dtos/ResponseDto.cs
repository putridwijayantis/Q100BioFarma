namespace Q100BioFarma.Infrastructur.Dtos;

public class ResponseDto
{
    public ResponseDto(int statusCode, MessageDto messageValue, object objectValue)
    {
        Status = statusCode;
        Message = messageValue;
        Data = objectValue;
    }

    public int Status { get; set; }

    public MessageDto Message { get; set; }

    public object Data { get; set; }
}