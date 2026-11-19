# Dotnet API registration and login
Creating this repository to quickly be able to make projects that require logging in and registration.
Examples of this could be:
- Blog
- Ticket system
- Social media clone
- Information systems
- Web portals

# Stack
This repository uses .NET Core 9.0 with Identity, JWT Bearer tokens and MySQL with Pomelo.

# Endpoints
- /api/account/register (POST)
- /api/account/login

# Setup
1. Set your MySQL password in appsettings.Development.json → ConnectionStrings:DefaultConnection (or keep it out of the file: dotnet user-secrets init and dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=...;password=...").
2. Create the database:
dotnet tool install --global dotnet-ef   # once
dotnet ef database update
3. Run: dotnet run → Swagger at http://localhost:5299/swagger
To protect an endpoint, add [Authorize] and read the current user with User.GetUserName() from extensions/ClaimsExtensions.cs.

# Note on AI use
This repository was built on a snapshot of a .NET Core API project I made for my own social media platform that I worked on a year ago with minimal AI use apart from a few standard tasks using Github Copilot. I used Claude to provide further documentation and to clean controllers and Dto's that were not relevant to the functionality of registering and logging in. The programming I did on this project stays the same.
