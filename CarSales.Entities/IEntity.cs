
namespace CarSales.Entities
{
    public interface IEntity<TIDType> where TIDType : struct
    {
        TIDType Id { get; set; }
    }
}
