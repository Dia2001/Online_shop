
using OnlineShop.Business.Rule;
using OnlineShop.Services.Base;
using OnlineShop.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using OnlineShop.Application.Helpers;
using OnlineShop.Core.ExceptionHandle;
using OnlineShop.Core.Schemas.Base;
using OnlineShop.Core.Schemas;

namespace OnlineShop.Application.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork uow)
        {
            try
            {
                var cookieToken = context.Request.Cookies["access_token"];
                var tokenHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                if (cookieToken != null || (tokenHeader != null && tokenHeader != "null"))
                {
                    string token = cookieToken == null ? tokenHeader.Replace("Bearer ", "") : cookieToken;
                    AttachUserToContext(context, uow, token);
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                var middlewareHelper = new MiddlewareHelper();
                await middlewareHelper.HandleExceptionAsync(context, ex);
            }
        }

        private void AttachUserToContext(HttpContext context, IUnitOfWork uow, string token)
        {
            if (context.Request.Path.Value != null && (context.Request.Path.Value.ToLower() == "/api/auth/refresh" || context.Request.Path.Value.ToLower() == "/api/users/get-current-user" || context.Request.Path.Value.ToLower() == "/api/auth/login"))
            {
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtUtil.SECRET_KEY);
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userCredentialString = jwtToken.Claims.First(x => x.Type == "id").Value;
            int id = Int32.Parse(userCredentialString);
            UserSchema user = uow.Users.FindOne(id);
            CustomerSchema customer = uow.Customers.FindOne(id);
            if (user != null)
            {
                context.Items["User"] = user;
                if (user.Id != UserRule.ADMIN_ID)
                {
                    CheckPermission(context, user.Id, uow);
                }
            }

            if (customer != null)
            {
                context.Items["Customer"] = customer;
                if (customer.Id != UserRule.CUSTOMER_ID)
                {
                    CheckCustomerPermission(context, customer.Id, uow);
                }
            }
        }

        private void CheckCustomerPermission(HttpContext context, int customerId, IUnitOfWork uow)
        {
            var header = context.Request.Path.Value;
            bool isRequestApi = header.Contains("/api/");
            if (isRequestApi)
            {
                var apiRequest = header.Replace("/api/", "").Split("/");
                string endPoint = "";
                if (apiRequest.Length > 1)
                {
                    var isNumeric = apiRequest.Length == 2 ? int.TryParse(apiRequest[1], out int n) : false;
                    endPoint = isNumeric ? apiRequest[0] : apiRequest[0] + "/" + apiRequest[1];
                }
                else
                {
                    endPoint = apiRequest[0];
                }

                bool isAccess = uow.Customers.CheckPermissionAction(customerId, endPoint);

                if (!isAccess)
                {
                    throw new ForbiddenException();
                }
            }
        }

        private void CheckPermission(HttpContext context, int userId, IUnitOfWork uow)
        {
            var header = context.Request.Path.Value;
            bool isRequestApi = header.Contains("/api/");
            if (isRequestApi)
            {
                var apiRequest = header.Replace("/api/", "").Split("/");
                string endPoint = "";
                if (apiRequest.Length > 1)
                {
                    var isNumeric = apiRequest.Length == 2 ? int.TryParse(apiRequest[1], out int n) : false;
                    endPoint = isNumeric ? apiRequest[0] : apiRequest[0] + "/" + apiRequest[1];
                }
                else
                {
                    endPoint = apiRequest[0];
                }

                bool isAccess = uow.Users.CheckPermissionAction(userId, endPoint);

                if (!isAccess)
                {
                    throw new ForbiddenException();
                }
            }
        }
    }
}
