namespace Dramatist.Models;


public class User
{
    public int Id { get; set; }
    public string Nick { get; set; }
    public string Name { get; set; }

    public User()
    {
        throw new NotImplementedException();
    }

    public User(string nick)
    {
        Nick = nick;
    }

    public Uri GetPublicProfile() => new Uri(Dnevnik.KnetworkBaseAddress + $"/{Nick}");
    // ToDo: FIX ME!
    public Uri GetAvatarUri() => new Uri(Dnevnik.KnetworkBaseAddress + $"/{Id}");
    // ToDo: FIX ME!
    public Uri GetAvatarUri(AvatarType t)
    {
        switch (t)
        {
            // ToDo: FIX ME!
            case AvatarType.Big:
                return new Uri(Dnevnik.KnetworkBaseAddress + $"/{Id}");
            case AvatarType.Medium:
                return new Uri(Dnevnik.KnetworkBaseAddress + $"/{Id}");
            case AvatarType.Small:
                return new Uri(Dnevnik.KnetworkBaseAddress + $"/{Id}");
            case AvatarType.Tiny:
                return new Uri(Dnevnik.KnetworkBaseAddress + $"/{Id}");
            // ToDo: drop default?
            default:
                throw new Exception("Invalid avatar type!");
        }
    }
}
