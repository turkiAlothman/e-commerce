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
        new Product("Smartwatch", "A smartwatch with multiple health tracking features.", 249.99, 75, "https://images.unsplash.com/photo-1519400190073-6205f5f9b6e6"),
        new Product("Camera", "A digital camera with high resolution and multiple shooting modes.", 499.99, 20, "https://images.unsplash.com/photo-1519183071298-a2962f556a51"),
        new Product("Tablet", "A versatile tablet perfect for work and entertainment.", 399.99, 40, "https://images.unsplash.com/photo-1498050108023-c5249f4df085"),
        new Product("Gaming Console", "A next-gen gaming console with stunning graphics and performance.", 499.99, 25, "https://images.unsplash.com/photo-1612631345475-1e4a3e835bc1"),
        new Product("Bluetooth Speaker", "A portable Bluetooth speaker with deep bass and clear sound.", 59.99, 150, "https://images.unsplash.com/photo-1598743242425-d6f4e5e7b7f6"),
        new Product("Wireless Mouse", "A comfortable and precise wireless mouse with long battery life.", 29.99, 200, "https://images.unsplash.com/photo-1561668032-ea6c054dcf03"),
        new Product("Mechanical Keyboard", "A mechanical keyboard with customizable RGB backlighting.", 129.99, 80, "https://images.unsplash.com/photo-1555617117-08a57d016db3"),
        new Product("Gaming Chair", "An ergonomic gaming chair with adjustable height and lumbar support.", 199.99, 45, "https://images.unsplash.com/photo-1602090202763-d2d7dce92764"),
        new Product("4K Monitor", "A 27-inch 4K monitor with stunning color accuracy and sharpness.", 349.99, 25, "https://images.unsplash.com/photo-1517336714731-489689fd1ca8"),
        new Product("External Hard Drive", "A 2TB external hard drive with fast data transfer speeds.", 89.99, 120, "https://images.unsplash.com/photo-1587825140809-57527b6a6f72"),
        new Product("Drone", "A high-performance drone with 4K camera and GPS navigation.", 599.99, 15, "https://images.unsplash.com/photo-1516997128236-22f0d3fc1d9e"),
        new Product("VR Headset", "An immersive VR headset compatible with multiple platforms.", 399.99, 35, "https://images.unsplash.com/photo-1600566754693-466c9f51aa91"),
        new Product("Fitness Tracker", "A sleek fitness tracker with heart rate monitoring and GPS.", 149.99, 90, "https://images.unsplash.com/photo-1533236646067-e2f9cddb4c90"),
        new Product("Electric Toothbrush", "An electric toothbrush with multiple brushing modes and a long battery life.", 79.99, 200, "https://images.unsplash.com/photo-1557682224-5b8590cd9ec5"),
        new Product("Smart Thermostat", "A smart thermostat that learns your preferences and saves energy.", 249.99, 40, "https://images.unsplash.com/photo-1599427309963-9bdb5aeff6ef"),
        new Product("Instant Pot", "A multi-functional Instant Pot for quick and easy meals.", 99.99, 70, "https://images.unsplash.com/photo-1592578629296-1378c967f893"),
        new Product("Air Purifier", "An air purifier that removes pollutants and allergens from your home.", 199.99, 60, "https://images.unsplash.com/photo-1583341374641-91f8dd6630e5"),
        new Product("Electric Kettle", "A fast-boiling electric kettle with temperature control.", 49.99, 100, "https://images.unsplash.com/photo-1615475391506-22980a87fc80"),
        new Product("Espresso Machine", "A high-quality espresso machine for perfect coffee at home.", 299.99, 20, "https://images.unsplash.com/photo-1577982936210-382a71b0095e"),
        new Product("Robot Vacuum", "A robot vacuum cleaner that efficiently cleans your home.", 399.99, 30, "https://images.unsplash.com/photo-1614203462064-8f3065cc9b7e"),
        new Product("Smart Light Bulbs", "A set of smart light bulbs that can be controlled with your phone.", 69.99, 150, "https://images.unsplash.com/photo-1586363104863-9f1d7fbff4d1"),
        new Product("Security Camera", "A smart security camera with motion detection and night vision.", 149.99, 50, "https://images.unsplash.com/photo-1581091870679-2f0b3679caec"),
        new Product("Portable Power Bank", "A portable power bank with high capacity and fast charging.", 39.99, 200, "https://images.unsplash.com/photo-1546159070-80d9917b7cd2"),
        new Product("Electric Scooter", "A foldable electric scooter with long battery life and fast speed.", 499.99, 25, "https://images.unsplash.com/photo-1548191265-cc70d3d45ba1"),

        });

        }
    }
}