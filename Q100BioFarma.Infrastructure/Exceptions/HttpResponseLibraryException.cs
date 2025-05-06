namespace Q100BioFarma.Infrastructur.Exceptions;

public class HttpResponseLibraryException : Exception
{
    /// <summary>
    ///     Standard Text error for HttpResponseLibraryException
    /// </summary>
    public const string CustomError = "Oops! Something went wrong!";

    /// <summary>
    ///     Initializes a new instance of the <see cref="HttpResponseLibraryException" /> class.
    /// </summary>
    /// <param name="status"></param>
    /// <param name="code"></param>
    /// <param name="title"></param>
    /// <param name="message"></param>
    public HttpResponseLibraryException(int status, int code, string title, string message)
    {
        Title = title;
        Status = status;
        Code = code;
        Message = message;
    }

    /// <summary>
    ///     Gets or sets for Title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Gets or sets for Status
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    ///     Gets or sets for Code Exception
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    ///     Gets or sets for Message
    /// </summary>
#pragma warning disable CS0108, CS0114
    public string Message { get; set; }
#pragma warning restore CS0108, CS0114
}