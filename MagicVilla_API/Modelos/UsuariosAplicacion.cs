using Microsoft.AspNetCore.Identity;

namespace MagicVilla_API.Modelos
{
    public class UsuariosAplicacion : IdentityUser
    {
        public string Nombres { get; set; }
    }
}
