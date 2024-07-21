
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace e_commerce.Domain.Models
{
    public class Product
    {
        public Product(string productName, string Describtion, double Price, int Quantity)
        {
            this.productName = productName;
            this.Describtion = Describtion;
            this.Price = Price;
            this.Quantity = Quantity;

        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set;}
        public string productName {set;get;}
        public string Describtion {set;get;}
        public double Price;
        public int Quantity;

        
    }
}