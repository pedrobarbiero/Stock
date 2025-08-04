namespace Framework.Domain.Generators;

public class Version7GuidGenerator : IIdGenerator
{
    public Guid Create() => Guid.CreateVersion7();
}