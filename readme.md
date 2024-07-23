# TaskManager

***TaskManager*** is a user-friendly web application developed using ASP.NET Core 8. It serves as a comprehensive tool for organizations to effectively tasks across multiple projects and teams. Within this platform, employees have the flexibility to seamlessly create, delete, and update tasks as needed. Furthermore, they can enhance their task reports by incorporating comments, attachments, and additional details. For managers, TaskManager offers advanced functionalities such as project creation, employees invitations, and access to detailed statistics. These features empower managers to optimize project workflows and productivity within the organization.

TaskManager is designed following the principles of ***Domain-Driven Design (DDD)*** and employing a ***clean architecture*** approach. It is structured into three main components: infrastructure, Domain, and Application. This setup ensures efficiency and maintainability.

you can use the application here : https://taskmanager20240328032228.azurewebsites.net

please use the below crediantils in order to access the application functionalities

email = SattamDfo@yahoo.com
<br/>
password = jioejv*U&&^ 


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
