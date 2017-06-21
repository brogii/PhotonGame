using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Model
{
    class User
    {
        public virtual int Id {get;set;}
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime Regdate { get; set; }

    }
}
