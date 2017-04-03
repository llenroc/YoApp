﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using YoApp.Identity.Controllers;
using YoApp.Data;
using YoApp.Data.Models;
using YoApp.DataObjects.Account;
using YoApp.Identity;
using Microsoft.AspNetCore.Identity;

namespace YoApp.Tests.Services.Identity.Controller
{
    public class AccountControllerTests
    {
        private ILogger<AccountController> _logger;

        public AccountControllerTests()
        {
            _logger = new Mock<ILogger<AccountController>>().Object;
        }

        [Theory]
        [InlineData("4915701234", "Bob", "Martin")]
        public async void UpdateName_OnNicknameUpdate_ReturnsOk(string username, string oldNickname, string newNickname)
        {
            //Arrange
            var fakeUser = new ApplicationUser
            {
                UserName = username,
                Nickname = oldNickname
            };

            var unitOfWorkMock = new Mock<UserManager<ApplicationUser>>();
            unitOfWorkMock
                .Setup(r => r.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(fakeUser);

            var httpContextMock = new Mock<DefaultHttpContext>(null);
            httpContextMock
                .SetupGet(am => am.User.Identity.Name)
                .Returns(username);

            var controller = new AccountController(_logger, unitOfWorkMock.Object);
            controller.ControllerContext.HttpContext = httpContextMock.Object;

            //Act
            var dto = new UpdatedAccountDto { Nickname = newNickname };
            var response = await controller.UpdateAccount(dto);

            //Assert
            Assert.IsType<OkResult>(response);
        }

        //[Theory]
        //[InlineData("4915701234", "Bob", "Martin")]
        //public async void UpdateStatus_OnStatusUpdate_ReturnsNewStatus(string username, string oldStatus, string newStatus)
        //{
        //    //Arrange
        //    var fakeUser = new ApplicationUser
        //    {
        //        UserName = username,
        //        Status = oldStatus
        //    };

        //    var unitOfWorkMock = new Mock<IUnitOfWork>();
        //    unitOfWorkMock
        //        .Setup(r => r.UserRepository
        //        .GetByUsernameAsync(It.IsAny<string>()))
        //        .ReturnsAsync(fakeUser);

        //    var httpContextMock = new Mock<DefaultHttpContext>(null);
        //    httpContextMock
        //        .SetupGet(am => am.User.Identity.Name)
        //        .Returns(username);

        //    var controller = new AccountController(_logger, unitOfWorkMock.Object);
        //    controller.ControllerContext.HttpContext = httpContextMock.Object;

        //    //Act
        //    var dto = new UpdatedAccountDto { StatusMessage = newStatus };
        //    var response = await controller.UpdateAccount(dto);
        //    var resultValue = ((OkObjectResult)response).Value as string;

        //    //Assert
        //    Assert.Equal(newStatus, resultValue);
        //}

        //[Fact]
        //public async void GetNickname_NullUser_ReturnsNotfound()
        //{
        //    //Arrange
        //    var unitOfWorkMock = new Mock<IUnitOfWork>();
        //    unitOfWorkMock
        //        .Setup(r => r.UserRepository
        //        .GetByUsernameAsync(It.IsAny<string>()))
        //        .ReturnsAsync(null);

        //    var httpContextMock = new Mock<DefaultHttpContext>(null);
        //    httpContextMock
        //        .SetupGet(am => am.User.Identity.Name)
        //        .Returns("Anybody");

        //    var controller = new AccountController(_logger, unitOfWorkMock.Object);
        //    controller.ControllerContext.HttpContext = httpContextMock.Object;

        //    //Act
        //    var response = await controller.GetAccount();

        //    //Assert
        //    Assert.IsType<NotFoundResult>(response);
        //}

        //[Fact]
        //public async void GetStatus_NullUser_ReturnsNotfound()
        //{
        //    //Arrange
        //    var unitOfWorkMock = new Mock<IUnitOfWork>();
        //    unitOfWorkMock
        //        .Setup(r => r.UserRepository
        //        .GetByUsernameAsync(It.IsAny<string>()))
        //        .ReturnsAsync(null);

        //    var httpContextMock = new Mock<DefaultHttpContext>(null);
        //    httpContextMock
        //        .SetupGet(am => am.User.Identity.Name)
        //        .Returns("Anybody");

        //    var controller = new AccountController(_logger, unitOfWorkMock.Object);
        //    controller.ControllerContext.HttpContext = httpContextMock.Object;

        //    //Act
        //    var response = await controller.GetAccount();

        //    //Assert
        //    Assert.IsType<NotFoundResult>(response);
        //}
    }
}