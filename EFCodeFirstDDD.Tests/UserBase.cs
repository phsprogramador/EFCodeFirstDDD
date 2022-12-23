using Applications.DTO.Request;
using Applications.DTO.Response;
using Azure.Core;
using Azure;
using Domains;
using System.Net;
using Adapteres.Interfaces;
using Applications.Interfaces;
using Applications.Services;
using Cores.Service;
using Moq;
using Applications.Helpers;
using Cores.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace EFCodeFirstDDD.Tests;

public abstract class UserBase
{
    internal readonly Mock<IApplicationServiceUser> _application;
    internal readonly Mock<IServiceUser> _service;
    internal readonly Mock<IApplicationValidatorUser> _validator;
    internal readonly Mock<IMapperUser> _mapper;
    internal readonly Mock<IRepositoryUser> _repository;

    internal User? entity;    
    internal RequestUser request;
    internal ResponseUser response;

    internal ResponseBase baseResponse;

    public UserBase()
    {
        _application = new Mock<IApplicationServiceUser>();
        _service = new Mock<IServiceUser>();
        _validator = new Mock<IApplicationValidatorUser>();
        _mapper = new Mock<IMapperUser>();
        _repository = new Mock<IRepositoryUser>();
    }

    internal virtual void LoadEntityMock()
    {
        Guid id = new Guid("69EDC590-63A1-4BF4-99D0-C6A4D10577E4");
        Guid idTypeUser = new Guid("77D1D994-C428-421A-8B8C-0EE21C9F6280");
        string name = "Name User";
        string login = "userlogin";
        string password = "userpassword";
        string email = "user@email.com";
        DateTime dtInsert = DateTime.Parse("2022-01-23 00:00:00.000Z");
        DateTime dtUpdate = dtInsert;
        bool active = true;
        
        baseResponse = new  ResponseBase
        {
            message = "Ok",
            statusCode = (int)HttpStatusCode.OK
        };

        entity = new User
        {
            id = id,
            idProfile = idTypeUser,
            name = name,
            login = login,
            password = password,
            email = email,
            dtInsert = dtInsert,
            dtUpdate = dtUpdate,
            active = active
        };
        request = new RequestUser
        {
            id = id,
            idTypeUser = idTypeUser,
            name = name,
            login = login,
            password = password,
            email = email,
            dtInsert = dtInsert,
            dtUpdate = dtUpdate,
            active = active
        };
        response = new ResponseUser
        {
            id = id,
            idTypeUser = idTypeUser,
            name = name,
            login = login,
            password = password,
            email = email,
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
