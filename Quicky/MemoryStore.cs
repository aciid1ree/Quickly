using Quicky.Models;

namespace Quickly;

public class MemoryStore
{
    public static List<BumpRequest> Bumps = new List<BumpRequest>();
    public static List<User> Users = new List<User>();
    public static List<OrderItem> Receipt = new List<OrderItem>();
}