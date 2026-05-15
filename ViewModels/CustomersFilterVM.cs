namespace Shoppable.ViewModels;
public class CustomersFilterVM
{

    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? City { get; set; }
    public string? Phone { get; set; }
    public List<Customer>? customers { get; set; }
}
