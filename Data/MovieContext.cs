using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
namespace MovieApi.Data;
public class MovieContext(DbContextOptions<MovieContext> options):DbContext(options) {
public DbSet<Movie> Movies => Set<Movie>(); public DbSet<MovieDetails> MovieDetails => Set<MovieDetails>(); public DbSet<Review> Reviews => Set<Review>(); public DbSet<Actor> Actors => Set<Actor>(); public DbSet<MovieActor> MovieActors => Set<MovieActor>(); public DbSet<Director> Directors => Set<Director>(); public DbSet<Country> Countries => Set<Country>(); public DbSet<Genre> Genres => Set<Genre>(); public DbSet<MovieGenre> MovieGenres => Set<MovieGenre>();
protected override void OnModelCreating(ModelBuilder b) {
b.Entity<Movie>().HasOne(x=>x.Details).WithOne().HasForeignKey<MovieDetails>(x=>x.MovieId);
b.Entity<Movie>().HasMany(x=>x.Reviews).WithOne().HasForeignKey(x=>x.MovieId);
b.Entity<MovieActor>().HasKey(x=>new{x.MovieId,x.ActorId}); b.Entity<MovieActor>().HasOne(x=>x.Movie).WithMany(x=>x.MovieActors).HasForeignKey(x=>x.MovieId); b.Entity<MovieActor>().HasOne(x=>x.Actor).WithMany(x=>x.MovieActors).HasForeignKey(x=>x.ActorId);
b.Entity<MovieGenre>().HasKey(x=>new{x.MovieId,x.GenreId}); b.Entity<MovieGenre>().HasOne(x=>x.Movie).WithMany(x=>x.MovieGenres).HasForeignKey(x=>x.MovieId); b.Entity<MovieGenre>().HasOne(x=>x.Genre).WithMany().HasForeignKey(x=>x.GenreId);
b.Entity<Movie>().HasOne(x=>x.Director).WithMany().HasForeignKey(x=>x.DirectorId); b.Entity<Movie>().HasOne(x=>x.Country).WithMany().HasForeignKey(x=>x.CountryId);
}}
