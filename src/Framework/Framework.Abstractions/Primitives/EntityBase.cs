namespace Framework.Abstractions.Primitives;

/// <summary>
///     Represents the base class for all entities in the system.
/// </summary>
/// <typeparam name="TId">The type of the entity's identifier.</typeparam>
public abstract class EntityBase<TId> : IEqualityComparer<EntityBase<TId>>
    where TId : notnull
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityBase{TId}" /> class with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    protected EntityBase(TId id)
    {
        Id = id;
    }

    /// <summary>
    ///     Gets the identifier of the entity.
    /// </summary>
    public TId Id { get; }

    /// <summary>
    ///     Determines whether the specified <see cref="EntityBase{TId}" /> is equal to the current
    ///     <see cref="EntityBase{TId}" />.
    /// </summary>
    /// <param name="other">The <see cref="EntityBase{TId}" /> to compare with the current <see cref="EntityBase{TId}" />.</param>
    /// <returns>
    ///     true if the specified <see cref="EntityBase{TId}" /> is equal to the current <see cref="EntityBase{TId}" />;
    ///     otherwise, false.
    /// </returns>
    public bool Equals(EntityBase<TId>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other.GetType() != GetType()) return false;
        return Id.Equals(other.Id);
    }



    /// <summary>
    ///     Determines whether the specified object is equal to the current <see cref="EntityBase{TId}" />.
    /// </summary>
    /// <param name="obj">The object to compare with the current <see cref="EntityBase{TId}" />.</param>
    /// <returns>true if the specified object is equal to the current <see cref="EntityBase{TId}" />; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return Equals(obj as EntityBase<TId>);
    }

    /// <summary>
    ///     Returns a hash code for the current <see cref="EntityBase{TId}" />.
    /// </summary>
    /// <returns>A hash code for the current <see cref="EntityBase{TId}" />.</returns>
    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }

    /// <summary>
    ///     Determines whether two <see cref="EntityBase{TId}" /> instances are equal using IEqualityComparer.
    /// </summary>
    /// <param name="x">The first instance.</param>
    /// <param name="y">The second instance.</param>
    /// <returns>true if the two instances are equal; otherwise, false.</returns>
    public bool Equals(EntityBase<TId>? x, EntityBase<TId>? y)
    {
        if (x is null && y is null) return true;
        if (x is null || y is null) return false;
        return x.Equals(y);
    }

    /// <summary>
    ///     Returns a hash code for the given <see cref="EntityBase{TId}" /> instance.
    /// </summary>
    /// <param name="obj">The instance.</param>
    /// <returns>A hash code for the specified instance.</returns>
    public int GetHashCode(EntityBase<TId> obj)
    {
        return obj.Id.GetHashCode();
    }
}
