using Domains;
using Adapteres.Interfaces;
using Applications.DTO.Request;
using Applications.DTO.Response;

namespace Adapteres.Mappers;

public class MapperProfile : MapperBase<Profile, ResponseProfile, RequestProfile>, IMapperProfile
{
    public override Profile ToEntity(RequestProfile request)
    {
        Profile entity = new();

        entity.id = request.id;
        entity.name = request.name;
        entity.description = request.description;
        entity.dtInsert = request.dtInsert;
        entity.dtUpdate = request.dtUpdate;
        entity.active = request.active;

        return entity;
    }
    public override Profile ToEntity(ResponseProfile response)
    {
        Profile entity = new();
        entity.id = entity.id;
        entity.name = entity.name;
        entity.description = entity.description;
        entity.dtInsert = entity.dtInsert;
        entity.dtUpdate = entity.dtUpdate;
        entity.active = entity.active;
        return entity;
    }
    public virtual Profile ToEntity(Profile entity, RequestProfile request)
    {
        Profile user = new();
        user.id = request.id != Guid.Empty ? request.id : entity.id;        
        user.name = !string.IsNullOrEmpty(request.name) ? request.name : entity.name;
        user.description = !string.IsNullOrEmpty(request.description) ? request.description : entity.description;        
        user.dtInsert = entity.dtInsert;
        user.dtUpdate = DateTime.Now;
        user.active = request.active;

        return user;
    }
    public override ResponseProfile ToResponse(string message, int statusCode)
    {
        ResponseProfile entity = new();

        entity.message = message;
        entity.statusCode = statusCode;

        return entity;
    }
    public override ResponseProfile ToResponse(string message, int statusCode, Profile entity)
    {
        ResponseProfile request = new();

        request.id = entity.id;
        request.name = entity.name;
        request.description = entity.description;
        request.dtInsert = entity.dtInsert;
        request.dtUpdate = entity.dtUpdate;
        request.active = entity.active;

        request.statusCode = statusCode;
        request.message = message;

        return request;
    }
}