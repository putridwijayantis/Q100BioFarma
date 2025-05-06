using Q100BioFarma.Infrastructur.Models;

namespace Q100BioFarma.Database.Abstract;

/// <summary>
///     Overrides the <see cref="ExtensionBase">ExtensionBase</see> class and provides the ExtCore.Data extension
///     information.
/// </summary>
public class Extension : ExtensionBase
{
    /// <summary>
    ///     Gets the name of the extension.
    /// </summary>
    public override string Name => "Q100BioFarma.Database";

    /// <summary>
    ///     Gets the URL of the extension.
    /// </summary>
    public override string Url => "";

    /// <summary>
    ///     Gets the version of the extension.
    /// </summary>
    public override string Version => "";

    /// <summary>
    ///     Gets the authors of the extension (separated by commas).
    /// </summary>
    public override string Authors => "Putri";
}