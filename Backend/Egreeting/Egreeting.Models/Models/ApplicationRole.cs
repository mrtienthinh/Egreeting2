using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class ApplicationRole : IdentityRole
    {
        public virtual EgreetingRole EgreetingRole { get; set; }
    }
}
