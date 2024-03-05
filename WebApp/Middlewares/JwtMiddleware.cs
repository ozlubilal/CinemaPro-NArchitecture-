using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApp.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Cookies["accessToken"];

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                // JWT içindeki talepleri al
                var claims = jsonToken.Claims;

                // Rollerin bulunması   
                var roles = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

                // Kullanıcı için bir ClaimsIdentity oluştur
                var identity = new ClaimsIdentity(claims, "jwt");

                // Kullanıcıya rolleri ekle
                identity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                // ClaimsIdentity'i kullanarak ClaimsPrincipal oluştur
                var principal = new ClaimsPrincipal(identity);

                // HttpContext.User özelliğini ayarla
                context.User = principal;
            }
            // Kullanıcı yetkilendirilmemişse, login sayfasına yönlendirme yap
         

            await _next(context);
        }

    }
}
