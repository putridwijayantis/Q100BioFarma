namespace Q100BioFarma.Infrastructur;

public class GlobalConfiguration
{
    public static string DefaultCulture => "en-US";

    public static string WebRootPath { get; set; }

    public static string ContentRootPath { get; set; }

    public static string Environment { get; set; }
}