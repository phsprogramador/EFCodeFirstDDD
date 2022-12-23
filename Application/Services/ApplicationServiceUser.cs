using System.Net;
using Cores.Service;
using Applications.Interfaces;
using Adapteres.Interfaces;
using Applications.DTO.Request;
using Applications.DTO.Response;
using Applications.Helpers;
using Domains;

namespace Applications.Services;

public class ApplicationServiceUser : IApplicationServiceUser
{
    private IServiceUser _service;
    private IApplicationValidatorUser _validator;
    private IMapperUser _mapper;

    public ApplicationServiceUser(IServiceUser service, IApplicationValidatorUser validator, IMapperUser mapper)
    {
        _service = service;
        _validator = validator;
        _mapper = mapper;
    }
    public List<ResponseUser> Create(List<RequestUser> requests)
    {
        List<ResponseUser> responses = new();

        if (requests == null)
            responses.Add(_mapper.ToResponse(Messages.emptyRequest, (int)HttpStatusCode.UnprocessableEntity));

        foreach (RequestUser request in requests)
        {
            ResponseUser response = new();
            try
            {
                response.id = _service.Create(_mapper.ToEntity(request));

                User user = _service.Query(_mapper.ToEntity(response));

                if (user == null)
                    responses.Add(_mapper.ToResponse(Messages.emptyRequest, (int)HttpStatusCode.InternalServerError));
                else
                    response = _mapper.ToResponse(string.Format(Messages.messageSucessCreate, request.id), (int)HttpStatusCode.NoContent, user);
            }
            catch
            {
                response.statusCode = (int)HttpStatusCode.InternalServerError;
                response.message += string.Format(Messages.messageError, request.id);
                continue;
            }

            responses.Add(response);
        }

        return responses;
    }
    public ResponseUser Query(RequestUser request)
    {
        ResponseUser response = new();

        if (request == null)
            return _mapper.ToResponse(Messages.emptyRequest, (int)HttpStatusCode.UnprocessableEntity);

        try
        {
            User user = _service.Query(_mapper.ToEntity(request));

            if (user == null)
                response = _mapper.ToResponse(Messages.emptyRequest, (int)HttpStatusCode.NotFound);
            else
                response = _mapper.ToResponse(string.Format(Messages.messageSucessQuery, request.id), (int)HttpStatusCode.OK, user);
        }
        catch
        {
            response.statusCode = (int)HttpStatusCode.InternalServerError;
            response.message += string.Format(Messages.messageError, request.id);
        }

        return response;
    }
    public ResponseBase Remove(Guid id)
    {
        ResponseBase response = new();

        if (id == null)
            return _mapper.ToBase(Messages.emptyRequest, (int)HttpStatusCode.UnprocessableEntity);

        try
        {
            User entity = _service.Query(new User() { id = id });

            if (entity == null)
            {
                response.statusCode = (int)HttpStatusCode.NotFound;
                response.message += string.Format(Messages.messageNotFundQuery, id);
                return response;
            }

            if (_service.Remove(entity))
            {
                response.statusCode = (int)HttpStatusCode.NoContent;
                response.message += string.Format(Messages.messageSucessRemove, id);
            }
        }
        catch
        {
            response.statusCode = (int)HttpStatusCode.InternalServerError;
            response.message += string.Format(Messages.messageError, id);
        }

        return response;
    }
    public ResponseBase Update(Guid id, RequestUser request)
    {
        if (request == null)
            return _mapper.ToBase(Messages.emptyRequest, (int)HttpStatusCode.UnprocessableEntity);

        ResponseBase response = new();

        try
        {   
            User entity = _service.Query(new User() { id = id });

            if(entity == null)
            {
                response.statusCode = (int)HttpStatusCode.NotFound;
                response.message += string.Format(Messages.messageNotFundQuery, request.id);
                return response;
            }

            if (_service.Update(_mapper.ToEntity(entity, request)))
            {
                response.statusCode = (int)HttpStatusCode.NoContent;
                response.message += string.Format(Messages.messageSucessUpdate, request.id);
            }
        }
        catch
        {
            response.statusCode = (int)HttpStatusCode.InternalServerError;
            response.message += string.Format(Messages.messageError, request.id);
        }

        return response;
    }
    public List<ResponseUser> List(RequestUser entity)
    {
        List<ResponseUser> responses = new();

        try
        {
            responses.AddRange(_service.List(_mapper.ToEntity(entity)));
            responses.ForEach(r =>
            {
                r.statusCode = (int)HttpStatusCode.OK;
                r.message = string.Format(Messages.messageError, r.id);
            });
        }
        catch (Exception ex)
        {
            responses = new();
            responses.Add(
                new ResponseUser
                {
                    statusCode = (int)HttpStatusCode.InternalServerError,
                    message = string.Format(Messages.messageError, ex.Message)
                });
        }

        return responses;
    }
}