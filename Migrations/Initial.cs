using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MovieApi.Data;
namespace MovieApi.Migrations;
[DbContext(typeof(MovieContext))]
[Migration("202611240001_Initial")]
public class Initial:Migration {
protected override void Up(MigrationBuilder b){
b.Sql("CREATE TABLE Directors (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)");
b.Sql("CREATE TABLE Countries (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)");
b.Sql("CREATE TABLE Genres (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL)");
b.Sql("CREATE TABLE Actors (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, BirthYear INTEGER NOT NULL)");
b.Sql("CREATE TABLE Movies (Id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT NOT NULL, Year INTEGER NOT NULL, Genre TEXT NOT NULL, Duration INTEGER NOT NULL, DirectorId INTEGER REFERENCES Directors(Id), CountryId INTEGER REFERENCES Countries(Id))");
b.Sql("CREATE TABLE MovieDetails (Id INTEGER PRIMARY KEY AUTOINCREMENT, MovieId INTEGER NOT NULL UNIQUE REFERENCES Movies(Id) ON DELETE CASCADE, Synopsis TEXT NOT NULL, Language TEXT NOT NULL, Budget TEXT NOT NULL)");
b.Sql("CREATE TABLE Reviews (Id INTEGER PRIMARY KEY AUTOINCREMENT, MovieId INTEGER NOT NULL REFERENCES Movies(Id) ON DELETE CASCADE, ReviewerName TEXT NOT NULL, Comment TEXT NOT NULL, Rating INTEGER NOT NULL CHECK(Rating BETWEEN 1 AND 5))");
b.Sql("CREATE TABLE MovieActors (MovieId INTEGER NOT NULL REFERENCES Movies(Id) ON DELETE CASCADE, ActorId INTEGER NOT NULL REFERENCES Actors(Id) ON DELETE CASCADE, Role TEXT NOT NULL, PRIMARY KEY(MovieId,ActorId))");
b.Sql("CREATE TABLE MovieGenres (MovieId INTEGER NOT NULL REFERENCES Movies(Id) ON DELETE CASCADE, GenreId INTEGER NOT NULL REFERENCES Genres(Id) ON DELETE CASCADE, PRIMARY KEY(MovieId,GenreId))");
foreach(var pair in new[]{("Movies","DirectorId"),("Movies","CountryId"),("Reviews","MovieId"),("MovieActors","ActorId"),("MovieGenres","GenreId")}) b.Sql($"CREATE INDEX IX_{pair.Item1}_{pair.Item2} ON {pair.Item1}({pair.Item2})");
}
protected override void Down(MigrationBuilder b){foreach(var t in new[]{"MovieGenres","MovieActors","Reviews","MovieDetails","Movies","Actors","Genres","Countries","Directors"})b.Sql($"DROP TABLE {t}");}}
