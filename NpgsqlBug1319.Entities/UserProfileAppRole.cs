using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NpgsqlBug1319.Entities
{
    public class UserProfileAppRole
    {
        [Key]
        public int UserProfileAppRoleID { set; get; }

        [Required]
        [ForeignKey(nameof(UserProfile))]
        public Guid UserProfileID { set; get; }

        public UserProfile UserProfile { set; get; }

        [Required]
        [ForeignKey(nameof(AppRole))]
        public Guid AppRoleID { set; get; }

        public AppRole AppRole { set; get; }
    }
}
