namespace Domain.Entity;

public class User
{
    public string Name { get; set; }
    public string Email { get; private set; }
    public string HashPassword { get; private set; }

    public User(string name, string email, string password)
    {
        Name = name;
        Email = ValidateEmail(email);
        HashPassword = password;
    }

    private string ValidateEmail(string email)
    {
        //TODO: validar extensão e caracteres do email e colocar em outra classe estática
        throw new NotImplementedException();
    }
    
    public bool ValidateEntity()
    {
        // TODO: validar se os campos foram preenchidos corretamente 
        throw new NotImplementedException();
    }

    
    
}