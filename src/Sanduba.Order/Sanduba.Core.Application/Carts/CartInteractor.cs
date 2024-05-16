using System.Collections.Generic;
using System.Linq;
using Sanduba.Core.Domain.Commons.Exceptions;
using Sanduba.Core.Application.Abstraction.Carts.RequestModel;
using Sanduba.Core.Application.Abstraction.Carts.ResponseModel;
using Sanduba.Core.Application.Abstraction.Carts;

namespace Sanduba.Core.Application.Carts
{
    public class CartInteractor(ICartPersistenceGateway cartPersistence) : ICartInteractor
    {
        private readonly ICartPersistenceGateway _cartPersistence = cartPersistence;

        public AddItemResponseModel AddItem(AddItemRequestModel requestModel)
        {
            try
            {
                _cartPersistence.AddItem(requestModel.Id, requestModel.ProductId);

                return new AddItemResponseModel(requestModel.Id);
            }
            catch (DomainException ex)
            {
                throw;
            }
        }

        public GetSummaryResponseModel Checkout()
        {
            throw new System.NotImplementedException();
        }

        public List<GetSummaryResponseModel> GetSummary(GetSummaryRequestModel requestModel)
        {
            var produtos = _cartPersistence.GetSummary(requestModel.Id);
            return produtos.Select(produto =>
                new GetSummaryResponseModel
                (
                    Id: produto
                )).ToList();
        }

        public GetSummaryResponseModel RemoveItem(RemoveItemRequestModel requestModel)
        {
            _cartPersistence.RemoveItem(requestModel.Id, requestModel.ProductId);
            return new GetSummaryResponseModel
            (
                Id: requestModel.ProductId
            );
        }
    }
}
