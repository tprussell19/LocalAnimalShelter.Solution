# _Local Animal Shelter_

#### _API for an animal shelter_

#### By _Thomas Russell_

## Technologies Used

- _C#_
- _ASP.NET Core_
- _MySQL_
- _Entity_
- _EF Core_
- _Markdown_

## Description

_A RESTful API used to keep track of animals at a local animal shelter. Users can get a list of all entries in the database, get a specific entry by its id, as well as add, edit, and delete entries. The API also uses version control, has enabled CORS, and uses Swagger documentation._

## Setup and Use

### Prerequisites

- [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- A text editor like [VS Code](https://code.visualstudio.com/)
- A browser like Chrome or Firefox
- [MySQL Workbench](https://dev.mysql.com/downloads/workbench/)
- An application to consume the api like [Postman](https://www.postman.com/downloads/) or VS Code extension [Thunder Client](https://marketplace.visualstudio.com/items?itemName=rangav.vscode-thunder-client).

### Installation

1. Clone the repository: `$ git clone https://github.com/tprussell19/LocalAnimalShelter.Solution`
2. Navigate to the `LocalAnimalShelter.Solution/` directory on your computer
3. Open with your preferred text editor to view the code base
4. Create a database using SQL via Entity:

- Navigate to `LocalAnimalShelter.Solution/LocalAnimalShelter` in your command line
- Create a file named `appsettings.json` and paste the following code:
  ```
  {
    "Logging": {
      "LogLevel": {
        "Default": "Warning",
        "System": "Information",
        "Microsoft": "Information"
      }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=thomas_russell;uid=root;pwd=[YOUR-PASSWORD-GOES-Here];"
    }
  }
  ```
  Note: Be sure to change `[YOUR-PASSWORD-GOES-HERE]` to your actual MySQL password.
- Once the migration has been added, run the command `dotnet ef database update` to setup your database. (If this command throws and error, you might have to run the command `dotnet tool install --global dotnet-ef` first.)

5. To run the app:
   - Navigate to `LocalAnimalShelter.Solution/LocalAnimalShelter` in your command line
   - Run the command `dotnet restore` to restore the dependencies that are listed in the .csproj
   - Run the commmand `dotnet build` to build the project and its dependencies into a set of binaries
   - Finally, run the command `dotnet run` to run the project!
   - Note: `dotnet run` also restores and builds the project, so you can use this single command to start the app and launch
   - Once the application is running, you can use Postman or Thunder Client to access the API.

## API Documentation

This API is documented using Swagger. While the app is running, navigate in your browser to `http://localhost:5000/swagger` to see the documentation for all of the API's endpoints.

## Known Bugs

- _No known bugs_

## License

_MIT © Thomas Russell 2021_

## Contact Information

Thomas Russell _t.p.russell19@gmail.com_
