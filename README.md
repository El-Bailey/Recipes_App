# Recipes_App
Visual Studio 2019 ASP.NET 3.1 MVC App (no Entity Framework dependency) to Create, View, Edit, and Delete Recipes in SQL Server DB. Project also includes User Registration, Login, Logout, and Password hashing using RFC2898.

<h3>Dependencies</h3>
<ul><li>Visual Studio 2019</li>
  <li>SQL Server (project is configured for localhost)</li>
  <li>SQL Server Management Studio</li></ul>

<h3>Instructions</h3>
<ol><li>Open SQL Server Management Studio</li>
  <li>Open SQL_Setup.sql</li>
  <li>Execute the script. (Some blocks may need to run separately.)</li>
  <li>Once the database is created, the table is created, and the stored procedures are created..</li>
  <li>Open the project in Visual Studio 2019.</li>
  <li>Resolve any dependencies by downloading the necessary NuGet packages.</li>
  <li>Update the connection string in appsettings.json to reflect your connection to the server on which you created the database.</li>
  <li>Run the project</li>
  <li>Register to create user account</li>
  <li>Login</li>
  <li>Keep track of your recipes!</li>
<br><br>
<h2>Upcoming Enhancements</h2>
-Testing
-Allow users to update passwords
-Allow users to reset passwords
-Ownership of Recipes<br>
-Break out individual ingredients and their measurements in the database and in the forms.<br>
-UI Improvements<br>
-AWS hosting (as a separate project)
