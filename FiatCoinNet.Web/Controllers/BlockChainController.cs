using FiatCoinNetWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiatCoinNetWeb.Controllers
{
    public class BlockChainController : Controller
    {
        private ModelDataContext _DataContext = null;
        protected ModelDataContext DataContext
        {
            get
            {
                if (_DataContext == null)
                {
                    _DataContext = new ModelDataContext();
                }

                var options = new DataLoadOptions();
                options.LoadWith<LowerLevelBlock>(p => p.Id);
                _DataContext.LoadOptions = options;

                return _DataContext;
            }
        }
        // GET: BlockChain
        public ActionResult Index()
        {
            var list = this.DataContext.LowerLevelBlocks;
            return View(this);
        }
    }
}