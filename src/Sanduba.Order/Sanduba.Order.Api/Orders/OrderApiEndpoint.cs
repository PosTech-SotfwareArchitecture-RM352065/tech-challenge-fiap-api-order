using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sanduba.API.Pedidos.Requests;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Security.Claims;
using Sanduba.Core.Application.Abstraction.Orders.RequestModel;
using Sanduba.Core.Application.Abstraction.Orders.ResponseModel;
using Sanduba.Core.Application.Abstraction.Orders;
using System.Linq;
using OrderItem = Sanduba.Core.Application.Abstraction.Orders.RequestModel.OrderItem;

namespace Sanduba.API.Pedidos
{
    [Authorize]
    [ApiController]
    [Route("order")]

    public class OrderApiEndpoint : ControllerBase
    {
        private readonly ILogger<OrderApiEndpoint> _logger;
        private readonly OrderController<string> orderController;

        public OrderApiEndpoint(ILogger<OrderApiEndpoint> logger, OrderController<string> orderController)
        {
            _logger = logger;
            this.orderController = orderController;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Consulta order a partir do número de order")]
        [SwaggerResponse(200, "Dados do order", typeof(GetOrderResponseModel))]
        public IActionResult Get(Guid requestModel)
        {
            return Ok(orderController.GetOrder(new GetOrderRequestModel(requestModel)));
        }

        [HttpPost(Name = "CadastraPedido")]
        [SwaggerOperation(Summary = "Cadastra novo order")]
        [SwaggerResponse(200, "Id do order", typeof(CreateOrderResponseModel))]
        public IActionResult Post(CreateOrderApiRequest request)
        {
            var sub = User.FindFirstValue("sub");
            Guid userId;

            if (!Guid.TryParse(sub, out userId))
            {
                _logger.LogError($"Erro ao obter usuário na sessão. Parametro sub: {sub}");
                return BadRequest("Usuário inválido! ");
            }

            var controllerRequest = new CreateOrderRequestModel(userId, request.Items
                                                .Select(item =>
                                                    new OrderItem
                                                    (
                                                        ProductId: item.ProductId,
                                                        UnitPrice: item.UnitPrice
                                                    )).ToList()); ;

            return Ok(orderController.CreateOrder(controllerRequest));
        }

        [HttpPut(Name = "AtualizaPedido")]
        [SwaggerOperation(Summary = "Cadastra novo order")]
        [SwaggerResponse(200, "Status order", typeof(UpdateOrderResponseModel))]
        public IActionResult Put(UpdateStatisOrderResquestModel requestModel)
        {
            var sub = User.FindFirstValue("sub");
            Guid userId;

            if (!Guid.TryParse(sub, out userId))
            {
                _logger.LogError($"Erro ao obter usuário na sessão. Parametro sub: {sub}");
                return BadRequest("Usuário inválido! ");
            }

            return Ok();
        }
    }
}