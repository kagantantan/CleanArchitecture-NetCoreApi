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

        /// <summary>
        /// Get All Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [SwaggerResponse(StatusCodes.Status200OK, "Products", typeof(BaseResponseDto<List<ProductDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Products", typeof(BaseResponseDto<List<ProductDto>>))]
        public async Task<ActionResult<BaseResponseDto<List<ProductDto>>>> Get([FromQuery]ProductRequest request)
        {
            var result = await _mediator.Send(request);

            if (result == null)
            {
                return this.NotFound("No data available");
            }

            return Ok(result);

        }

        /// <summary>
        /// Get All Products With Pagination
        /// </summary>
        /// <returns></returns>
        [HttpGet("pagination")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [SwaggerResponse(StatusCodes.Status200OK, "Products", typeof(PagedResponse<List<ProductDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Products", typeof(PagedResponse<List<ProductDto>>))]
        public async Task<ActionResult<PagedResponse<List<ProductDto>>>> GetAll([FromQuery]ProductPaginationRequest request)
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
