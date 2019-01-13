

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PersonApi.Business;
using PersonApi.Data.VO;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tapioca.HATEOAS;

namespace PersonApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookBusiness _bookBusiness;

        public BooksController(IBookBusiness bookBusiness) => _bookBusiness = bookBusiness;

        [HttpGet]
        [SwaggerResponse(200, Type = typeof(List<PersonVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get() => Ok(_bookBusiness.FindAll());

        [HttpGet("{id}")]
        [SwaggerResponse(200, Type = typeof(PersonVO))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var response = _bookBusiness.FindById(id);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost]
        [SwaggerResponse(201, Type = typeof(PersonVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookVO book) => book == null
            ? (IActionResult) BadRequest()
            : Ok(new ObjectResult(_bookBusiness.Create(book)));
    
        [HttpPut]
        [SwaggerResponse(202, Type = typeof(PersonVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVO book) => Ok(_bookBusiness.Update(book));

        [HttpDelete("{id}")]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}