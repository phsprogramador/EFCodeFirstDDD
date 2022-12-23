using Applications.DTO.Request;
using Applications.DTO.Response;
using Applications.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Controllers
{

    public class ProfileController : Controller
    {
        private readonly IApplicationServiceProfile _application;
        public ProfileController(IApplicationServiceProfile application)
        {
            _application = application;
        }

        [HttpPost]
        [Route("[controller]")]
        public IActionResult Create([FromBody] List<RequestProfile> requests)
        {
            List<ResponseProfile> responses = _application.Create(requests);

            if (responses.First().statusCode != (int)HttpStatusCode.OK)
                return new ObjectResult(responses.First().message) { StatusCode = responses.First().statusCode };

            return CreatedAtAction(nameof(Query), new { id = responses.First().id }, responses);
        }

        [HttpGet]
        [Route("[controller]/{id:Guid}")]
        public IActionResult Query([FromRoute] Guid id)
        {
            ResponseProfile response = _application.Query(new RequestProfile { id = id, active = true });

            if (response.statusCode != (int)HttpStatusCode.OK)
                return new ObjectResult(response.message) { StatusCode = response.statusCode };

            return Ok(response);
        }

        [HttpGet]
        [Route("[controller]/{name}")]
        public IActionResult Query([FromRoute] string name)
        {
            ResponseProfile response = _application.Query(new RequestProfile { name = name, active = true });

            if (response.statusCode != (int)HttpStatusCode.OK)
                return new ObjectResult(response.message) { StatusCode = response.statusCode };

            return Ok(response);
        }

        [HttpPut]
        [Route("[controller]/{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] RequestProfile request)
        {
            ResponseBase response = _application.Update(id, request);

            if (response.statusCode != (int)HttpStatusCode.OK)
                return new ObjectResult(response.message) { StatusCode = response.statusCode };

            return Ok();
        }

        [HttpDelete]        
        [Route("[controller]/{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            ResponseBase response = _application.Remove(id);

            if (response.statusCode != (int)HttpStatusCode.OK)
                return new ObjectResult(response.message) { StatusCode = response.statusCode };

            return Ok();
        }

        [HttpPost]        
        [Route("[controller]/list/")]
        public IActionResult List([FromBody] RequestProfile request)
        {
            List<ResponseProfile> responses = _application.List(request).ToList();

            if (responses.First().statusCode != (int)HttpStatusCode.OK)
                return new ObjectResult(responses.First().message) { StatusCode = responses.First().statusCode };

            return Ok(responses);
        }
    }
}