using System.Diagnostics.CodeAnalysis;

namespace ApbdTest1.Models.DTOs;

public class BooksDto
{
    
    public int Pk { get; set; } 
    public string Title { get; set; }= string.Empty;
    public List<AuthorsDto> Authors { get; set;}
    public List<PublishingHousesDto> PublishingHouses { get; set; }
    public List<GenresDto> Genres { get; set; }
}

public class PublishingHousesDto
{
    public int Pk { get; set; }
    public string Name { get; set; }= string.Empty;
    
    [MaybeNull]
    public string OwnerFirstName { get; set; }= string.Empty;
    public string OwnerLastName { get; set; }= string.Empty;
}

public class AuthorsDto
{
    public int Pk { get; set; }
    public string FirstName { get; set; }= string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class GenresDto
{
    public int Pk { get; set; }
    public string Name { get; set; }= string.Empty;
}