using System.ComponentModel.DataAnnotations;

namespace CarSales.Entities
{
    public class User : IEntity<int>
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Surname { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsItActive { get; set; }
        public DateTime UserCreateDate { get; set; }
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
