using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Domain.Models
{
    public class User : BaseModel
    {
        public User(string FirstName, string LastName, string Email,string Password){
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Password = Password;
        }
        
        public string FirstName{get;set;} = string.Empty;
        public string LastName{get;set;} = string.Empty;
        public string Email{get;set;} = string.Empty;
        public string Password{get;set;} = string.Empty; 
        
    }
}