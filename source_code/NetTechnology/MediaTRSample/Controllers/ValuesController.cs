using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MediaTRSample.Command;
using MediaTRSample.Request;
using Microsoft.AspNetCore.Mvc;

namespace MediaTRSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IMediator _mediator;

        public ValuesController(IMediator mediator) {
            _mediator = mediator;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //RegisterUser registerUser = new RegisterUser() { EmailAddress = "1", LastName = "2", FirstName = "3" };
            Task registered = _mediator.Send(new SendUpdateCommand("Send哈哈哈哈"));
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _mediator.Publish(new UpdateCommand("嘿嘿猫"));
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
