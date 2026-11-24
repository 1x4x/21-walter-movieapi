using System.ComponentModel.DataAnnotations;
namespace MovieApi.DTOs;
public class MovieCreateDto { [Required] public string Title {get;set;}=""; [Range(1888,2100)] public int Year {get;set;} [Required] public string Genre {get;set;}=""; [Range(1,1440)] public int Duration {get;set;} public DetailsDto? Details {get;set;} public int? DirectorId {get;set;} public int? CountryId {get;set;} public List<int> GenreIds {get;set;}=[]; }
public class MovieUpdateDto:MovieCreateDto {}
public record MovieDto(int Id,string Title,int Year,string Genre,int Duration);
public class DetailsDto { public string Synopsis {get;set;}=""; public string Language {get;set;}=""; [Range(0,double.MaxValue)] public decimal Budget {get;set;} }
public record ActorDto(int Id,string Name,int BirthYear,string? Role=null);
public record ReviewDto(int Id,string ReviewerName,string Comment,int Rating);
public record MovieDetailDto(int Id,string Title,int Year,string Genre,int Duration,DetailsDto? Details,List<ReviewDto> Reviews,List<ActorDto> Actors,string? Director,string? Country,List<string> Genres);
public class ActorCreateDto { [Required] public string Name {get;set;}=""; [Range(1800,2100)] public int BirthYear {get;set;} }
public class ReviewCreateDto { [Required] public string ReviewerName {get;set;}=""; [Required] public string Comment {get;set;}=""; [Range(1,5)] public int Rating {get;set;} }
public class MovieActorCreateDto { [Range(1,int.MaxValue)] public int ActorId {get;set;} [Required] public string Role {get;set;}=""; }
