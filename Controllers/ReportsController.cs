using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;using MovieApi.Data;
namespace MovieApi.Controllers;
[ApiController,Route("api/reports")] public class ReportsController(MovieContext db):ControllerBase {
private async Task<List<MovieApi.Models.Movie>> Movies()=>await db.Movies.AsNoTracking().Include(m=>m.Reviews).Include(m=>m.Country).Include(m=>m.MovieGenres).ThenInclude(g=>g.Genre).ToListAsync();
private static IEnumerable<string> Genres(MovieApi.Models.Movie m)=>m.MovieGenres.Count==0?new[]{m.Genre}:m.MovieGenres.Select(g=>g.Genre.Name).Distinct();
[HttpGet("movies/top5pergenre")] public async Task<IActionResult> Top()=>Ok((await Movies()).Where(m=>m.Reviews.Count>0).SelectMany(m=>Genres(m).Select(g=>new{Genre=g,m.Title,Rating=m.Reviews.Average(r=>r.Rating)})).GroupBy(x=>x.Genre).Select(g=>new{Genre=g.Key,Movies=g.OrderByDescending(x=>x.Rating).Take(5)}));
[HttpGet("movies/average-ratings")] public async Task<IActionResult> Average()=>Ok((await Movies()).SelectMany(m=>Genres(m).SelectMany(g=>m.Reviews.Select(r=>new{Genre=g,r.Rating}))).GroupBy(x=>x.Genre).Select(g=>new{Genre=g.Key,Average=g.Average(x=>x.Rating)}));
[HttpGet("actors/most-active")] public async Task<IActionResult> Active()=>Ok(await db.Actors.Select(a=>new{a.Name,Movies=a.MovieActors.Count}).OrderByDescending(a=>a.Movies).ToListAsync());
[HttpGet("movies/longest-per-country")] public async Task<IActionResult> Longest()=>Ok((await Movies()).Where(m=>m.Country!=null).GroupBy(m=>m.Country!.Name).Select(g=>g.OrderByDescending(m=>m.Duration).Select(m=>new{Country=g.Key,m.Title,m.Duration}).First()));
[HttpGet("movies/with-most-reviews")] public async Task<IActionResult> Most()=>Ok((await Movies()).OrderByDescending(m=>m.Reviews.Count).Select(m=>new{m.Title,Reviews=m.Reviews.Count}).FirstOrDefault());
[HttpGet("genres/popular")] public async Task<IActionResult> Popular()=>Ok((await Movies()).SelectMany(Genres).GroupBy(g=>g).Select(g=>new{Genre=g.Key,Movies=g.Count()}).OrderByDescending(g=>g.Movies));}
