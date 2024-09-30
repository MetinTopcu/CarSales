﻿using System.ComponentModel.DataAnnotations;

namespace CarSales.Entities
{
    public class User : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Name { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Surname { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Email { get; set; }
        [StringLength(20)]
        public string? Phone { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "{0} Boş Bırakılamaz")]
        public string Password { get; set; }
        public bool IsItActive { get; set; }
        [Display(Name = "Created Date"), ScaffoldColumn(false)]
        public DateTime UserCreateDate { get; set; }
        [Display(Name = "User Role")]
        public int RoleId { get; set; }
        [Display(Name = "User Role")]
        public Role? Role { get; set; }
    }
}
