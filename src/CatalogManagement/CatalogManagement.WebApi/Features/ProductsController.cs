using CatalogManagement.Application.Products.CreateProduct;
using CatalogManagement.Application.Products.DeleteProduct;
using CatalogManagement.Application.Products.GetProductById;
using CatalogManagement.Application.Products.GetProducts;
using CatalogManagement.Application.Products.UpdateProduct;
using Common.APICommon;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogManagement.WebApi.Features;

/// <summary>
/// Controller for managing product operations
/// </summary>
[Authorize]
[ApiController]
[Route("api/products")]
public class ProductsController(IMediator mediator) : Controller
{
    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="request">The product creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
        {
            Success = true,
            Message = "Product created successfully",
            Data = response
        });
    }

    /// <summary>
    /// Retrieves a product by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new GetProductByIdQuery(id);
        var response = await mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetProductByIdResponse>
        {
            Success = true,
            Message = "Product retrieved successfully",
            Data = response
        });
    }

    /// <summary>
    /// Retrieves a list of products with pagination, sorting and filtering
    /// </summary>
    /// <param name="query">The query parameters for retrieving products</param>
    /// <param name="filters">Additional filters as key-value pairs</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of products matching the query</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<GetProductsResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts(
        [FromQuery] QueryStringWithFilters<GetProductsQuery, GetProductsResponse> filters,
        CancellationToken cancellationToken)
    {
        var query = filters.GetQuery();
        return Ok(await mediator.Send(query, cancellationToken));
    }

    /// <summary>
    /// Update a product
    /// </summary>
    /// <param name="request">The product update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        var response = await mediator.Send(request, cancellationToken);

        return Ok(new ApiResponseWithData<UpdateProductResponse>
        {
            Success = true,
            Message = "Product updated successfully",
            Data = response
        });
    }

    /// <summary>
    /// Deletes a product by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the product was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(id);
        await mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Product deleted successfully"
        });
    }
}
