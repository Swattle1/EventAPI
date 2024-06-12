using EventAPI.Controllers;
using EventAPI.Data;
using EventAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EventAPI.UnitTests
{
    public class VenueControllerTests
    {
        [Fact]
        public async Task PostVenue_ShouldAddNewVenue()
        {
            // Create a new in-memory database
            var options = new DbContextOptionsBuilder<EventDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Valid test DTO
            var venueDTO = new VenueDTO { Name = "Venue Name", LocationBBox = "Some location" };

            using (var context = new EventDBContext(options))
            {
                var controller = new VenueController(context);
                await controller.PostVenue(venueDTO);
            }

            // Assert
            using (var context = new EventDBContext(options))
            {
                Assert.Equal(1, context.Venues.Count());
                Assert.Equal(venueDTO.Name, context.Venues.Single().Name);
            }
        }

        [Fact]
        public async Task PostVenue_ShouldReturnBadRequest_WhenNameIsNotProvided()
        {
            // Create a new in-memory database
            var options = new DbContextOptionsBuilder<EventDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Invalid test DTO without name
            var venueDTO = new VenueDTO();

            using (var context = new EventDBContext(options))
            {
                var controller = new VenueController(context);
                var result = await controller.PostVenue(venueDTO);

                // Assert
                var actionResult = Assert.IsType<ActionResult<VenueDTO>>(result);
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            }
        }
    }
}
