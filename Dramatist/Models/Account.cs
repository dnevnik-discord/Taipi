namespace Dramatist.Models;

class Account //: User
{
    public string   Email { get; set; }
    public string   Password { get; set; }
    // public User     User { get; set; }

    public Account(string email, string password)
    {
        Email = email;
        Password = password;
    }
}