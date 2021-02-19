namespace DXP.SmartConnectPickup.Common.Models
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
    }
}
