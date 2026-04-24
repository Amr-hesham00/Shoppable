namespace Shoppable;

public class Result
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    //public T? Data { get; set; }

    public static Result Ok(string msg = "")
    {
        return new Result() { Success = true, Message = msg };
    }
    public static Result Fail(string msg)
    {
        return new Result() { Success = false, Message = msg };
    }

}
