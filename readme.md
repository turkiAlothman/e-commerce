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
   make sure that port 8080 is not busy. Visit http://localhost:8080. Dummy data will be inserted during the application setup process so that you can test the system.  


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

## user authentication and registration

#### User Login

- Employees can securely log in to the system using their email address and password.
- Upon successful authentication, employees gain access to their personalized dashboard and features.

#### User Registration

- Employees can register for the system by receiving an invitation email containing a registration link.
- To complete the registration process, employees click on the registration link provided in the invitation email.
- Registration involves setting up a password and providing necessary details to create a user account.


## Authentication and Authorizations:

#### Authentication:
- Users must authenticate before accessing any features mentioned above.

#### Authorizations:

- All functionalities are accessible by authenticated users, except the following:

1. **Assigning Employees to a Specific Task**:
   - Only the reporter (creator) of the task can assign employees to it.

2. **Removing Employees from a Specific Task**:
   - Only the reporter (creator) of the task can remove employees from it.

3. **Editing Task Attributes (Title, Description, Status, Priority)**:
   - Only the reporter (creator) or the assignee of the task can edit its attributes.

4. **Adding Comments in a Specific Task**:
   - All authenticated users can add comments to tasks.

5. **Deleting Comments in a Specific Task**:
   - Only the owner of the comment can delete it.

6. **Inviting New Employees**:
   - Only managers have the authority to invite new employees.

7. **Creating New Projects**:
   - Only managers have the authority to create new projects.

8. **Adding Employees to a Project**:
   - Only managers have the authority to add employees to projects.

9. **Removing Employees from a Project**:
 - Only managers have the authority to remove employees from projects.

10. **registration**:
 - employee cannot register until he invited by a manager.
