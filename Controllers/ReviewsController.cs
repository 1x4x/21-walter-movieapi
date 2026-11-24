using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;using MovieApi.Data;using MovieApi.DTOs;using MovieApi.Models;
namespace MovieApi.Controllers;
[ApiController] public class ReviewsController(MovieContext db):ControllerBase {
[HttpGet("/api/movies/{movieId:int}/reviews")] public async Task<IActionResult> Get(int movieId){if(!await db.Movies.AnyAsync(x=>x.Id==movieId))return NotFound();return Ok(await db.Reviews.Where(x=>x.MovieId==movieId).Select(r=>new ReviewDto(r.Id,r.ReviewerName,r.Comment,r.Rating)).ToListAsync());}
[HttpPost("/api/movies/{movieId:int}/reviews")] public async Task<IActionResult> Post(int movieId,ReviewCreateDto d){if(!await db.Movies.AnyAsync(x=>x.Id==movieId))return NotFound();var r=new Review{MovieId=movieId,ReviewerName=d.ReviewerName,Comment=d.Comment,Rating=d.Rating};db.Add(r);await db.SaveChangesAsync();return Created($"/api/reviews/{r.Id}",new ReviewDto(r.Id,r.ReviewerName,r.Comment,r.Rating));}
[HttpGet("/api/reviews/{id:int}")] public async Task<IActionResult> One(int id){var r=await db.Reviews.FindAsync(id);return r==null?NotFound():Ok(new ReviewDto(r.Id,r.ReviewerName,r.Comment,r.Rating));}
[HttpDelete("/api/reviews/{id:int}")] public async Task<IActionResult> Delete(int id){var r=await db.Reviews.FindAsync(id);if(r==null)return NotFound();db.Remove(r);await db.SaveChangesAsync();return NoContent();}}
