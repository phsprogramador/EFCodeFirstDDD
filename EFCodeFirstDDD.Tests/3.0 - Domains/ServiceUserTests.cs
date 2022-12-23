using Moq;
using Domains;
using Services;
using System.Net;
using Applications.Helpers;
using System.Linq.Expressions;
using Applications.DTO.Response;

namespace EFCodeFirstDDD.Tests;

public class ServiceUserTests  : UserBase
{
    private readonly ServiceUser _service;
    public ServiceUserTests()
	{
        _service = new ServiceUser(_repository.Object);
    }

    [Fact]
    public void ServiceUserCreateOK()
    {
        LoadEntityMock();

        _repository
            .Setup(s => s.Create(entity))
            .Returns(entity.id);

        _repository
            .Setup(s => s.Query(entity))
            .Returns(entity);

        Guid id = _service.Create(entity);

        Assert.NotNull(id);
        Assert.Equal(entity.id, id);
    }

    [Fact]
    public void ServiceUserQueryOK()
    {
        LoadEntityMock();

        _repository
            .Setup(s => s.Query(entity))
            .Returns(entity);

        User resp = _service.Query(entity);

        Assert.NotNull(response);
        Assert.Equal(request.id, resp.id);
        Assert.Equal(request.name, resp.name);
        Assert.Equal(request.login, resp.login);
        Assert.Equal(request.password, resp.password);
        Assert.Equal(request.email, resp.email);
        Assert.Equal(request.dtInsert, resp.dtInsert);
        Assert.Equal(request.dtUpdate, resp.dtUpdate);
        Assert.Equal(request.active, resp.active);   
    }

    [Fact]
    public void ServiceUserUpdateOK()
    {
        LoadEntityMock();

        _repository
            .Setup(s => s.Update(entity))
            .Returns(true);

        bool resp = _service.Update(entity);

        Assert.True(resp);        
    }

    [Fact]
    public void ServiceUserRemoveOK()
    {
        LoadEntityMock();

        _repository
            .Setup(s => s.Remove(entity))
            .Returns(true);

        bool resp = _service.Remove(entity);

        Assert.True(resp);
    }

    [Fact]    
    public void ServiceUserListOK()
    {
        LoadEntityMock();
        _repository
            .Setup(s => s.List(entity))
            .Returns(new List<ResponseUser> { response });

        List<ResponseUser> responses = _service.List(entity);

        Assert.NotNull(responses[0]);
        Assert.Equal(responses.Count, 1);
    }
}
