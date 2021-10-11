using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountService.DiscountServiceClient _discountGrpcClient;

        public DiscountGrpcService(DiscountService.DiscountServiceClient discountGrpcClient)
        {
            _discountGrpcClient = discountGrpcClient;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };

            return await _discountGrpcClient.GetDiscountAsync(discountRequest);
        }


    }
}
