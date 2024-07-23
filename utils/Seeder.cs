using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DnsClient;
using e_commerce.Configurations;
using e_commerce.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace e_commerce.utils
{
    public class Seeder
    {
        public static void InsertDummyData(IApplicationBuilder builder){
        
        DatabaseSettings data =  builder.ApplicationServices.GetService<IOptions<DatabaseSettings>>().Value;
        var client = new MongoClient(data.connectionString);
        var Database =  client.GetDatabase(data.databaseName);
        var collection = Database.GetCollection<Product>(data.productCollection);

        
        long count  = collection.CountDocuments(_=> true);
        
        if (count != 0)
        return;

        collection.InsertMany(new List<Product>{

        new Product("Smartphone", "A high-end smartphone with a powerful processor and stunning display.", 699.99, 50, "https://images.unsplash.com/photo-1556656793-08538906a9f8"),
        new Product("Laptop", "A lightweight laptop with excellent battery life and performance.", 999.99, 30, "https://images.unsplash.com/photo-1517336714731-489689fd1ca8"),
        new Product("Headphones", "Noise-cancelling headphones with superior sound quality.", 199.99, 100, "https://images.unsplash.com/photo-1511367461989-f85a21fda167"),
        new Product("Smartwatch", "A smartwatch with multiple health tracking features.", 249.99, 75, "http://images.unsplash.com/photo-1598516802414-50a01bee818d"),
        new Product("Camera", "A digital camera with high resolution and multiple shooting modes.", 499.99, 20, "https://cdn.mos.cms.futurecdn.net/4wpKrH93D37dDPTisdqGy4.jpg"),
        new Product("Tablet", "A versatile tablet perfect for work and entertainment.", 399.99, 40, "https://images.unsplash.com/photo-1498050108023-c5249f4df085"),
        new Product("Gaming Console", "A next-gen gaming console with stunning graphics and performance.", 499.99, 25, "https://cdn.thewirecutter.com/wp-content/media/2023/05/gamingconsoles-2048px-00730-3x2-1.jpg"),
        new Product("Bluetooth Speaker", "A portable Bluetooth speaker with deep bass and clear sound.", 59.99, 150, "https://m.media-amazon.com/images/I/718yxonHN8L.jpg"),
        new Product("Wireless Mouse", "A comfortable and precise wireless mouse with long battery life.", 29.99, 200, "https://m.media-amazon.com/images/I/51sTLdrBAPL._AC_UF1000,1000_QL80_.jpg"),
        new Product("Mechanical Keyboard", "A mechanical keyboard with customizable RGB backlighting.", 129.99, 80, "https://m.media-amazon.com/images/I/71-+n3UMsML._AC_UF894,1000_QL80_.jpg"),
        new Product("Gaming Chair", "An ergonomic gaming chair with adjustable height and lumbar support.", 199.99, 45, "https://www.ikea.com/sa/en/images/products/utespelare-gaming-chair-bomstad-black__0984819_pe816424_s5.jpg?f=s"),
        new Product("4K Monitor", "A 27-inch 4K monitor with stunning color accuracy and sharpness.", 349.99, 25, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQik1Qwo7SODm4ldTUtgwK3yanavUv-Nh6VkA&s"),
        new Product("External Hard Drive", "A 2TB external hard drive with fast data transfer speeds.", 89.99, 120, "https://m.media-amazon.com/images/I/71lG8EHlRNL.jpg"),
        new Product("Drone", "A high-performance drone with 4K camera and GPS navigation.", 599.99, 15, "https://cdn.mos.cms.futurecdn.net/kXUY9hyetVzhZ2scwJP7p3-1200-80.jpg"),
        new Product("VR Headset", "An immersive VR headset compatible with multiple platforms.", 399.99, 35, "https://m.media-amazon.com/images/I/61uBS5UX85L._AC_UF1000,1000_QL80_.jpg"),
        new Product("Fitness Tracker", "A sleek fitness tracker with heart rate monitoring and GPS.", 149.99, 90, "https://m.media-amazon.com/images/I/61Bugm3Wo+L._AC_UF894,1000_QL80_.jpg"),
        new Product("Electric Toothbrush", "An electric toothbrush with multiple brushing modes and a long battery life.", 79.99, 200, "https://m.media-amazon.com/images/I/71Ipo1ZbMFL._AC_UF1000,1000_QL80_.jpg"),
        new Product("Smart Thermostat", "A smart thermostat that learns your preferences and saves energy.", 249.99, 40, "https://m.media-amazon.com/images/I/81Nm1ol5zUL.jpg"),
        new Product("Instant Pot", "A multi-functional Instant Pot for quick and easy meals.", 99.99, 70, "https://assets.epicurious.com/photos/61e1e0c15bc241c4207af262/1:1/w_3026,h_3026,c_limit/InstantPot_HERO_012322_25936_V1_final.jpg"),
        new Product("Air Purifier", "An air purifier that removes pollutants and allergens from your home.", 199.99, 60, "https://m.media-amazon.com/images/I/714TjtxQJSL.jpg"),
        new Product("Electric Kettle", "A fast-boiling electric kettle with temperature control.", 49.99, 100, "https://assets.epicurious.com/photos/609e9d9848cd17a4fcdbf149/16:9/w_2560%2Cc_limit/The-Best-Electric-Kettle-11042018_V1.jpg"),
        new Product("Espresso Machine", "A high-quality espresso machine for perfect coffee at home.", 299.99, 20, "httpshttps://assets.epicurious.com/photos/62741684ef40ea9d3866a0be/3:2/w_4998,h_3332,c_limit/breville-bambino-espresso-maker_HERO_050422_8449_VOG_Badge_final.jpg"),
        new Product("Robot Vacuum", "A robot vacuum cleaner that efficiently cleans your home.", 399.99, 30, "https://media-cldnry.s-nbcnews.com/image/upload/t_fit-760w,f_auto,q_auto:best/newscms/2019_48/1512879/robot-vaccum-today-main-191126.jpg"),
        new Product("Smart Light Bulbs", "A set of smart light bulbs that can be controlled with your phone.", 69.99, 150, "https://tekled.co.uk/cdn/shop/products/smart-a60-e27-edison-screw-9w-rgb-white-led-gls-bulb-main.jpg?v=1633985813&width=1170"),
        new Product("Security Camera", "A smart security camera with motion detection and night vision.", 149.99, 50, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRRbJOQaMAewIjZCPwmZKhQj87hg8Dp0G_BPw&s"),
        new Product("Portable Power Bank", "A portable power bank with high capacity and fast charging.", 39.99, 200, "https://m.media-amazon.com/images/I/514bgqH8f8L._AC_UF1000,1000_QL80_.jpg"),
        new Product("Electric Scooter", "A foldable electric scooter with long battery life and fast speed.", 499.99, 25, "https://m.media-amazon.com/images/I/41HBjzMJC7L.jpg"),

        });

        }
    }
}