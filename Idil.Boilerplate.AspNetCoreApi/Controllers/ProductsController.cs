using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Idil.Boilerplate.Core.Dtos;
using Idil.Boilerplate.Core.Dtos.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Idil.Boilerplate.AspNetCoreApi.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/products
        /// <summary>
        /// Get All Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [SwaggerResponse(StatusCodes.Status200OK, "Products", typeof(ProductResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Products", typeof(BaseResponseDto))]
        public async Task<ActionResult<ProductResponse>> Get([FromQuery]ProductRequest request)
        {
            var result = await _mediator.Send(request);

            if (result == null)
            {
                return this.NotFound("No data available");
            }

            return Ok(result);

        }
    }
}
