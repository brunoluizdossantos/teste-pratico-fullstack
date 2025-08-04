using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration.Repository;

public static class TestDbContextFactory
{
	public static ApplicationDbContext Create()
	{
		var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase(databaseName: "TestePratico")
			.Options;

		return new ApplicationDbContext(options);
	}

	public static void Destroy(ApplicationDbContext context)
	{
		context.Database.EnsureDeleted();
		context.Dispose();
	}
}