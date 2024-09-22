
namespace CarSales.Entities
{
    public class Customer : IEntity<int>
    {
        public int Id { get; set; }
        public int CarID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TCNO { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }
        public virtual Car? Car { get; set; }
    }
}
