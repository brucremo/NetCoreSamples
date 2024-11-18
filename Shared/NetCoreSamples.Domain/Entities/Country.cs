namespace NetCoreSamples.Domain.Entities;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();
}
