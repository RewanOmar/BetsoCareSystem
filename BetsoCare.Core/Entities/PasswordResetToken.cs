using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Entities
{
    public class PasswordResetToken
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public string Token { get; set; } = null!;

        public DateTime ExpireAt { get; set; }

        public bool Used { get; set; } = false;
    }
}
