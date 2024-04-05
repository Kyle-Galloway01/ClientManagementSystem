using Microsoft.AspNetCore.Builder; // Import namespaces for ASP.NET Core
using Microsoft.AspNetCore.Hosting; // Import namespaces for ASP.NET Core
using Microsoft.Extensions.Configuration; // Import namespaces for ASP.NET Core
using Microsoft.Extensions.DependencyInjection; // Import namespaces for ASP.NET Core
using Microsoft.Extensions.Hosting; // Import namespaces for ASP.NET Core
using Microsoft.EntityFrameworkCore; // Import namespaces for Entity Framework Core
using Microsoft.AspNetCore.Identity; // Import namespaces for ASP.NET Core Identity

var builder = WebApplication.CreateBuilder(args); // Create a new instance of WebApplicationBuilder to build the web application

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("IdentityDB")); // Register the ApplicationDbContext with an in-memory database named "IdentityDB". ApplicationDbContext represents a session with the database and allows querying and saving of data.

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true) // Configure default identity options. This includes requiring confirmed accounts for sign-in, which adds an extra layer of security to the system.
    .AddEntityFrameworkStores<ApplicationDbContext>(); // Add Entity Framework Core stores for ASP.NET Core Identity. This registers the services required by ASP.NET Core Identity to store and manage user-related data.

builder.Services.AddControllersWithViews(); // Add MVC services to the service container. MVC (Model-View-Controller) is a design pattern used to structure web applications, where Models represent data, Views represent the UI, and Controllers handle user input and application logic.

var app = builder.Build(); // Build the application using the WebApplicationBuilder instance

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) // Check if the application is not running in a development environment
{
    app.UseExceptionHandler("/Home/Error"); // Use custom error handling middleware to handle exceptions and display user-friendly error pages
    app.UseHsts(); // Enable HTTP Strict Transport Security (HSTS) middleware. HSTS instructs browsers to only connect to the server over HTTPS to enhance security.
}

app.UseHttpsRedirection(); // Enable HTTPS redirection middleware. This redirects HTTP requests to HTTPS for secure communication.
app.UseStaticFiles(); // Enable serving static files. Static files such as HTML, CSS, and JavaScript files are served directly by the server without being processed by ASP.NET Core.

app.UseRouting(); // Enable routing middleware. Routing determines how incoming requests are mapped to the appropriate endpoint in the application.

app.UseAuthentication(); // Enable authentication middleware. Authentication verifies the identity of users accessing the application and grants access based on their credentials.
app.UseAuthorization(); // Enable authorization middleware. Authorization determines what resources and actions users are allowed to access based on their identity and role.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Configure the default route for MVC controllers. This route specifies the default controller, action, and optional parameters for handling requests.

app.Run(); // Execute the request pipeline to handle incoming HTTP requests and generate responses
