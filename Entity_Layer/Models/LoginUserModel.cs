using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer.Models
{
    // User class'ı için login modeli. Sadece username ve şifre var
    public class LoginUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
       
    
    }
}
