using Applications.DTO.Response;
using Domains;
using Moq;
using Services;
using System.Linq.Expressions;

namespace EFCodeFirstDDD.Tests;

public class ServiceProfileTests : ProfileBase
{
    private readonly ServiceProfile _service;
    public ServiceProfileTests()
    {
        _service = new ServiceProfile(_repository.Object);
    }

    [Fact]
    public void ServiceTypeUserCreateOK()
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
    public void ServiceTypeUserQueryOK()
    {
        LoadEntityMock();

        _repository
            .Setup(s => s.Query(entity))
            .Returns(entity);

        Profile resp = _service.Query(entity);

        Assert.NotNull(response);
        Assert.Equal(request.id, resp.id);
        Assert.Equal(request.name, resp.name);
        Assert.Equal(request.description, resp.description);        
        Assert.Equal(request.dtInsert, resp.dtInsert);
        Assert.Equal(request.dtUpdate, resp.dtUpdate);
        Assert.Equal(request.active, resp.active);
    }

    [Fact]
    public void ServiceTypeUserUpdateOK()
    {
        LoadEntityMock();

        _repository
            .Setup(s => s.Update(entity))
            .Returns(true);

        bool resp = _service.Update(entity);

        Assert.True(resp);
    }

    [Fact]
    public void ServiceTypeUserRemoveOK()
    {
        LoadEntityMock();

        _repository
            .Setup(s => s.Remove(entity))
            .Returns(true);

        bool resp = _service.Remove(entity);

        Assert.True(resp);
    }

    [Theory]
    [InlineData(1, 1)]
    public void ServiceTypeUserListOK(int skip, int take)
    {
        LoadEntityMock();
        _repository
            .Setup(s => s.List(entity))
            .Returns(new List<ResponseProfile> { response });

        List<ResponseProfile> responses = _service.List(entity);

        Assert.NotNull(responses[0]);
        Assert.Equal(responses.Count, 1);
    }
}
