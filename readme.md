# e-commerce

This simple e-commerce application, built with ASP.NET Core using the MVC architecture, allows users to view products, add items to their cart, and make payments. The application is monolithic and includes both HTML endpoints and RESTful APIs to enhance system responsiveness. You can view the RESTful APIs at the /swagger route. Some endpoints require authentication; please register at /auth/signup (accessible via the top right corner) to authenticate yourself. The application uses JWT combined with cookies for session management and tracking.


## project's dependencies and packages
- **Humanizer:** provide utilizations to convert DateTime object into human readble format date.
- **MimeKit:** Mail client tool used to sends emails within the application.
- **JsonPatch:** provide utilizations to apply a smooth patch request.
- **NewtonsoftJson:** object to json converter.
- **Pomelo.EntityFrameworkCore.MySql** Mysql DBMS driver.
- **Microsoft.EntityFrameworkCore** most pupoler ORM system in asp.net.
- **RandomString4Net** - random text generator.


### user authentication and registration

#### User Login

- Employees can securely log in to the system using their email address and password.
- Upon successful authentication, employees gain access to their personalized dashboard and features.

#### User Registration

- Employees can register for the system by receiving an invitation email containing a registration link.
- To complete the registration process, employees click on the registration link provided in the invitation email.
- Registration involves setting up a password and providing necessary details to create a user account.

#### Password Management

- Employees can reset their passwords if they forget them or need to change them for security reasons.
- To reset the password, employees initiate the process by clicking on the "Reset Password" link.
- An email containing a password reset link is sent to the employee's registered email address.
- By clicking on the password reset link, employees can securely set a new password for their account.


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
