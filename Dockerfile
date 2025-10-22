# Use the official .NET 8 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore as distinct layers
COPY *.sln ./
COPY Employee\ Tasks\ Management\ Sample\ Project/*.csproj ./Employee\ Tasks\ Management\ Sample\ Project/
COPY Domain/*.csproj ./Domain/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY TasksManagement.Domain/*.csproj ./TasksManagement.Domain/
COPY TasksManagement.Infrastructure/*.csproj ./TasksManagement.Infrastructure/
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the project
WORKDIR /src/Employee Tasks Management Sample Project
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET 8 runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 80
EXPOSE 80

# Set the entrypoint
ENTRYPOINT ["dotnet", "Employee Tasks Management Sample Project.dll"]