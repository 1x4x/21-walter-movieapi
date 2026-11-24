using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;using MovieApi.Data;using MovieApi.DTOs;using MovieApi.Models;
namespace MovieApi.Controllers;
[ApiController,Route("api/actors")] public class ActorsController(MovieContext db):ControllerBase {
[HttpGet] public async Task<IActionResult> Get()=>Ok(await db.Actors.Select(a=>new ActorDto(a.Id,a.Name,a.BirthYear,null)).ToListAsync());
[HttpGet("{id:int}")] public async Task<IActionResult> One(int id){var a=await db.Actors.FindAsync(id);return a==null?NotFound():Ok(new ActorDto(a.Id,a.Name,a.BirthYear));}
[HttpPost] public async Task<IActionResult> Post(ActorCreateDto d){var a=new Actor{Name=d.Name,BirthYear=d.BirthYear};db.Add(a);await db.SaveChangesAsync();return CreatedAtAction(nameof(One),new{id=a.Id},new ActorDto(a.Id,a.Name,a.BirthYear));}
[HttpPut("{id:int}")] public async Task<IActionResult> Put(int id,ActorCreateDto d){var a=await db.Actors.FindAsync(id);if(a==null)return NotFound();a.Name=d.Name;a.BirthYear=d.BirthYear;await db.SaveChangesAsync();return NoContent();}
[HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id){var a=await db.Actors.FindAsync(id);if(a==null)return NotFound();db.Remove(a);await db.SaveChangesAsync();return NoContent();}
[HttpPost("/api/movies/{movieId:int}/actors")] public async Task<IActionResult> Link(int movieId,MovieActorCreateDto d){if(!await db.Movies.AnyAsync(x=>x.Id==movieId)||!await db.Actors.AnyAsync(x=>x.Id==d.ActorId))return NotFound();if(await db.MovieActors.FindAsync(movieId,d.ActorId)!=null)return Conflict();db.MovieActors.Add(new(){MovieId=movieId,ActorId=d.ActorId,Role=d.Role});await db.SaveChangesAsync();return Created($"/api/movies/{movieId}/details",d);}
[HttpPost("/api/movies/{movieId:int}/actors/{actorId:int}")] public Task<IActionResult> LinkRoute(int movieId,int actorId,[FromQuery]string role)=>Link(movieId,new(){ActorId=actorId,Role=role});
}
