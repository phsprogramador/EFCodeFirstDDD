using Adapteres.Interfaces;
using Applications.DTO.Request;
using Applications.DTO.Response;
using Applications.Helpers;
using Applications.Interfaces;
using Applications.Services;
using Autofac.Core;
using Cores.Service;
using Domains;
using Moq;
using System;
using System.Linq.Expressions;
using System.Net;
using Xunit;

namespace EFCodeFirstDDD.Tests;

public class ApplicationServiceUserTests : UserBase
{   
    private readonly ApplicationServiceUser _application;

    public ApplicationServiceUserTests()
    { 
        _application = new ApplicationServiceUser(_service.Object, _validator.Object, _mapper.Object);
    }

    [Fact(Skip = "Fix Pending")]
    public void ApplicationServiceUserCreateOK()
    {
        LoadEntityMock();
        LoadMapperSetups(string.Format(Messages.messageSucessCreate, entity.id));

        List<RequestUser> requests = new List<RequestUser> { request };

        _service
          .Setup(s => s.Create(entity))
          .Returns(entity.id);

        _service
            .Setup(s => s.Query(entity))
            .Returns(entity);

        List<ResponseUser> responses = _application.Create(requests);

        Assert.NotNull(responses[0]);
        Assert.Equal(responses.Count, 1);
    }

    [Fact] 
    public void ApplicationServiceUserQueryOK()
    {
        LoadEntityMock();
        LoadMapperSetups(string.Format(Messages.messageSucessQuery, entity.id));

        _service
            .Setup(s => s.Query(entity))
            .Returns(entity);

        ResponseUser resp = _application.Query(request);

        Assert.NotNull(response);
        Assert.Equal(request.id, resp.id);
        Assert.Equal(request.name, resp.name);
        Assert.Equal(request.login, resp.login);
        Assert.Equal(request.password, resp.password);
        Assert.Equal(request.email, resp.email);
        Assert.Equal(request.dtInsert, resp.dtInsert);
        Assert.Equal(request.dtUpdate, resp.dtUpdate);
        Assert.Equal(request.active, resp.active);
        Assert.Equal((int)HttpStatusCode.OK, resp.statusCode);
        Assert.Equal("Ok", resp.message);
    }

    [Fact(Skip = "Fix Pending")]
    public void ApplicationServiceUserUpdateOK()
    {
        LoadEntityMock();
        LoadMapperSetups(string.Format(Messages.messageSucessUpdate, entity.id));

        _service
            .Setup(s => s.Query(entity))
            .Returns(entity);

        _service
            .Setup(s => s.Update(entity))
            .Returns(true);

        ResponseBase responseBase = _application.Update(entity.id, request);

        Assert.NotNull(responseBase);
        Assert.Equal((int)HttpStatusCode.OK, responseBase.statusCode);
        Assert.Equal(string.Format(Messages.messageSucessUpdate, entity.id), responseBase.message);
    }

    [Fact(Skip = "Fix Pending")]
    public void ApplicationServiceUserRemoveOK()
    {
        LoadEntityMock();
        LoadMapperSetups(string.Format(Messages.messageSucessRemove, entity.id));

        _service
          .Setup(s => s.Query(new User() { id = request.id }))
          .Returns(entity);

        _service
          .Setup(s => s.Remove(entity))
          .Returns(true);

        ResponseBase responseBase = _application.Remove(entity.id);

        Assert.NotNull(responseBase);
        Assert.Equal((int)HttpStatusCode.OK, responseBase.statusCode);
        Assert.Equal(string.Format(Messages.messageSucessRemove, entity.id), responseBase.message);
    }

    [Fact]    
    public void ApplicationServiceUserListOK()
    {   
        LoadEntityMock();

        _service
          .Setup(s => s.List(entity))
          .Returns(new List<ResponseUser> { response });

        List<ResponseUser> responses = _application.List(request);

        Assert.NotNull(responses[0]);
        Assert.Equal(responses.Count, 1);
    }        
}
