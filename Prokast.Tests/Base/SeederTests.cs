using Microsoft.EntityFrameworkCore;
using Prokast.Tests.Infrastructure;

namespace Prokast.Tests.Base;

public class SeederTests(TestAppFactory factory) : BaseTest(factory)
{

    [Fact]
    public async Task Seeder_ShouldSeedDeafultData_OnStartup()
    {
        // Arrange

        // Act
        var clientEntities = await DbContext.Clients.ToListAsync();
        var dictionaryParamsEntities = await DbContext.DictionaryParams.ToListAsync();
        var productEntities = await DbContext.Products.ToListAsync();
        var warehouseEntities = await DbContext.Warehouses.ToListAsync();
        var storedProductEntities = await DbContext.StoredProducts.ToListAsync();
        // Assert
        Assert.NotEmpty(clientEntities);
        Assert.NotEmpty(dictionaryParamsEntities);
        Assert.NotEmpty(productEntities);
        Assert.NotEmpty(warehouseEntities);
        Assert.NotEmpty(storedProductEntities);
    }

}
