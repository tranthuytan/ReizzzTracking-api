using Microsoft.AspNetCore.Http;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BL.UnitTest.Services.BaseServiceTest
{
    public class HttpContextAccessorServiceTest
    {
        protected IHttpContextAccessor _httpContextAccessorMock { get; set; }
        protected ClaimsPrincipal claimsPrincipal;
        public HttpContextAccessorServiceTest()
        {
            _httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();
            //Mock user's claims
            //TODO: add more claims to claimsPrincipal, currently only Id is added
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,"1")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            claimsPrincipal = new ClaimsPrincipal(identity);
        }
    }
}
