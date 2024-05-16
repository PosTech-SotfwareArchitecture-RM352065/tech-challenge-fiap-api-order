using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sanduba.API.Carts.Requests;
using Sanduba.Core.Application.Abstraction.Carts;
using Sanduba.Core.Application.Abstraction.Carts.RequestModel;
using Sanduba.Core.Application.Abstraction.Carts.ResponseModel;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Sanduba.API.Carts
{
    [ApiController]
    [Route("cart")]
    public class CartApiEndpoint(ILogger<CartApiEndpoint> logger, CartController<IActionResult> cartController) : ControllerBase
    {
        private readonly ILogger<CartApiEndpoint> _logger = logger;
        private readonly CartController<IActionResult> _cartController = cartController;

        [Authorize]
        [HttpGet(Name = "GetSummary")]
        [SwaggerOperation(Summary = "Obtem produtos no carrinho")]
        [SwaggerResponse(200, "Itens no carrinho", typeof(List<GetSummaryRequestModel>))]
        public IActionResult Get()
        {
            var sub = User.FindFirstValue("sub");
            Guid userId;

            if (!Guid.TryParse(sub, out userId))
            {
                _logger.LogError($"Erro ao obter usuário na sessão. Parametro sub: {sub}");
                return BadRequest("Usuário inválido! ");
            }

            var controllerRequest = new GetSummaryRequestModel(userId);

            return _cartController.GetSummary(controllerRequest);
        }

        [Authorize]
        [HttpPost(Name = "AdicionarProduto")]
        [SwaggerOperation(Summary = "Adicionar produto ao Cart")]
        [SwaggerResponse(200, "Lista do Cart", typeof(AddItemResponseModel))]
        public IActionResult Post(AddItemResponseModel request)
        {
            var sub = User.FindFirstValue("sub");
            Guid userId;

            if (!Guid.TryParse(sub, out userId))
            {
                _logger.LogError($"Erro ao obter usuário na sessão. Parametro sub: {sub}");
                return BadRequest("Usuário inválido! ");
            }

            var controllerRequest = new AddItemRequestModel(userId, request.ProductId);

            return _cartController.AddItem(controllerRequest);
        }

        [Authorize]
        [HttpPost("{id}/checkout")]
        [SwaggerOperation(Summary = "Adicionar produto ao Cart")]
        [SwaggerResponse(200, "Lista do Cart", typeof(AddItemResponseModel))]
        public IActionResult Post(string id)
        {
            var sub = User.FindFirstValue("sub");
            Guid userId;

            if (!Guid.TryParse(sub, out userId))
            {
                _logger.LogError($"Erro ao obter usuário na sessão. Parametro sub: {sub}");
                return BadRequest("Usuário inválido! ");
            }

            return _cartController.Checkout();
        }

        [Authorize]
        [HttpDelete(Name = "RemoveProduto")]
        [SwaggerOperation(Summary = "Remove produto do Cart")]
        [SwaggerResponse(200, "Número do Produto", typeof(RemoveItemRequest))]
        public IActionResult Delete(RemoveItemRequest request)
        {
            var sub = User.FindFirstValue("sub");
            Guid userId;

            if (!Guid.TryParse(sub, out userId))
            {
                _logger.LogError($"Erro ao obter usuário na sessão. Parametro sub: {sub}");
                return BadRequest("Usuário inválido! ");
            }

            var controllerRequest = new RemoveItemRequestModel(userId, request.ProductId);

            return _cartController.RemoveItem(controllerRequest);
        }
    }
}