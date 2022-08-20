namespace Dramatist.Models;


public class User
{
    public int Id { get; set; }
    public string Nick { get; set; }
    public string Name { get; set; }

    // <div class="user">
	//  <div class="userHolder">
	//    <div class="avatar"><a href="/selqnin" title="Към профила на selqnin"><img alt="selqnin" src="/img/man60.png"  /></a>
	// 	    <a href="#" class="dropdown ">&nbsp;</a><ul name="users1134259-9305" class="lgrelations">
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
