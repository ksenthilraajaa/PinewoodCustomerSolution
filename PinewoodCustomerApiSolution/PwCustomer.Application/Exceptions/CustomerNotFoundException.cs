namespace PwCustomer.Application;

public class NotFoundException : Exception
{
    public NotFoundException(string message)
    : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException)
    : base(message, innerException)
    {
    }
}

public class CustomerNotFoundException : NotFoundException
{
    public CustomerNotFoundException(string message)
    : base(message)
    {
    }

    public CustomerNotFoundException(string message, Exception innerException)
    : base(message, innerException)
    {
    }
}
