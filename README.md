Note: 2 pages(Professor and enrollment) still need refinement. to be completed by end of week.
# Course Registration Management System

A web-based course registration system built with **ASP.NET Core Razor Pages** and **MySQL**, designed to manage course, student, enrollment, and professor data. This project includes features for adding, editing, and deleting course records, complete with server-side validation and exception handling.

---

## Technologies Used

- **ASP.NET Core Razor Pages**
- **MySQL**
- **Entity Framework Core**
- **C#**

---

## Database Structure

### Tables:

- `student`
- `course`
- `professor`
- `enrollment`
- `__efmigrationshistory`

### Key Relationships:

- `student` and `course` are connected via the `enrollment` table with composite keys and cascading deletes.
- `course` includes a CHECK constraint ensuring `Credits > 0`.

### Sample Data Included:

```sql
-- student
(1, 'Alice Johnson', '2002-05-15', 1, 'Computer Science'),
(2, 'Bob Smith', '2001-07-22', 1, 'Mathematics'),
(3, 'Cara Lee', '2000-11-05', 0, 'Physics')

-- professor
(1001, 'Dr. Emily Carter', 'Computer Science'),
(1002, 'Dr. John Doe', 'Mathematics'),
(1003, 'Dr. Sarah Connor', 'Physics')

-- course
(22, 'were', 3),
(24, 'hey', 2)
Features:
-Display and manage a list of all courses.

-Add new courses with input validation:

-Valid numeric values for courseId and credits

-Alphabetic validation for courseName

-Edit existing course data, with exception handling for duplicate or non-existent IDs.

-Delete courses with user confirmation.

-Exception messages are descriptive and user-friendly.

Running the Project:
-Restore the database
-Use the exported_db.sql file to create the schema and populate sample data:
bash
Copy
Edit
mysql -u root -p < exported_db.sql
Run the ASP.NET project
Make sure your appsettings.json is configured with the correct connection string.

Use the app:
Navigate to /Courses to manage course entries.

Validation & Exception Handling
Custom error messages are shown for:
-Invalid input types (e.g. text in Credits field)
-Missing required fields
-Duplicate course IDs
-Attempting to update non-existent entries

Developer Notes:
-The Razor Page model (coursesModel.cshtml.cs) handles form submission logic via OnPost() and renders data using OnGet().

-IsString() is a custom utility to ensure that course names only contain letters.
(Note: This project was my first razor page project and more of a fullstack playground. As a result there is a strong emphasis on error handling directed towards user's inputs.
There's certainly alot of error checking implemented because sometimes the html forms are not always doing what they are suppose
to. So i was fighting to provide different layers of error handling on backend as well. I love when it's userfriendly as i feel it could save the user time on their daily activity on your product and 
save them from unecessary stress. If this could help you for a related project feel free to clone and update to your liking. it would obviously be advisable to put let focus on user input if you are dealing with
a larger or more complex database or rather implement it with javascript directly in your .cshtml page)

-Primary keys (like CourseID) are not allowed to be modified directly once created.

File Overview:
-exported_db.sql – Full SQL schema and data dump.

-Pages/courses.cshtml.cs Pages/student.cshtml.cs, Pages/professor.cshtml.cs, enrollment– Core Razor Page logic for handling course management.
-ApplicationDbContext.cs – EF Core database context mapping the tables.

Lessons Learned
-How to use EF Core with MySQL in Razor Pages.
-Implementing input validation and exception handling in server-side forms.
-Managing foreign key relationships and database constraints
