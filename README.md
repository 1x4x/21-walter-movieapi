# 🎬 MovieApi
Dark-mode ASP.NET Core controller API, .NET 10, EF Core and SQLite.

## 🚀 Run
Install .NET 10 SDK. Run `dotnet restore` then `dotnet run --urls http://localhost:5080`. Open http://localhost:5080. OpenAPI: `/openapi/v1.json`. Seed data is inserted on startup.

## 🔗 Relationships
Movie → MovieDetails (1:1), Reviews (1:N), Actors (N:M via MovieActor with Role), Director and Country (N:1), Genres (N:M via MovieGenre). The legacy Genre string is retained for the exercise.

## 📡 Endpoints
- GET/POST `/api/movies`; GET/PUT/DELETE `/api/movies/{id}`; GET `/api/movies/{id}/details`.
- Filters: `?genre=Drama&year=1994`, `?actor=Tom+Hanks`, `?withactors=true` on single movie.
- GET/POST `/api/actors`; GET/PUT/DELETE `/api/actors/{id}`.
- POST `/api/movies/{movieId}/actors` with `{"actorId":1,"role":"Lead"}` or `/api/movies/{movieId}/actors/{actorId}?role=Lead`.
- GET/POST `/api/movies/{movieId}/reviews`; GET/DELETE `/api/reviews/{id}`.
- GET `/api/reports/` followed by `movies/top5pergenre`, `movies/average-ratings`, `actors/most-active`, `movies/longest-per-country`, `movies/with-most-reviews`, `genres/popular`.

## 🧪 Postman checks
GET movies → 200; POST movie with valid title/year/genre/duration → 201; POST with duration 0 → 400; GET missing ID → 404; DELETE existing ID → 204. Review rating 6 → 400. Duplicate actor link → 409.

## ⚠️ Migration and exercise limitations
The included baseline is a manually authored SQLite migration, not scaffolded in Visual Studio. It has no generated model snapshot and pending-model warnings are suppressed to allow startup. Before further development, on a fresh disposable database remove the baseline migration, remove the warning suppression in Program.cs, install `dotnet-ef` version 10.0.0 and run `dotnet ef migrations add InitialCreate` then `dotnet ef database update`. Do not do this against an existing database without a migration plan. For the classroom requirement of one migration per relationship, reproduce each relationship incrementally and review generated migrations. The Git commit before scaffolding must be performed locally. No .NET SDK was available to build or test this project here. The API is anonymous; use locally only. Reports aggregate in memory for the small demo dataset.
