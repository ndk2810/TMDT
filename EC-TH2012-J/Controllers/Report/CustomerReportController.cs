using System; 
using System.Collections.Generic; 
using System.Data.Entity; 
using System.Data.Entity.Infrastructure; 
using System.Linq; 
using System.Net; 
using System.Net.Http; 
using System.Web.Http;  
using System.Threading.Tasks; 
using System.Web; 
using System.IO; 
using System.Net.Http.Headers;
using EC_TH2012_J.Models;
namespace EC_TH2012_J.Controllers
{
    public class CustomerReportController : ApiController
    {
        // GET api/<controller> 
        [HttpGet] 
        public async Task<HttpResponseMessage> GetXLSReport() 
        { 
            string fileName = string.Concat("Contacts.xls"); 
            string filePath = HttpContext.Current.Server.MapPath("~/Reports/" + fileName);

            List<AspNetUser> accountList = new UserModel().GetAll();

            await EC_TH2012_J.Models.Report.ReportGenerator.GenerateCustomerXLS(accountList, filePath); 
 
            HttpResponseMessage result = null; 
            result = Request.CreateResponse(HttpStatusCode.OK); 
            result.Content = new StreamContent(new FileStream(filePath, FileMode.Open)); 
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment"); 
            result.Content.Headers.ContentDisposition.FileName = fileName; 
 
            return result; 
        } 
	}
}