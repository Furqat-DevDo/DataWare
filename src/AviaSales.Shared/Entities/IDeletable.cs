namespace AviaSales.Shared.Entities;

public interface IDeletable
{
    public bool IsDeleted { get; set; }
}