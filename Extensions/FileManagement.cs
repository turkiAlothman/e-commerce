using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Extensions
{
    public static class FileManagement
    {
        public static List<string> validExtentions = ["jpg","png","jpeg"];
        
        public static int MaxSize = 5000000;
        public static (bool,string)  validate(this IFormFile file){
                
                string exnetion = file.FileName.Split(".").Last().ToLower();
                
                if(!validExtentions.Contains(exnetion)) return (false , "file exnetion is invalid");

                if(file.Length > MaxSize) return (false , $"file length exceeded {MaxSize} (max size)");

                return (true,"");


        }
    }
}