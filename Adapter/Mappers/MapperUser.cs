using Domains;
using Adapteres.Interfaces;
using Applications.DTO.Request;
using Applications.DTO.Response;

namespace Adapteres.Mappers
{
    public class MapperUser : MapperBase<User, ResponseUser, RequestUser>, IMapperUser
    {   
        public override User ToEntity(RequestUser request)
        {
            User entity = new();

            entity.id = request.id;
            entity.idProfile = request.idTypeUser;
            entity.name = request.name;
            entity.login = request.login;
            entity.password = request.password;
            entity.email = request.email;
            entity.dtInsert = request.dtInsert;
            entity.dtUpdate = request.dtUpdate;
            entity.active = request.active;

            return entity;
        }        
        public override User ToEntity(ResponseUser response) 
        {
            User entity = new();
            entity.id = response.id;
            entity.idProfile = response.idTypeUser;
            entity.name = response.name;
            entity.login = response.login;
            entity.password = response.password;
            entity.email = response.email;
            entity.dtInsert = response.dtInsert;
            entity.dtUpdate = response.dtUpdate;
            entity.active = response.active;

            return entity;
        }
        public virtual User ToEntity(User entity, RequestUser request) 
        {
            User user = new();
            user.id = request.id != Guid.Empty ? request.id : entity.id;
            user.idProfile = request.idTypeUser != Guid.Empty ? request.idTypeUser : entity.idProfile;
            user.name = !string.IsNullOrEmpty(request.name)? request.name : entity.name;
            user.login = !string.IsNullOrEmpty(request.login)? request.login : entity.login;
            user.password = !string.IsNullOrEmpty(request.password) ? request.password : entity.login;
            user.email = !string.IsNullOrEmpty(request.email)? request.email : entity.email;
            user.dtInsert = entity.dtInsert;
            user.dtUpdate = DateTime.Now;
            user.active = request.active;

            return user;
        }
        public override ResponseUser ToResponse(string message, int statusCode)
        {
            ResponseUser response = new();

            response.statusCode = statusCode;
            response.message = message;

            return response;
        }
        public override ResponseUser ToResponse(string message, int statusCode, User entity)
        {
            ResponseUser response = new();

            response.id = entity.id;
            response.idTypeUser = entity.idProfile;
            response.name = entity.name;
            response.login = entity.login;
            response.password = entity.password;
            response.email = entity.email;
            response.dtInsert = entity.dtInsert;
            response.dtUpdate = entity.dtUpdate;
            response.active = entity.active;

            response.statusCode = statusCode;
            response.message = message;

            return response;
        }      
    }
}