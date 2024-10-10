
# MovieStore

MovieStore is a web-based application for browsing and managing movies. The application is built using ASP.NET Core, following the MVC (Model-View-Controller) design pattern. 

## Features

- **Browse Movies**: View a list of available movies.
- **Search**: Search for movies based on title, genre, and more.
- **Manage Movies**: Add, update, and delete movie records.
- **Responsive Design**: Optimized for different screen sizes with a modern and clean UI.

## Project Structure

The project follows the standard structure of an ASP.NET Core MVC application:

- **Controllers/**: Contains the controllers which handle HTTP requests and return responses.
- **Views/**: Contains the views, which are the front-end templates rendered to the user.
- **Data/**: Handles the database context and data models.
- **Repositories/**: Contains the logic for interacting with the database (data access).
- **wwwroot/**: Static files (CSS, JS, images, etc.).
- **appsettings.json**: Configuration settings for the application, such as database connections.

## Getting Started

### Prerequisites

To run this project locally, you need the following:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/MovieStore.git
   ```
2. Navigate to the project directory:
   ```bash
   cd MovieStore
   ```
3. Restore the dependencies:
   ```bash
   dotnet restore
   ```

### Running the Application

To run the application locally, use the following commands:

1. Build the project:
   ```bash
   dotnet build
   ```
2. Run the project:
   ```bash
   dotnet run
   ```

The application will be available at `https://localhost:5001` (or as specified in the launch settings).

### Database Setup

To configure the database, follow these steps:

1. Update the connection string in `appsettings.json` to match your database configuration.
2. Run the following command to apply migrations:
   ```bash
   dotnet ef database update
   ```

### Contributing

Feel free to fork the repository and submit pull requests. For major changes, please open an issue first to discuss what you would like to change.

### License

This project is licensed under the MIT License.
