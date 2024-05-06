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
using System.Collections.Generic;

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
        [SwaggerOperation(Summary = "Get Order by Id")]
        [SwaggerResponse(200, "Order details", typeof(GetOrderResponseModel))]
        public IActionResult Get(string id)
        {
            if (Guid.TryParse(id, out Guid orderId))
            {
                return Ok(orderController.GetOrder(new GetOrderRequestModel(orderId)));
            }
            else
            {
                _logger.LogError($"Error while retrivng id from query string: {Request.Query}");
                return BadRequest("Código inválido!");
            }

        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get Order by Client Id")]
        [SwaggerResponse(200, "Dados do order", typeof(List<GetOrderResponseModel>))]
        public IActionResult Get()
        {
            var sub = User.FindFirstValue("sub");

            if (Guid.TryParse(sub, out Guid userId))
            {
                return Ok(orderController.GetOrderByClientId(new GetOrderByClientIdRequestModel(userId)));
            }
            else
            {
                _logger.LogError($"Error while retrivng id from query string: {sub}");
                return BadRequest("Código inválido!");
            }

        }


        [HttpPost(Name = "CreateOrder")]
        [SwaggerOperation(Summary = "Create a new order")]
        [SwaggerResponse(200, "Order Id", typeof(CreateOrderResponseModel))]
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
        [SwaggerOperation(Summary = "Update order")]
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