using Pasquali.Sisprods.Domain.Commands;
using Pasquali.Sisprods.Domain.Entities;
using Pasquali.Sisprods.Domain.Handlers;
using Pasquali.Sisprods.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Pasquali.Sisprods.Api.Controllers
{
    public class ClientController : ApiController
    {
        private readonly ClientRepository _repository;
        private readonly ClientHandler _handler;

        public ClientController(ClientRepository repository, ClientHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        // GET: api/Client
        [ResponseType(typeof(IEnumerable<Client>))]
        public IHttpActionResult GetClients()
        {
            return Ok((IEnumerable<Client>)_repository.GetAll().ToList());
        }

        // GET: api/Client/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(int id)
        {
            Client client = _repository.GetById(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/Client/5
        [ResponseType(typeof(GenericCommandResult))]
        public IHttpActionResult PutClient([FromUri] int id, [FromBody] UpdateClientCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != command.Id)
                return BadRequest();

            try
            {
                var result = (GenericCommandResult)_handler.Handle(command);

                if (!result.Success)
                    return Content(HttpStatusCode.BadRequest, result.Data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    //return StatusCode(HttpStatusCode.InternalServerError);
                    return InternalServerError(ex);
                }
            }

        }

        // POST: api/Client
        [ResponseType(typeof(GenericCommandResult))]
        public IHttpActionResult PostClient([FromBody] CreateClientCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = (GenericCommandResult)_handler.Handle(command);

            if (!result.Success)
                return Content(HttpStatusCode.BadRequest, result.Data);

            return Ok(result);
        }

        // DELETE: api/Client/5
        [ResponseType(typeof(GenericCommandResult))]
        public IHttpActionResult DeleteClient([FromUri] int id, [FromBody] DeleteClientCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != command.Id)
                return BadRequest();

            try
            {
                var result = (GenericCommandResult)_handler.Handle(command);

                if (!result.Success)
                    return Content(HttpStatusCode.BadRequest, result.Data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    //return StatusCode(HttpStatusCode.InternalServerError);
                    return InternalServerError(ex);
                }
            }
        }

        private bool ClientExists(int id)
        {
            var clientExists = _repository.GetById(id);
            return (clientExists != null || clientExists.ClientId > 0);
        }

        // PUT: api/Client/5
        [Route("{id}/products")]
        [HttpPut]
        [ResponseType(typeof(GenericCommandResult))]
        public IHttpActionResult PutClientProducts([FromUri] int id, [FromBody] AddProductsClientCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != command.ClientId)
                return BadRequest();

            try
            {
                var result = (GenericCommandResult)_handler.Handle(command);

                if (!result.Success)
                    return Content(HttpStatusCode.BadRequest, result.Data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (!ClientExists(id))
                    return NotFound();

                return InternalServerError(ex);
                //return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}