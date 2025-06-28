namespace Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; private set; }    
    public string Email { get; private set; }    
    public string PasswordHash { get; private set; }    
    public string Salt { get; private set; }    
}