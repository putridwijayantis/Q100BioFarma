using Q100BioFarma.Infrastructur.Exceptions;

namespace Q100BioFarma.Infrastructur.Constants;

public class Error
{
    public static Exception CustomError(string exMessage)
    {
        throw new HttpResponseLibraryException(HttpStatusCodes.BAD_REQUEST,
            HttpStatusCodes.ERROR_TRANSACT,
            "ERROR",
            exMessage);
    }
    
    public static Exception CustomErrorBadRequest(string exMessage)
    {
        throw new HttpResponseLibraryException(HttpStatusCodes.BAD_REQUEST,
            HttpStatusCodes.BAD_REQUEST,
            "ERROR",
            exMessage);
    }
}