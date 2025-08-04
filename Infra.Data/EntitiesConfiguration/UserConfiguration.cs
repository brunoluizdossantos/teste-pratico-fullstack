using Domain.Entities;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(t => t.Id);

		builder.Property(p => p.Name).HasMaxLength(Constants.NameMaxLength).IsRequired();
		builder.Property(p => p.Email).HasMaxLength(Constants.EmailMaxLength).IsRequired();
		builder.Property(p => p.HashedPassword).HasMaxLength(Constants.HashMaxLength).IsRequired();
		builder.Property(p => p.Salt).HasMaxLength(Constants.SaltMaxLength).IsRequired();
	}
}