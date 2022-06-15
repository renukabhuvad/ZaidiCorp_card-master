using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZaidiCorp.Controllers
{
    public class SurveyController : Controller
    {
        // GET: Survey
        public ActionResult SavingSurvey()
        {
            return View();
        }
        public ActionResult RetirementSurvey()
        {
            return View();
        }
        public ActionResult ChildrenEducationSurvey()
        {
            return View();
        }
        public ActionResult IncomeProtectionSurvey()
        {
            return View();
        }
    }
}