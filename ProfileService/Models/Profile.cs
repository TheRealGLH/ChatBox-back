using ChatBoxSharedObjects.Models;

namespace ProfileService.Models;
public class Profile : StoredResource
{
    public string ProfileText { get; set; }
    
    public Profile(string owner) : base(owner)
    {
    }
}