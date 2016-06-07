using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Validator;

namespace MRZApi.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpPost, Route("api/mrz/validate")]
        public IHttpActionResult Validate(MRZForm m)
        {
            MRZ o = new MRZ();
            o.SetPassportNum(m.PassportNum);
            o.SetNationality(m.Nationality);
            o.SetDOB(m.DOB);
            o.SetSex(m.Sex);
            o.SetPassportExpDate(m.Expiry);
            o.SetPersonalNum();
            o.SetFinalCheckDigit();
            return Ok(o);
        }
    }
}
