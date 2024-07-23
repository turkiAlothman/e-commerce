# e-commerce

This simple e-commerce application, built with ASP.NET Core using the MVC architecture, allows users to view products, add items to their cart, and make payments. The application is monolithic and includes both HTML endpoints and RESTful APIs to enhance system responsiveness. You can view the RESTful APIs at the /swagger route. Some endpoints require authentication; please register at /auth/signup (accessible via the top right corner) to authenticate yourself. The application uses JWT combined with cookies for session management and tracking.


## Setup Instructions
The entire application, including the database and web server, has been built using Docker and Docker Compose. To run the application, ensure that you have Docker Engine installed on your machine.

1. **Clone the repository:**
   ```sh
   git clone <repository-url>

2. **move to repository directory:**
   ```sh
   cd <repository-root>
3. **move to repository directory:**
   ```sh
   docker-compose up
   
   - make sure that port 8080 is not busy. Visit http://localhost:8080. Dummy data will be inserted during the application setup process so that you can test the system.  


## Programming Languages and Frameworks

#### C#

- **Version:** 12.0

#### ASP.NET Core

- **Version:** 8

#### Frontend Technologies
- **JavaScript**
- **HTML**
- **CSS**
- **Razor**

## project's packages

- **MongoDB.Driver:** Mongodb Database Driver for ASP.NET Core.
- **JwtBearer:** provide jwt utilitis(token generation, token decoding, token signing).
- **JsonPatch:** provide utilizations to apply a smooth patch request.
- **NewtonsoftJson:** object to json converter.

## RESTful APIs

### CartApi

**POST** /api/Cart/{ProductId}  
*Description:* Adds a product to the cart.  
*Authentication Required:* false

**DELETE** /api/Cart/{ProductId}  
*Description:* Removes a product from the cart.  
*Authentication Required:* false

**GET** /api/Cart  
*Description:* Retrieves the current cart contents.  
*Authentication Required:* false

**DELETE** /api/Cart/remove/{ProductId}  
*Description:* Removes a specific product from the cart.  
*Authentication Required:* false

**POST** /api/Cart/checkout  
*Description:* Initiates the checkout process for the current cart.  
*Authentication Required:* false

### ProductApi

**GET** /api/Product  
*Description:* Retrieves a list of all products.  
*Authentication Required:* false

**POST** /api/Product  
*Description:* Creates a new product.  
*Authentication Required:* true

**GET** /api/Product/{id}  
*Description:* Retrieves the details of a specific product.  
*Authentication Required:* false

**PATCH** /api/Product/{id}  
*Description:* Updates the details of a specific product.  
*Authentication Required:* true

**DELETE** /api/Product/{id}  
*Description:* Deletes a specific product.  
*Authentication Required:* true
