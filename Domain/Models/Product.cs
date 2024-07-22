
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace e_commerce.Domain.Models
{
    public class Product: BaseModel
    {
        public Product(string productName, string Describtion, double Price, int Quantity,string imageUrl = null)
        {
            this.productName = productName;
            this.Describtion = Describtion;
            this.Price = Price;
            this.Quantity = Quantity;
            this.imageUrl = imageUrl;

        }

        public string productName {set;get;}
        public string Describtion {set;get;}
        public double Price {set;get;}
        public int Quantity {set;get;}
        public string? imageUrl {set;get;} = null;

        
    }
}