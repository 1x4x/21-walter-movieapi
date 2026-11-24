namespace MovieApi.Models;
public class Movie { public int Id {get;set;} public string Title {get;set;}=""; public int Year {get;set;} public string Genre {get;set;}=""; public int Duration {get;set;} public MovieDetails? Details {get;set;} public List<Review> Reviews {get;set;}=[]; public List<MovieActor> MovieActors {get;set;}=[]; public int? DirectorId {get;set;} public Director? Director {get;set;} public int? CountryId {get;set;} public Country? Country {get;set;} public List<MovieGenre> MovieGenres {get;set;}=[]; }
public class MovieDetails { public int Id {get;set;} public int MovieId {get;set;} public string Synopsis {get;set;}=""; public string Language {get;set;}=""; public decimal Budget {get;set;} }
public class Review { public int Id {get;set;} public int MovieId {get;set;} public string ReviewerName {get;set;}=""; public string Comment {get;set;}=""; public int Rating {get;set;} }
public class Actor { public int Id {get;set;} public string Name {get;set;}=""; public int BirthYear {get;set;} public List<MovieActor> MovieActors {get;set;}=[]; }
public class MovieActor { public int MovieId {get;set;} public int ActorId {get;set;} public string Role {get;set;}=""; public Movie Movie {get;set;}=null!; public Actor Actor {get;set;}=null!; }
public class Director { public int Id {get;set;} public string Name {get;set;}=""; }
public class Country { public int Id {get;set;} public string Name {get;set;}=""; }
public class Genre { public int Id {get;set;} public string Name {get;set;}=""; }
public class MovieGenre { public int MovieId {get;set;} public int GenreId {get;set;} public Movie Movie {get;set;}=null!; public Genre Genre {get;set;}=null!; }
