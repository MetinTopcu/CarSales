
namespace CarSales.Entities
{
    public class Service : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime ServiceArrivalDate { get; set; }
        public string CarProblem { get; set; }
        public decimal ServiceCharge { get; set; }
        public DateTime ServiceExitDate { get; set; }
        public string CarFix { get; set; }
        public bool Warranty { get; set; }
        public string CarPlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string BodyType { get; set; }
        public string SaseNo { get; set; }
        public string Notes { get; set; }
    }
}
