using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BL;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]

    public class CustomersInTurnController : ApiController
    {
        public IHttpActionResult GetTurnsToCustomer(HttpRequestMessage request)
        {
            try
            {
                String access_token = request.Headers.Authorization.ToString();
                int custId = Token.GetCustIdFromToken(access_token);
                return Ok(CustInLineBL.GetTurnsToCustomer(custId));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IHttpActionResult DeleteTurn(int turnId)
        {
            try
            {
                TurnBL.DeleteTurn(turnId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
