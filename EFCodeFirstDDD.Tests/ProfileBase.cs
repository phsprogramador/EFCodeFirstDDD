using Moq;
using Data;
using Domains;
using System.Net;
using Cores.Service;
using Cores.Repository;
using Adapteres.Interfaces;
using Applications.Helpers;
using Applications.Interfaces;
using Applications.DTO.Request;
using Applications.DTO.Response;

namespace EFCodeFirstDDD.Tests;

public abstract class ProfileBase
{
    internal readonly Mock<IApplicationServiceProfile> _application;
    internal readonly Mock<IServiceProfile> _service;
    internal readonly Mock<IApplicationValidatorProfile> _validator;
    internal readonly Mock<IMapperProfile> _mapper;
    internal readonly Mock<IRepositoryProfile> _repository;
    internal readonly Mock<SqlContext> _context;

    internal Profile entity;
    internal RequestProfile request;
    internal ResponseProfile response;

    internal ResponseBase baseResponse;

    public ProfileBase()
    {
        _application = new Mock<IApplicationServiceProfile>();
        _service = new Mock<IServiceProfile>();
        _validator = new Mock<IApplicationValidatorProfile>();
        _mapper = new Mock<IMapperProfile>();
        _repository = new Mock<IRepositoryProfile>();
        _context = new Mock<SqlContext>();
    }

    internal virtual void LoadEntityMock()
    {
        Guid id = new Guid("5AE9BA42-ACCD-4E91-8060-F3BA46BF8048");
        string name = "TypeUser Name";
        string description = "TypeUser Description";
        DateTime dtInsert = DateTime.Parse("2022-01-23 00:00:00.000Z");
        DateTime dtUpdate = dtInsert;
        bool active = true;

        baseResponse = new ResponseBase
        {
            message = "Ok",
            statusCode = (int)HttpStatusCode.OK
        };
        entity = new Profile
        {
            id = id,
            name = name,
            description = description,
            dtInsert = dtInsert,
            dtUpdate = dtUpdate,
            active = active
        };
        request = new RequestProfile
        {
            id = id,
            name = name,
            description = description,
            dtInsert = dtInsert,
            dtUpdate = dtUpdate,
            active = active
        };
        response = new ResponseProfile
        {
            id = id,
            name = name,
            description = description,
            dtInsert = dtInsert,
            dtUpdate = dtUpdate,
            active = active,
            message = "Ok",
            statusCode = (int)HttpStatusCode.OK
        };
    }

    internal virtual void LoadMapperSetups(string messageOK)
    {
        _mapper
            .Setup(m => m.ToEntity(request))
            .Returns(entity);

        _mapper
            .Setup(m => m.ToEntity(response))
            .Returns(entity);

        _mapper
            .Setup(m => m.ToEntity(entity, request))
            .Returns(entity);

        _mapper
            .Setup(m => m.ToResponse(string.Format(Messages.emptyRequest, entity.id), (int)HttpStatusCode.NoContent))
            .Returns(response);

        _mapper
            .Setup(m => m.ToResponse(messageOK, (int)HttpStatusCode.OK, entity))
            .Returns(response);
    }
}
