using System.ComponentModel.DataAnnotations;

namespace Server.Application.Cart;

public class AddCustomerInformationRequest
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    public string Address1 { get; set; } = string.Empty;
    public string Address2 { get; set; } = string.Empty;
    [Required]
    public string City { get; set; } = string.Empty;
    [Required]
    public string PostCode { get; set; } = string.Empty;
}

public interface IAddCustomerInformationRequestHandler
{
    Task<bool> Handle(AddCustomerInformationRequest request);
}

public class AddCustomerInformationRequestHandler : IAddCustomerInformationRequestHandler
{
    private readonly ISessionManager _sessionManager;

    public AddCustomerInformationRequestHandler(ISessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public Task<bool> Handle(AddCustomerInformationRequest request)
    {
        var customerInformation = new CustomerInformation
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address1 = request.Address1,
            Address2 = request.Address2,
            City = request.City,
            PostCode = request.PostCode
        };

        _sessionManager.AddCustomerInformation(customerInformation);

        return Task.FromResult(true);
    }
}