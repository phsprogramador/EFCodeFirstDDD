using System.Net;
using Applications.DTO.Request;
using Applications.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;

namespace EFCodeFirstDDD.Tests;

public class ProfileControllerTests : ProfileBase
{
    private readonly ProfileController _controller;

    public ProfileControllerTests()
    {
        _controller = new ProfileController(_application.Object);
    }

    [Fact]
    public void TypeUserControllerCreateOK()
    {
        LoadEntityMock();

        List<RequestProfile> requests = new() { request };
        List<ResponseProfile> responses = new() { response };

        _application
            .Setup(a => a.Create(requests))
            .Returns(responses);

        var resp = (ObjectResult)_controller.Create(requests);

        Assert.NotNull(resp);
        Assert.Equal((int)HttpStatusCode.Created, resp.StatusCode);
    }

    [Fact(Skip = "Fix Pending")]
    public void TypeUserControllerQueryByIdOK()
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
    public void TypeUserControllerQueryByNameOK()
    {
        LoadEntityMock();

        _application
            .Setup(a => a.Query(request))
            .Returns(response);

        var resp = (ObjectResult)_controller.Query(request.name);

        Assert.NotNull(resp);
        Assert.Equal((int)HttpStatusCode.OK, resp.StatusCode);
    }

    [Fact]
    public void TypeUserControllerUpdateOK()
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
    public void TypeUserControllerDeleteOK()
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
    [InlineData(1, 1)]
    public void TypeUserControllerListOK(int skip, int take)
    {
        LoadEntityMock();

        List<ResponseProfile> responses = new() { response };

        _application
            .Setup(a => a.List(request))
            .Returns(responses);

        var resp = (OkObjectResult)_controller.List(request);

        Assert.NotNull(resp);
        Assert.Equal((int)HttpStatusCode.OK, resp.StatusCode);
    }
}
