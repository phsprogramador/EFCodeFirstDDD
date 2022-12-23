using Moq;
using Domains;
using Adapteres.Interfaces;
using Applications.Services;
using Applications.Interfaces;
using Applications.DTO.Request;
using Applications.DTO.Response;
using Cores.Service;
using System.Linq.Expressions;
using System.Net;
using Applications.Helpers;

namespace EFCodeFirstDDD.Tests;

public class ApplicationServiceProfileTests : ProfileBase
{
    private readonly ApplicationServiceProfile _application;
    public ApplicationServiceProfileTests()
    {
        _application = new ApplicationServiceProfile(_service.Object, _validator.Object, _mapper.Object);
    }

    [Fact(Skip = "Fix Pending")]
    public void ApplicationServiceTypeUserCreateOK()
    {
        LoadEntityMock();
        //LoadMapperSetups(string.Format(Messages.messageSucessCreate, entity.id));

        Profile user = new Profile { id = entity.id };        
        List<RequestProfile> requests = new List<RequestProfile> { request };

        response = new ResponseProfile { id = entity.id };

        _mapper
            .Setup(m => m.ToEntity(request))
            .Returns(entity);

        _mapper
            .Setup(m => m.ToEntity(response))
            .Returns(entity);

        _service
            .Setup(s => s.Create(entity))
            .Returns(entity.id);

        _service
            .Setup(s => s.Query(user))
            .Returns(entity);

        List<ResponseProfile> responses = _application.Create(requests);

        Assert.NotNull(responses[0]);
        Assert.Equal((int)HttpStatusCode.Created, responses.First().statusCode);
    }

    [Fact]
    public void ApplicationServiceTypeUserQueryOK()
    {
        LoadEntityMock();
        LoadMapperSetups(string.Format(Messages.messageSucessQuery, entity.id));

        _service
            .Setup(s => s.Query(entity))
            .Returns(entity);

        ResponseProfile resp = _application.Query(request);

        Assert.NotNull(response);
        Assert.Equal(request.id, resp.id);
        Assert.Equal(request.name, resp.name);
        Assert.Equal(request.description, resp.description);
        Assert.Equal(request.dtInsert, resp.dtInsert);
        Assert.Equal(request.dtUpdate, resp.dtUpdate);
        Assert.Equal(request.active, resp.active);
        Assert.Equal((int)HttpStatusCode.OK, resp.statusCode);
        Assert.Equal("Ok", resp.message);
    }

    [Fact(Skip = "Fix Pending")]
    public void ApplicationServiceTypeUserUpdateOK()
    {
        LoadEntityMock();
        LoadMapperSetups(string.Format(Messages.messageSucessUpdate, entity.id));

        _service
            .Setup(s => s.Query(new Profile { id = request.id}))
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
    public void ApplicationServiceTypeUserRemoveOK()
    {
        LoadEntityMock();
        LoadMapperSetups(string.Format(Messages.messageSucessRemove, entity.id));

        _service
           .Setup(s => s.Query(new Profile() { id = request.id }))
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
    public void ApplicationServiceTypeUserListOK()
    {   
        LoadEntityMock();

        _service
          .Setup(s => s.List(entity))
          .Returns(new List<ResponseProfile> { response });

        List<ResponseProfile> responses = _application.List(request);

        Assert.NotNull(responses[0]);
        Assert.Equal(responses.Count, 1);
    }
}