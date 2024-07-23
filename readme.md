# e-commerce

This simple e-commerce application, built with ASP.NET Core using the MVC architecture, allows users to view products, add items to their cart, and make payments. The application is monolithic and includes both HTML endpoints and RESTful APIs to enhance system responsiveness. You can view the RESTful APIs at the /swagger route. Some endpoints require authentication; please register at /auth/signup (accessible via the top right corner) to authenticate yourself. The application uses JWT combined with cookies for session management and tracking.

<br/>
<br/>

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

<br/>
<br/>

## Programming Languages and Frameworks

#### C#

- **Version:** 12.0

#### ASP.NET Core

- **Version:** 8

#### Frontend Technologies
- **JavaScript**
- **HTML**
- **CSS**
- **Bootstrap**
- **jQuery**
- **Razor**

<br/>
<br/>

## project's packages

- **MongoDB.Driver:** Mongodb Database Driver for ASP.NET Core.
- **JwtBearer:** provide jwt utilitis(token generation, token decoding, token signing).
- **JsonPatch:** provide utilizations to apply a smooth patch request.
- **NewtonsoftJson:** object to json converter.

<br/>
<br/>

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



<br/>
<br/>

## Web Service APIs (HTML)

### CartApi

**POST** /  
*Description:* home page that view products with pagination.  
*Authentication Required:* false

**GET** /{ProductId}  
*Description:* product details page.  
*Authentication Required:* false

**GET** /cart 
*Description:* Get user cart.  
*Authentication Required:* false

### authentication

**GET** /Auth/signin  
*Description:* signin page.  
*Authentication Required:* false

**POST** /Auth/signin  
*Description:* signin loin logic.  
*Authentication Required:* false


**GET** /Auth/signup  
*Description:* signup page.  
*Authentication Required:* false

**POST** /Auth/signup  
*Description:* perform signup logic.  
*Authentication Required:* false


**Get** /Auth/signout  
*Description:* perform Signou logic.  
*Authentication Required:* false

<br/>
<br/>

## System Behavior

Upon the user's initial request, a JWT session will be created that holds an empty product list. This means shopping cart items will be stored in the JWT token. Unauthenticated users are allowed to:

- View products
- View product details
- Add and remove products from the shopping cart

However, users cannot checkout until they authenticate. Once a user authenticates (signs in or signs out), the products stored in the shopping cart within the session will be copied to the database. This ensures the shopping cart can persist indefinitely.



<br/>
<br/>

## Project Root Folders Description

1. **Controllers**
   - APIs Registeration. Part of the MVC architecture, responsible for handling logic and business processes.
   - RESTful APIs registered in REST folder

2. **Views**
   - Part of the MVC architecture, responsible for rendering views and using the template engine.

3. **Models**
   - Contains domain classes used in the application (MongoDB documents).
   - **Repository:** Houses the DB query logic and processes.

4. **Services**
   - Contains independent services that can be injected into other components.

5. **Middlewares**
   - Middleware components included in the request pipeline.

6. **wwwroot**
   - Contains application resources such as JavaScript, CSS, and other static files.

7. **Utilities**
   - Ensures code remains modular and organized.

8. **Extensions**
   - Contains extension methods.

9. **Configurations**
   - Houses structured configuration options.

10. **DTOs (Data Transfer Objects)**
    - Contains classes for requests and responses.

