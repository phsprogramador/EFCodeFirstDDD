using System.Net;
using Applications.DTO.Request;
using Applications.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;

namespace EFCodeFirstDDD.Tests;

public class UserControllerTests : UserBase
{
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _controller = new UserController(_application.Object);
    }

    [Fact]
    public void UserControllerCreateOK()
    {
        LoadEntityMock();

        List<RequestUser> requests = new() { request };
        List<ResponseUser> responses = new() { response };

        _application
            .Setup(a => a.Create(requests))
            .Returns(responses);

        var resp = (ObjectResult)_controller.Create(requests);

        Assert.NotNull(resp);
        Assert.Equal((int)HttpStatusCode.Created, resp.StatusCode);
    }

    [Fact(Skip = "Fix Pending")]
    public void UserControllerQueryByIdOK()
    {
        LoadEntityMock();

        _application
            .Setup(a => a.Query(request))
            .Returns(response);

        var resp = (ObjectResult)_controller.Query(request.id);

        Assert.NotNull(resp);
        Assert.Equal((int)HttpStatusCode.OK, resp.StatusCode);
    }

    [Fact(Skip = "Fix Pending")]
    public void UserControllerQueryByLoginPwdOK()
    {
        LoadEntityMock();

        _application
            .Setup(a => a.Query(request))
            .Returns(response);

        var resp = (ObjectResult)_controller.Query(request.login, request.password);

        Assert.NotNull(resp);
        Assert.Equal((int)HttpStatusCode.OK, resp.StatusCode);
    }

    [Fact]
    public void UserControllerUpdateOK()
    {
        LoadEntityMock();

        _application
            .Setup(a => a.Update(entity.id, request))
            .Returns(new ResponseBase
            {
                statusCode = (int)HttpStatusCode.OK,
                message = "Ok"
            });

        var resp = (OkResult)_controller.Update(entity.id, request);

        Assert.NotNull(resp);
        Assert.Equal((int)HttpStatusCode.OK, resp.StatusCode);
    }

    [Fact]
    public void UserControllerDeleteOK()
    {
        LoadEntityMock();

        _application
            .Setup(a => a.Remove(entity.id))
            .Returns(baseResponse);

        var resp = (OkResult)_controller.Delete(entity.id);

        Assert.NotNull(resp);
        Assert.Equal((int)HttpStatusCode.OK, resp.StatusCode);
    }

    [Theory]
    [InlineData(1,1)]
    public void UserControllerListOK(int skip, int take)
    {
        LoadEntityMock();
        
        List<ResponseUser> responses = new() { response };

        _application
            .Setup(a => a.List(request))
            .Returns(responses);

        var resp = (OkObjectResult)_controller.List(request);

        Assert.NotNull(resp);
        Assert.Equal((int)HttpStatusCode.OK, resp.StatusCode);
    }
}
