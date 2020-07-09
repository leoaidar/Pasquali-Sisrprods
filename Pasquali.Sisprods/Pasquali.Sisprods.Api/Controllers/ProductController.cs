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
    public class ProductController : ApiController
    {
        private readonly ProductRepository _repository;
        private readonly ProductHandler _handler;

        public ProductController(ProductRepository repository, ProductHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        // GET: api/Product
        [ResponseType(typeof(IEnumerable<Product>))]
        public IHttpActionResult GetProducts()
        {
            return Ok((IEnumerable<Product>)_repository.GetAll().ToList());
        }

        // GET: api/Product/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = _repository.GetById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // PUT: api/Product/5
        [ResponseType(typeof(GenericCommandResult))]
        public IHttpActionResult PutProduct([FromUri] int id, [FromBody] UpdateProductCommand command)
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
                if (!ProductExists(id))
                    return NotFound();
                
                return InternalServerError(ex);
            }

        }

        // POST: api/Product
        [ResponseType(typeof(GenericCommandResult))]
        public IHttpActionResult PostProduct([FromBody] CreateProductCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = (GenericCommandResult)_handler.Handle(command);

            if (!result.Success)
                return Content(HttpStatusCode.BadRequest, result.Data);

            return Ok(result);
        }

        // DELETE: api/Product/5
        [ResponseType(typeof(GenericCommandResult))]
        public IHttpActionResult DeleteProduct([FromUri] int id, [FromBody] DeleteProductCommand command)
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
                if (!ProductExists(id))
                    return NotFound();

                return InternalServerError(ex);
            }
        }

        private bool ProductExists(int id)
        {
            var productExists = _repository.GetById(id);
            return (productExists != null || productExists.ProductId > 0);
        }
    }
}