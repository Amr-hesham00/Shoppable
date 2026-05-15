namespace Shoppable.Services.IServices;

public interface ICustomerService
{

    //public Task SaveDeleteAsync(int id);
    public Task<CustomerVM> DetailsAsync(int id);

}
