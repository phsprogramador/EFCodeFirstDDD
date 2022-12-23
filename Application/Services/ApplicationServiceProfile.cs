using System.Net;
using Adapteres.Interfaces;
using Applications.DTO.Request;
using Applications.DTO.Response;
using Applications.Helpers;
using Applications.Interfaces;
using Cores.Service;
using Domains;

namespace Applications.Services;

public class ApplicationServiceProfile : IApplicationServiceProfile
{
    private IServiceProfile _service;
    private IApplicationValidatorProfile _validator;
    private IMapperProfile _mapper;

    public ApplicationServiceProfile(IServiceProfile service, IApplicationValidatorProfile validator, IMapperProfile mapper)
    {
        _service = service;
        _validator = validator;
        _mapper = mapper;
    }
    public List<ResponseProfile> Create(List<RequestProfile> requests)
    {
        List<ResponseProfile> responses = new();

        if (requests == null)
            responses.Add(_mapper.ToResponse(Messages.emptyRequest, (int)HttpStatusCode.UnprocessableEntity));

        foreach (RequestProfile request in requests)
        {
            ResponseProfile response = new();

            try
            {
                response.id = _service.Create(_mapper.ToEntity(request));

                if (response.id == default(Guid))
                    return responses;

                Profile typeUser = _service.Query(_mapper.ToEntity(response));

                if (typeUser == null)
                    responses.Add(_mapper.ToResponse(Messages.emptyRequest, (int)HttpStatusCode.InternalServerError));
                else
                    responses.Add(_mapper.ToResponse(string.Format(Messages.messageSucessCreate, response.id), (int)HttpStatusCode.NoContent, typeUser));
            }
            catch
            {
                response.statusCode = (int)HttpStatusCode.InternalServerError;
                response.message += string.Format(Messages.messageError, request.name);
                continue;
            }
        }

        return responses;
    }
    public ResponseProfile Query(RequestProfile request)
    {
        ResponseProfile response = new();

        if (request == null)
            return _mapper.ToResponse(Messages.emptyRequest, (int)HttpStatusCode.UnprocessableEntity);

        try
        {
            Profile typeUser = _service.Query(_mapper.ToEntity(request));

            if (typeUser == null)
                response = _mapper.ToResponse(string.Format(Messages.emptyRequest, request.id), (int)HttpStatusCode.NotFound);
            else
                response = _mapper.ToResponse(string.Format(Messages.messageSucessQuery, request.id), (int)HttpStatusCode.OK, typeUser);
        }
        catch
        {
            response.statusCode = (int)HttpStatusCode.InternalServerError;
            response.message += string.Format(Messages.messageError, request.id);
        }

        return response;
    }
    public ResponseBase Update(Guid id, RequestProfile request)
    {
        if (request == null)
            return _mapper.ToBase(Messages.emptyRequest, (int)HttpStatusCode.UnprocessableEntity);

        ResponseBase response = new();

        try
        {
            Profile entity = _service.Query(new Profile() { id = id });

            if (entity == null)
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
    public ResponseBase Remove(Guid id)
    {
        if (id == null)
            return _mapper.ToBase(Messages.emptyRequest, (int)HttpStatusCode.UnprocessableEntity);

        ResponseBase response = new();

        try
        {
            Profile entity = _service.Query(new Profile() { id = id });

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
    public List<ResponseProfile> List(RequestProfile request)
    {
        List<ResponseProfile> responses = new();

        try
        {
            responses.AddRange(_service.List(_mapper.ToEntity(request)));
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
                new ResponseProfile
                {
                    statusCode = (int)HttpStatusCode.InternalServerError,
                    message = string.Format(Messages.messageError, ex.Message)
                });
        }

        return responses;
    }
}