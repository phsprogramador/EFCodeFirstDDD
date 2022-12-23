using Applications.DTO.Request;
using Applications.DTO.Response;
using Applications.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IApplicationServiceUser _application;

        public UserController(IApplicationServiceUser application)
        {
            _application = application;
        }

        ///<summary>
        ///New user
        ///</summary>
        ///<param name="requests"> Request properties</param>
        ///<returns>IActionResult</returns>
        ///<response code="201">Sucess</response>
        [HttpPost]        
        [Route("[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] List<RequestUser> requests)
        {
            List<ResponseUser> responses = _application.Create(requests);

            if (responses.First().statusCode != (int)HttpStatusCode.OK)
                return new ObjectResult(responses.First().message) { StatusCode = responses.First().statusCode };

            return CreatedAtAction(nameof(Query), responses);
        }

        [HttpGet]
        [Route("[controller]/{id:Guid}")]
        public IActionResult Query([FromRoute] Guid id)
        {
            ResponseUser response = _application.Query(new RequestUser { id = id, active = true });

            if (response.statusCode != (int)HttpStatusCode.OK)
                return new ObjectResult(response.message) { StatusCode = response.statusCode };

            return Ok(response);
        }

        [HttpGet]
        [Route("[controller]/{login}/{password}")]
        public IActionResult Query([FromRoute] string login, string password)
        {
            ResponseUser response = _application.Query(new RequestUser { login = login, password = password, active = true });

            if (response.statusCode != (int)HttpStatusCode.OK)
                return new ObjectResult(response.message) { StatusCode = response.statusCode };

            return Ok(response);
        }

        [HttpPut]
        [Route("[controller]/{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] RequestUser request)
        {
            ResponseBase response = _application.Update(id, request);

            if (response.statusCode != (int)HttpStatusCode.OK)
                return new ObjectResult(response.message) { StatusCode = response.statusCode };

            return Ok();
        }

        [HttpDelete]
        [Route("[controller]/{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            ResponseBase response = _application.Remove(id);

            if (response.statusCode != (int)HttpStatusCode.OK)
                return new ObjectResult(response.message) { StatusCode = response.statusCode };

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/list/")]
        public IActionResult List([FromBody] RequestUser request)
        {
            List<ResponseUser> responses = _application.List(request).ToList();

            if (responses.First().statusCode != (int)HttpStatusCode.OK)
                return new ObjectResult(responses.First().message) { StatusCode = responses.First().statusCode };

            return Ok(responses);
        }
    }
}
