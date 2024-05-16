using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Security.Claims;
using Sanduba.Core.Application.Abstraction.Orders.RequestModel;
using Sanduba.Core.Application.Abstraction.Orders.ResponseModel;
using Sanduba.Core.Application.Abstraction.Orders;
using System.Collections.Generic;
using Sanduba.API.Orders.Requests;
using AutoMapper;

namespace Sanduba.API.Orders
{
    [Authorize]
    [ApiController]
    [Route("order")]
    public class OrderApiEndpoint(ILogger<OrderApiEndpoint> logger, OrderController<IActionResult> orderController, IMapper mapper) : ControllerBase
    {
        private readonly ILogger<OrderApiEndpoint> _logger = logger;
        private readonly OrderController<IActionResult> orderController = orderController;
        private readonly IMapper _mapper = mapper;

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Order by Id")]
        [SwaggerResponse(200, "Order details", typeof(GetOrderResponseModel))]
        public IActionResult Get(string id)
        {
            if (Guid.TryParse(id, out Guid orderId))
            {
                return orderController.GetOrder(new GetOrderRequestModel(orderId));
            }
            else
            {
                _logger.LogError($"Error while retrivng id from query string: {Request.Query}");
                return BadRequest("Código inválido!");
            }

        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get Order by Client Id")]
        [SwaggerResponse(200, "Order details", typeof(List<GetOrderResponseModel>))]
        public IActionResult Get()
        {
            var sub = User.FindFirstValue("sub");

            if (Guid.TryParse(sub, out Guid userId))
            {
                return orderController.GetOrderByClientId(new GetOrderByClientIdRequestModel(userId));
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
        public IActionResult Post(OrderApiCreateRequest request)
        {
            var sub = User.FindFirstValue("sub");
            Guid customerId;

            if (!Guid.TryParse(sub, out customerId))
            {
                _logger.LogError($"Erro ao obter usuário na sessão. Parametro sub: {sub}");
                return BadRequest("Usuário inválido! ");
            }

            var controllerRequest = _mapper.Map<CreateOrderRequestModel>(request);
            controllerRequest.ClientId = customerId;

            return orderController.CreateOrder(controllerRequest);
        }
    }
}