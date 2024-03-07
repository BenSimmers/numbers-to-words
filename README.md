# Number to Words

## How to build and Run

### API

If using the dotnet CLI

```bash
cd api/
dotnet build # optional
dotnet run
```

If using Visual Studio or Jetbrains Rider
- Open project and run as you would a normal application
- Notes If on Visual Studio use this as a guide to run the [API](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio)
- Notes If on Rider use this as a guide to run the [API](https://www.jetbrains.com/help/rider/Running_LaunchSettings.html#running-and-debugging-launch-profiles)

### Client

```bash
cd client/
npm install
npm run build # optional
npm run dev
```

The API will be running on `http://localhost:5000` with Swagger docs on `http://localhost:5000/swagger/index.html`.

The client will be running on `http://localhost:5173`.
