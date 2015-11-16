using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiatCoinNetWeb.Controllers
{
    public class BlockController : Controller
    {
        // GET: Block
        public ActionResult Index()
        {
            return View();
        }
    }
}