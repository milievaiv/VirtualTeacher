//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using VirtualTeacher.Models;
//using VirtualTeacher.Services;

//namespace VirtualTeacher.Tests.Services
//{
//    [TestClass]
//    public class TokenServiceTests
//    {
//        [TestMethod]
//        public void CreateToken_Returns_Valid_Token()
//        {
//            // Arrange
//            var user = new BaseUser { Email = "test@example.com" };
//            var role = "Admin";
//            var secretKey = "some_secret_key"; // Ensure this matches the key size required by the encryption algorithm

//            var configurationMock = new Mock<IConfiguration>();
//            configurationMock.Setup(x => x.GetSection("JwtConfig:Secret").Value).Returns(secretKey);

//            var tokenService = new TokenService(configurationMock.Object);

//            // Act
//            var result = tokenService.CreateToken(user, role);

//            // Assert
//            Assert.IsNotNull(result);

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var keyBytes = System.Text.Encoding.UTF8.GetBytes(secretKey);
//            var symmetricSecurityKey = new SymmetricSecurityKey(keyBytes);
//            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

//            var tokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = symmetricSecurityKey,
//                ValidateIssuer = false,
//                ValidateAudience = false
//            };

//            SecurityToken validatedToken;
//            var principal = tokenHandler.ValidateToken(result, tokenValidationParameters, out validatedToken);
//            var claimsIdentity = principal.Identity as ClaimsIdentity;

//            Assert.AreEqual(user.Email, claimsIdentity.FindFirst(ClaimTypes.Email)?.Value);
//            Assert.AreEqual(role, claimsIdentity.FindFirst(ClaimTypes.Role)?.Value);
//        }




//        [TestMethod]
//        public void CreateToken_Returns_Valid_Expiry()
//        {
//            // Arrange
//            var user = new BaseUser { Email = "test@example.com" };
//            var role = "Admin";

//            var configurationMock = new Mock<IConfiguration>();
//            configurationMock.Setup(x => x.GetSection("JwtConfig:Secret").Value).Returns("some_secret_key");

//            var tokenService = new TokenService(configurationMock.Object);

//            // Act
//            var result = tokenService.CreateToken(user, role);

//            // Assert
//            Assert.IsNotNull(result);

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var decodedToken = tokenHandler.ReadJwtToken(result);

//            var expiry = decodedToken.ValidTo;
//            var expectedExpiry = DateTime.Now.AddHours(1);

//            Assert.IsTrue(expiry > DateTime.Now);
//            Assert.IsTrue(expiry <= expectedExpiry);
//        }
//    }
//}
