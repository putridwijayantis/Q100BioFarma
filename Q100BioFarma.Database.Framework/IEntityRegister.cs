using Microsoft.EntityFrameworkCore;

namespace Q100BioFarma.Database.Framework;

/// <summary>
/// IEntity Register contract interface
/// </summary>
public interface IEntityRegister
{
    /// <summary>
    /// Register entity to build a model to database
    /// </summary>
    /// <param name="modelbuilder"></param>
    void RegisterEntities(ModelBuilder modelbuilder);
}