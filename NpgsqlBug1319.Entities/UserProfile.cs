using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NpgsqlBug1319.Entities
{
    public class UserProfile
    {
        [Key]
        public Guid UserProfileID { set; get; }

        [Required]
        public string FullName { set; get; }

        public NpgsqlTsVector SearchVector { get; set; } // @RYAN: MIGRATION ATTEMPTS TO INSERT TO THIS COLUMN!!!

        [Required]
        public string Username { set; get; }

        [Required]
        public string Email { set; get; }

        public string Address { set; get; } // @RYAN: THIS PROPERTY ALSO CAUSES MIGRATION EXCEPTION!!!

        [Required]
        public DateTimeOffset CreatedAt { set; get; } = DateTimeOffset.UtcNow;

        [Required]
        public DateTimeOffset UpdatedAt { set; get; } = DateTimeOffset.UtcNow;

        public List<UserProfileAppRole> UserProfileAppRoles { set; get; }
    }
}
