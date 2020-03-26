using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NpgsqlBug1319.Entities
{
    public class AppRole
    {
        [Key]
        public Guid AppRoleID { set; get; }

        [Required]
        public string Name { set; get; }

        public string Description { set; get; }

        public NpgsqlTsVector SearchVector { set; get; } // @RYAN: MIGRATION ATTEMPTS TO INSERT TO THIS COLUMN!!!

        [Required]
        public DateTimeOffset CreatedAt { set; get; } = DateTimeOffset.UtcNow;

        [Required]
        public DateTimeOffset UpdatedAt { set; get; } = DateTimeOffset.UtcNow;

        public List<UserProfileAppRole> UserProfileAppRoles { set; get; }
    }
}
