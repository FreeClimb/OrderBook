using OrderBook.Domain;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrderBook.WebApi.Controllers
{
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        private readonly IOrderRepository _repository;

        public OrderController(IOrderRepository repository)
        {
            _repository = repository;
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet, Route("all")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                ICollection<Order> orders = await _repository.GetAllAsync();
                return Ok(orders);
            }
            catch (ErrorDbException ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ex.Error));
            }
        }

        [HttpGet, Route("details")]
        public async Task<IHttpActionResult> GetOrderDetails(int orderNum)
        {
            try
            {
                ICollection<OrderDetailWithProduct> details = await _repository.GetOrderDetailWithProductsByNumAsync(orderNum);

                return Ok(details);
            }
            catch (ErrorDbException ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ex.Error));
            }
        }

        [HttpPatch, Route("{orderNum:int}")]
        public async Task<IHttpActionResult> Patch(int orderNum)
        {
            try
            {
                Order order = await _repository.SetCompleteAsync(orderNum);

                return Ok(order);
            }
            catch (ErrorDbException ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ex.Error));
            }
        }
    }
}
