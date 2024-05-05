namespace Server.Application.Cart;

public class GetCustomerInformationRequest
{
}

public interface IGetCustomerInformationRequestHandler
{
    Task<GetCustomerInformationRequestHandler.Response> Handle(GetCustomerInformationRequest request);
}

public class GetCustomerInformationRequestHandler : IGetCustomerInformationRequestHandler
{
    private readonly ISessionManager _sessionManager;

    public GetCustomerInformationRequestHandler(ISessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public Task<Response> Handle(GetCustomerInformationRequest request)
    {
        var customerInformation = _sessionManager.GetCustomerInformation(); 

        if (customerInformation == null)
            return new Task<Response>(() => new Response());
        

        return Task.FromResult(new Response
        {
            FirstName = customerInformation.FirstName,
            LastName = customerInformation.LastName,
            Email = customerInformation.Email,
            PhoneNumber = customerInformation.PhoneNumber,
            Address1 = customerInformation.Address1,
            Address2 = customerInformation.Address2,
            City = customerInformation.City,
            PostCode = customerInformation.PostCode
        });
    }

    public class Response
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
    }
}