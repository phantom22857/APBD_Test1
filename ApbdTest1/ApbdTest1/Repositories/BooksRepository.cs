using ApbdTest1.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace ApbdTest1.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly IConfiguration _configuration;
    
    public BooksRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<bool> DoesBookExist(int id)
    {
        var query = "SELECT 1 FROM books WHERE PK = @ID";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public async Task<bool> DoesAuthorExist(int id)
    {
        var query = "SELECT 1 FROM authors WHERE PK = @ID";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public async Task<bool> DoesPublishingHouseExist(int id)
    {
        var query = "SELECT 1 FROM publishing_houses WHERE PK = @ID";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public async Task<bool> DoesGenreExist(int id)
    {
        var query = "SELECT 1 FROM genres WHERE PK = @ID";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public async Task<BooksDto> GetBook(int id)
{
    var query = @"
        SELECT 
            b.PK AS BookId,
            b.title AS BookTitle,
            a.PK AS AuthorId,
            a.first_name AS FirstName,
            a.last_name AS LastName,
            g.PK AS GenreId,
            g.name AS GenreName,
            ph.PK AS PublishingHouseId,
            ph.name AS PublishingHouseName,
            ph.owner_first_name AS PublishingHouseOwnerFirstName,
            ph.owner_last_name AS PublishingHouseOwnerLastName
        FROM 
            books b
        LEFT JOIN 
            books_authors ba ON b.PK = ba.FK_book
        LEFT JOIN 
            authors a ON ba.FK_author = a.PK
        LEFT JOIN 
            books_genres bg ON b.PK = bg.FK_book
        LEFT JOIN 
            genres g ON bg.FK_genre = g.PK
        LEFT JOIN 
            books_editions be ON b.PK = be.FK_book
        LEFT JOIN 
            publishing_houses ph ON be.FK_publishing_house = ph.PK
        WHERE 
            b.PK = @ID";
    
    await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
    await using SqlCommand command = new SqlCommand();

    command.Connection = connection;
    command.CommandText = query;
    command.Parameters.AddWithValue("@ID", id);
    
    await connection.OpenAsync();
	
    var reader = await command.ExecuteReaderAsync();

    BooksDto bookDto = null;

    while (await reader.ReadAsync())
    {
        if (bookDto is null)
        {
            bookDto = new BooksDto()
            {
                Pk = reader.GetInt32(reader.GetOrdinal("BookId")),
                Title = reader.GetString(reader.GetOrdinal("BookTitle")),
                Authors = new List<AuthorsDto>(),
                Genres = new List<GenresDto>(),
            };
        }

        bookDto.Authors.Add(new AuthorsDto()
        {
            Pk = reader.GetInt32(reader.GetOrdinal("AuthorId")),
            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
            LastName = reader.GetString(reader.GetOrdinal("LastName"))
        });

        bookDto.Genres.Add(new GenresDto()
        {
            Pk = reader.GetInt32(reader.GetOrdinal("GenreId")),
            Name = reader.GetString(reader.GetOrdinal("GenreName"))
        });

        
    }

    if (bookDto is null) throw new Exception("Book not found");
    
    return bookDto;
}

}