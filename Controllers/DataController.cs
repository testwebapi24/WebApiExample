using Microsoft.AspNetCore.Mvc;

namespace WebApiExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetPdfBase64/{token}/{param1}")]
        public string GetPdfBase64(string token, string param1)
        {
            // https://localhost:7082/data/GetPdfBase64/tokenval/paramval

            try
            {
                //var isAuthorised = IsAuthorised(token);
                //if (!isAuthorised)
                //{
                //    return "";
                //}

                string base64String = "abc";


                return base64String;
            }
            catch (Exception e)
            {
                return "";
            }

        }

        private bool IsAuthorised(string token)
        {
            try
            {
                var password = "tnFO^PCI2o#YKE03h{`,";
                var encryptionHelper = new EncryptionHelper.RijndaelCrypt(password);
                var authToken = encryptionHelper.Decrypt(token.Replace("_", "/"));

                var expires = DateTime.Parse(authToken);
                if (DateTime.Compare(DateTime.Now, expires) > 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
