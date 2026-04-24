namespace Shoppable.Models.IProperties;

public interface IHasUserId
{
    public string UserID { get; set; }
}

// this is because this case in GenericRepository:
/*
     public T? GetByUserId(string userid)
    {
        return dbset.FirstOrDefault(x => x.UserID == userid);
    }

 */
