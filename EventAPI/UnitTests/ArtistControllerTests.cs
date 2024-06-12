using EventAPI.Controllers;
using EventAPI.Data;
using EventAPI.DTO;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EventAPI.UnitTests
{
    public class ArtistControllerTests
    {
        [Fact]
        public async Task PostArtist_ShouldAddNewArtist()
        {
            // Create a new in-memory database
            var options = new DbContextOptionsBuilder<EventDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") 
                .Options;

            // Valid test DTO
            var artistDTO = new ArtistDTO { Name = "Artist Name", Genre = "Artist Genre" };

            using (var context = new EventDBContext(options))
            {
                var controller = new ArtistController(context);
                await controller.PostArtist(artistDTO);
            }

            // Assert
            using (var context = new EventDBContext(options))
            {
                Assert.Equal(1, context.Artists.Count());
                Assert.Equal(artistDTO.Name, context.Artists.Single().Name);
                Assert.Equal(artistDTO.Genre, context.Artists.Single().Genre);
            }
        }
    }
}
