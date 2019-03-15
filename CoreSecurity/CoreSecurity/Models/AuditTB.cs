using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSecurity.Models
{
    [Table("AuditTB")]
    public class AuditTb
    {
        [Key]
        public int UsersAuditId { get; set; }
        public int? UserId { get; set; }
        public string SessionId { get; set; }
        public string IpAddress { get; set; }
        public string PageAccessed { get; set; }
        public DateTime? LoggedInAt { get; set; }
        public DateTime? LoggedOutAt { get; set; }
        public string LoginStatus { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string UrlReferrer { get; set; }
        public string Method { get; set; }

    }
}
