namespace Domain.Entities;

public abstract class Entity
{
	public Guid Id { get; protected set; }
	protected Entity() => Id = Guid.NewGuid();
	public DateTime CreatedAt { get; protected set; }
	public DateTime DeletedAt { get; protected set; }
	public DateTime UpdatedAt { get; protected set; }
}
