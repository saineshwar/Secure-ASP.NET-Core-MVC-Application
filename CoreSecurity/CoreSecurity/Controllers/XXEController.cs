using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSecurity.Controllers
{
    public class XXEController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public XXEController(IHostingEnvironment hostingEnvironment)
        {
            _environment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            

            string xml = "<?xml version=\"1.0\"?><!DOCTYPE doc [<!ENTITY win SYSTEM \"file:///D:/demo.txt\">]><doc>&win;</doc>";

            XmlReaderSettings rs = new XmlReaderSettings();

            rs.DtdProcessing = DtdProcessing.Parse;

            XmlReader myReader = XmlReader.Create(new StringReader(xml), rs);

            while (myReader.Read())
            {
                var data = myReader.Value;
            }
           
            return View();
        }

        [HttpPost]

        public IActionResult Index(FormCollection formCollection)
        {

            #region Attack 1
            //var fileName = Path.Combine(_environment.WebRootPath, "XmlFiles") + "\\temp.xml";
            //XmlTextReader reader = new XmlTextReader(fileName);
            //reader.DtdProcessing = DtdProcessing.Ignore;
            //while (reader.Read())
            //{
            //    var data = reader.Value;
            //}

            #endregion

            return View();
        }
    }
}