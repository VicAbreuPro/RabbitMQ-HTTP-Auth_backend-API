
namespace API_ES.Model
{
    // Development Resource Class Aproach
    public class ResourceAuth
    {
        public string? UserName { get; set; }

        public string? Vhost { get; set; }

        public Resource Resource { get; set; }

        public string? Name { get; set; }

        public ResourcePermission Permission { get; set; }
    }

    public enum ResourcePermission
    {
        Configure,

        Write,

        Read
    }
}