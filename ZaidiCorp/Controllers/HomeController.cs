using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZaidiCorp.DBService;
using ZaidiCorp.Models;
using Npgsql;
using System.Data;
using System.Text;
using System.IO;
using System.Net;

namespace ZaidiCorp.Controllers
{
    public class HomeController : Controller
    {
        NpgsqlConnection supercon = new NpgsqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["superadmin"].ConnectionString);
        NpgsqlConnection agcon = new NpgsqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["agentadmin"].ConnectionString);

        // Agent Digital Card

        public ActionResult DigitalCard(string customerid)
        {
            Session["url"] = "DigitalCard?customerid=" + customerid;
            Session["customerid"]= customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open();}
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction,d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            
            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }
                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT aboutusid, data,customerid  FROM public.aboutus where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            // da = new NpgsqlDataAdapter(cmd);
            //DataTable about = new DataTable();
            //da.Fill(about);
            //if (about.Rows.Count > 0)
            //{
            //    cust.about = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.vision where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable vision = new DataTable();
            //da.Fill(vision);
            //if (vision.Rows.Count > 0)
            //{
            //    cust.vision = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.tbl_service where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable service = new DataTable();
            //da.Fill(service);
            //if (service.Rows.Count > 0)
            //{
            //    cust.service = about.Rows[0]["data"].ToString();
            //}
            
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });

                }
                model.testimonials = testimoniallist;

            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }

            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, customerid, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + "" + " and isfor='Agent' order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }
                model.concept = Concept;
            }
            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;
            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }
            //supercon.Close();
            //agcon.Close();
            return View(model);

        }

        public ActionResult DigitalCard1(string customerid)
        {
            Session["url"] = "DigitalCard1?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction,d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }
                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT aboutusid, data,customerid  FROM public.aboutus where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            // da = new NpgsqlDataAdapter(cmd);
            //DataTable about = new DataTable();
            //da.Fill(about);
            //if (about.Rows.Count > 0)
            //{
            //    cust.about = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.vision where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable vision = new DataTable();
            //da.Fill(vision);
            //if (vision.Rows.Count > 0)
            //{
            //    cust.vision = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.tbl_service where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable service = new DataTable();
            //da.Fill(service);
            //if (service.Rows.Count > 0)
            //{
            //    cust.service = about.Rows[0]["data"].ToString();
            //}

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });


                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename  FROM public.gallery_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + "" + " and isfor='Agent' order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.concept = Concept;
            }
            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard2(string customerid)
        {
            Session["url"] = "DigitalCard2?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction,d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }

                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT aboutusid, data,customerid  FROM public.aboutus where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            // da = new NpgsqlDataAdapter(cmd);
            //DataTable about = new DataTable();
            //da.Fill(about);
            //if (about.Rows.Count > 0)
            //{
            //    cust.about = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.vision where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable vision = new DataTable();
            //da.Fill(vision);
            //if (vision.Rows.Count > 0)
            //{
            //    cust.vision = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.tbl_service where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable service = new DataTable();
            //da.Fill(service);
            //if (service.Rows.Count > 0)
            //{
            //    cust.service = about.Rows[0]["data"].ToString();


            //}

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });

                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + "" + " and isfor='Agent' order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.concept = Concept;
            }

            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard3(string customerid)
        {
            Session["url"] = "DigitalCard3?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction,d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }

                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT aboutusid, data,customerid  FROM public.aboutus where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            // da = new NpgsqlDataAdapter(cmd);
            //DataTable about = new DataTable();
            //da.Fill(about);
            //if (about.Rows.Count > 0)
            //{
            //    cust.about = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.vision where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable vision = new DataTable();
            //da.Fill(vision);
            //if (vision.Rows.Count > 0)
            //{
            //    cust.vision = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.tbl_service where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable service = new DataTable();
            //da.Fill(service);
            //if (service.Rows.Count > 0)
            //{
            //    cust.service = about.Rows[0]["data"].ToString();


            //}

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });

                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });

                }

                model.gallery = Gallery;
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + "" + " and isfor='Agent' order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }
                model.concept = Concept;
            }

            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard4(string customerid)
        {
            Session["url"] = "DigitalCard4?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction,d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }
                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT aboutusid, data,customerid  FROM public.aboutus where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            // da = new NpgsqlDataAdapter(cmd);
            //DataTable about = new DataTable();
            //da.Fill(about);
            //if (about.Rows.Count > 0)
            //{
            //    cust.about = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.vision where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable vision = new DataTable();
            //da.Fill(vision);
            //if (vision.Rows.Count > 0)
            //{
            //    cust.vision = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.tbl_service where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable service = new DataTable();
            //da.Fill(service);
            //if (service.Rows.Count > 0)
            //{
            //    cust.service = about.Rows[0]["data"].ToString();


            //}

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });

                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });

                }

                model.gallery = Gallery;
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + "" + " and isfor='Agent' order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });

                }

                model.concept = Concept;
            }

            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard5(string customerid)
        {
            Session["url"] = "DigitalCard5?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction,d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }

                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT aboutusid, data,customerid  FROM public.aboutus where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            // da = new NpgsqlDataAdapter(cmd);
            //DataTable about = new DataTable();
            //da.Fill(about);
            //if (about.Rows.Count > 0)
            //{
            //    cust.about = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.vision where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable vision = new DataTable();
            //da.Fill(vision);
            //if (vision.Rows.Count > 0)
            //{
            //    cust.vision = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.tbl_service where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable service = new DataTable();
            //da.Fill(service);
            //if (service.Rows.Count > 0)
            //{
            //    cust.service = about.Rows[0]["data"].ToString();


            //}

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });


                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });

                }

                model.gallery = Gallery;
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + "" + " and isfor='Agent' order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.concept = Concept;
            }

            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard6(string customerid)
        {
            Session["url"] = "DigitalCard6?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction,d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }
                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT aboutusid, data,customerid  FROM public.aboutus where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            // da = new NpgsqlDataAdapter(cmd);
            //DataTable about = new DataTable();
            //da.Fill(about);
            //if (about.Rows.Count > 0)
            //{
            //    cust.about = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.vision where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable vision = new DataTable();
            //da.Fill(vision);
            //if (vision.Rows.Count > 0)
            //{
            //    cust.vision = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.tbl_service where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable service = new DataTable();
            //da.Fill(service);
            //if (service.Rows.Count > 0)
            //{
            //    cust.service = about.Rows[0]["data"].ToString();


            //}

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });


                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }

            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + "" + " and isfor='Agent' order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });

                }

                model.concept = Concept;
            }

            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard7(string customerid)
        {
            Session["url"] = "DigitalCard7?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction,d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }

                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT aboutusid, data,customerid  FROM public.aboutus where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            // da = new NpgsqlDataAdapter(cmd);
            //DataTable about = new DataTable();
            //da.Fill(about);
            //if (about.Rows.Count > 0)
            //{
            //    cust.about = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.vision where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable vision = new DataTable();
            //da.Fill(vision);
            //if (vision.Rows.Count > 0)
            //{
            //    cust.vision = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.tbl_service where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable service = new DataTable();
            //da.Fill(service);
            //if (service.Rows.Count > 0)
            //{
            //    cust.service = about.Rows[0]["data"].ToString();


            //}

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });


                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + "" + " and isfor='Agent' order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.concept = Concept;
            }

            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;
            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }
            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard8(string customerid)
        {
            Session["url"] = "DigitalCard8?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction,d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
               if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }
                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT aboutusid, data,customerid  FROM public.aboutus where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            // da = new NpgsqlDataAdapter(cmd);
            //DataTable about = new DataTable();
            //da.Fill(about);
            //if (about.Rows.Count > 0)
            //{
            //    cust.about = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.vision where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable vision = new DataTable();
            //da.Fill(vision);
            //if (vision.Rows.Count > 0)
            //{
            //    cust.vision = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.tbl_service where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable service = new DataTable();
            //da.Fill(service);
            //if (service.Rows.Count > 0)
            //{
            //    cust.service = about.Rows[0]["data"].ToString();


            //}

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });

                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + "" + " and isfor='Agent' order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }
                model.concept = Concept;
            }
            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;
           
            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard9(string customerid)
        {
            Session["url"] = "DigitalCard9?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction,d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }
                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
            }
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT aboutusid, data,customerid  FROM public.aboutus where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            // da = new NpgsqlDataAdapter(cmd);
            //DataTable about = new DataTable();
            //da.Fill(about);
            //if (about.Rows.Count > 0)
            //{
            //    cust.about = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.vision where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable vision = new DataTable();
            //da.Fill(vision);
            //if (vision.Rows.Count > 0)
            //{
            //    cust.vision = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.tbl_service where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable service = new DataTable();
            //da.Fill(service);
            //if (service.Rows.Count > 0)
            //{
            //    cust.service = about.Rows[0]["data"].ToString();


            //}

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });

                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename  FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });

                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + "" + " and isfor='Agent' order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });


                }

                model.concept = Concept;
            }

            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard10(string customerid)
        {
            Session["url"] = "DigitalCard10?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction,d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }
                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
            }
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT aboutusid, data,customerid  FROM public.aboutus where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            // da = new NpgsqlDataAdapter(cmd);
            //DataTable about = new DataTable();
            //da.Fill(about);
            //if (about.Rows.Count > 0)
            //{
            //    cust.about = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.vision where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable vision = new DataTable();
            //da.Fill(vision);
            //if (vision.Rows.Count > 0)
            //{
            //    cust.vision = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.tbl_service where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable service = new DataTable();
            //da.Fill(service);
            //if (service.Rows.Count > 0)
            //{
            //    cust.service = about.Rows[0]["data"].ToString();


            //}

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });


                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename  FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });

                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + "" + " and isfor='Agent' order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.concept = Concept;
            }

            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard11(string customerid)
        {
            Session["url"] = "DigitalCard11?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction,d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }
                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }

            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT aboutusid, data,customerid  FROM public.aboutus where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            // da = new NpgsqlDataAdapter(cmd);
            //DataTable about = new DataTable();
            //da.Fill(about);
            //if (about.Rows.Count > 0)
            //{
            //    cust.about = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.vision where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable vision = new DataTable();
            //da.Fill(vision);
            //if (vision.Rows.Count > 0)
            //{
            //    cust.vision = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.tbl_service where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable service = new DataTable();
            //da.Fill(service);
            //if (service.Rows.Count > 0)
            //{
            //    cust.service = about.Rows[0]["data"].ToString();


            //}

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });


                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='Agent'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + "" + " and isfor='Agent' order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.concept = Concept;
            }

            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        //Do Digital Cards

        public ActionResult DigitalCard12(string customerid)
        {
            Session["url"] = "DigitalCard12?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction, d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "' and d.isfor = 'DO'", supercon);

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }
                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT aboutusid, data,customerid  FROM public.aboutus where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            // da = new NpgsqlDataAdapter(cmd);
            //DataTable about = new DataTable();
            //da.Fill(about);
            //if (about.Rows.Count > 0)
            //{
            //    cust.about = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.vision where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable vision = new DataTable();
            //da.Fill(vision);
            //if (vision.Rows.Count > 0)
            //{
            //    cust.vision = about.Rows[0]["data"].ToString();
            //}
            //if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            //cmd = new NpgsqlCommand("SELECT *  FROM public.tbl_service where customerid=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            //da = new NpgsqlDataAdapter(cmd);
            //DataTable service = new DataTable();
            //da.Fill(service);
            //if (service.Rows.Count > 0)
            //{
            //    cust.service = about.Rows[0]["data"].ToString();


            //}

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });


                }
                model.testimonials = testimoniallist;

            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO' " + "order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.concept = Concept;
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT video_id, url,title, id, edate, efrom, action  FROM public.video_digi where id=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable video = new DataTable();
            da.Fill(video);
            if (video.Rows.Count > 0)
            {
                List<video> videos = new List<video>();
                for (int i = 0; i <= video.Rows.Count - 1; i++)
                {
                    char[] delimiters = new char[] { '=', '\\' };
                    if (video.Rows[i]["url"].ToString().Contains("=") == true)
                    {
                        string[] parts = video.Rows[i]["url"].ToString().Split(delimiters,
                                     StringSplitOptions.RemoveEmptyEntries);
                        videos.Add(new video
                        {
                            url = parts[1].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }
                    else
                    {

                        string[] parts = video.Rows[i]["url"].ToString().Split('/');
                        videos.Add(new video
                        {
                            url = parts[3].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }


                }
                model.videos = videos;
            }
            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard13(string customerid)
        {
            Session["url"] = "DigitalCard13?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction, d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "' and d.isfor = 'DO'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }
                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });

                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'" + "order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.concept = Concept;
            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT video_id, url, id, title, edate, efrom, action  FROM public.video_digi where id=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable video = new DataTable();
            da.Fill(video);
            if (video.Rows.Count > 0)
            {
                List<video> videos = new List<video>();
                for (int i = 0; i <= video.Rows.Count - 1; i++)
                {
                    char[] delimiters = new char[] { '=', '\\' };
                    if (video.Rows[i]["url"].ToString().Contains("=") == true)
                    {
                        string[] parts = video.Rows[i]["url"].ToString().Split(delimiters,
                                     StringSplitOptions.RemoveEmptyEntries);
                        videos.Add(new video
                        {
                            url = parts[1].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }
                    else
                    {

                        string[] parts = video.Rows[i]["url"].ToString().Split('/');
                        videos.Add(new video
                        {
                            url = parts[3].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }


                }
                model.videos = videos;
            }
            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard14(string customerid)
        {
            Session["url"] = "DigitalCard14?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction, d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "' and d.isfor = 'DO'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }
                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });


                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'" + "order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.concept = Concept;
            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT video_id, url, id, title, edate, efrom, action  FROM public.video_digi where id=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable video = new DataTable();
            da.Fill(video);
            if (video.Rows.Count > 0)
            {
                List<video> videos = new List<video>();
                for (int i = 0; i <= video.Rows.Count - 1; i++)
                {
                    char[] delimiters = new char[] { '=', '\\' };
                    if (video.Rows[i]["url"].ToString().Contains("=") == true)
                    {
                        string[] parts = video.Rows[i]["url"].ToString().Split(delimiters,
                                     StringSplitOptions.RemoveEmptyEntries);
                        videos.Add(new video
                        {
                            url = parts[1].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }
                    else
                    {

                        string[] parts = video.Rows[i]["url"].ToString().Split('/');
                        videos.Add(new video
                        {
                            url = parts[3].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }


                }
                model.videos = videos;
            }
            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard15(string customerid)
        {
            Session["url"] = "DigitalCard15?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction, d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "' and d.isfor = 'DO'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }

                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });

                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'" + "order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.concept = Concept;
            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT video_id, url, id, title, edate, efrom, action  FROM public.video_digi where id=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable video = new DataTable();
            da.Fill(video);
            if (video.Rows.Count > 0)
            {
                List<video> videos = new List<video>();
                for (int i = 0; i <= video.Rows.Count - 1; i++)
                {
                    char[] delimiters = new char[] { '=', '\\' };
                    if (video.Rows[i]["url"].ToString().Contains("=") == true)
                    {
                        string[] parts = video.Rows[i]["url"].ToString().Split(delimiters,
                                     StringSplitOptions.RemoveEmptyEntries);
                        videos.Add(new video
                        {
                            url = parts[1].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }
                    else
                    {

                        string[] parts = video.Rows[i]["url"].ToString().Split('/');
                        videos.Add(new video
                        {
                            url = parts[3].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }


                }
                model.videos = videos;
            }
            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard16(string customerid)
        {
            Session["url"] = "DigitalCard16?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction, d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "' and d.isfor = 'DO'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }

                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });
                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'" + "order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.concept = Concept;
            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT video_id, url, title, id, edate, efrom, action  FROM public.video_digi where id=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable video = new DataTable();
            da.Fill(video);
            if (video.Rows.Count > 0)
            {
                List<video> videos = new List<video>();
                for (int i = 0; i <= video.Rows.Count - 1; i++)
                {
                    char[] delimiters = new char[] { '=', '\\' };
                    if (video.Rows[i]["url"].ToString().Contains("=") == true)
                    {
                        string[] parts = video.Rows[i]["url"].ToString().Split(delimiters,
                                     StringSplitOptions.RemoveEmptyEntries);
                        videos.Add(new video
                        {
                            url = parts[1].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }
                    else
                    {

                        string[] parts = video.Rows[i]["url"].ToString().Split('/');
                        videos.Add(new video
                        {
                            url = parts[3].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }


                }
                model.videos = videos;
            }
            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard17(string customerid)
        {
            Session["url"] = "DigitalCard17?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction, d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "' and d.isfor = 'DO'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }

                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });


                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'" + "order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });

                }

                model.concept = Concept;
            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT video_id, url, title, id, edate, efrom, action  FROM public.video_digi where id=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable video = new DataTable();
            da.Fill(video);
            if (video.Rows.Count > 0)
            {
                List<video> videos = new List<video>();
                for (int i = 0; i <= video.Rows.Count - 1; i++)
                {
                    char[] delimiters = new char[] { '=', '\\' };
                    if (video.Rows[i]["url"].ToString().Contains("=") == true)
                    {
                        string[] parts = video.Rows[i]["url"].ToString().Split(delimiters,
                                     StringSplitOptions.RemoveEmptyEntries);
                        videos.Add(new video
                        {
                            url = parts[1].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }
                    else
                    {

                        string[] parts = video.Rows[i]["url"].ToString().Split('/');
                        videos.Add(new video
                        {
                            url = parts[3].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }


                }
                model.videos = videos;
            }
            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard18(string customerid)
        {
            Session["url"] = "DigitalCard18?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction, d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "' and d.isfor = 'DO'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }

                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });


                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });

                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'" + "order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.concept = Concept;
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT video_id, url, id, title, edate, efrom, action  FROM public.video_digi where id=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable video = new DataTable();
            da.Fill(video);

            if (video.Rows.Count > 0)
            {
                List<video> videos = new List<video>();
                for (int i = 0; i <= video.Rows.Count - 1; i++)
                {
                    char[] delimiters = new char[] { '=', '\\' };
                    if (video.Rows[i]["url"].ToString().Contains("=") == true)
                    {
                        string[] parts = video.Rows[i]["url"].ToString().Split(delimiters,
                                     StringSplitOptions.RemoveEmptyEntries);
                        videos.Add(new video
                        {
                            url = parts[1].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }
                    else
                    {

                        string[] parts = video.Rows[i]["url"].ToString().Split('/');
                        videos.Add(new video
                        {
                            url = parts[3].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }


                }
                model.videos = videos;
            }
            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard19(string customerid)
        {
            Session["url"] = "DigitalCard19?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction, d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "' and d.isfor = 'DO'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }
                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });
                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });

                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'" + "order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });


                }

                model.concept = Concept;
            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT video_id, url, id, title, edate, efrom, action  FROM public.video_digi where id=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable video = new DataTable();
            da.Fill(video);
            if (video.Rows.Count > 0)
            {
                List<video> videos = new List<video>();
                for (int i = 0; i <= video.Rows.Count - 1; i++)
                {
                    char[] delimiters = new char[] { '=', '\\' };
                    if (video.Rows[i]["url"].ToString().Contains("=") == true)
                    {
                        string[] parts = video.Rows[i]["url"].ToString().Split(delimiters,
                                     StringSplitOptions.RemoveEmptyEntries);
                        videos.Add(new video
                        {
                            url = parts[1].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }
                    else
                    {

                        string[] parts = video.Rows[i]["url"].ToString().Split('/');
                        videos.Add(new video
                        {
                            url = parts[3].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }


                }
                model.videos = videos;
            }
            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard20(string customerid)
        {
            Session["url"] = "DigitalCard20?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction, d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "' and d.isfor = 'DO'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }

                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {

                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });


                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'" + "order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.concept = Concept;
            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT video_id, url, id, title, edate, efrom, action  FROM public.video_digi where id=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable video = new DataTable();
            da.Fill(video);
            if (video.Rows.Count > 0)
            {
                List<video> videos = new List<video>();
                for (int i = 0; i <= video.Rows.Count - 1; i++)
                {
                    char[] delimiters = new char[] { '=', '\\' };
                    if (video.Rows[i]["url"].ToString().Contains("=") == true)
                    {
                        string[] parts = video.Rows[i]["url"].ToString().Split(delimiters,
                                     StringSplitOptions.RemoveEmptyEntries);
                        videos.Add(new video
                        {
                            url = parts[1].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }
                    else
                    {

                        string[] parts = video.Rows[i]["url"].ToString().Split('/');
                        videos.Add(new video
                        {
                            url = parts[3].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }


                }
                model.videos = videos;
            }
            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard21(string customerid)
        {
            Session["url"] = "DigitalCard21?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction, d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "' and d.isfor = 'DO'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }
                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();
            }

            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT testimonialid, name, imageurl, testimonialdes FROM public.testimonial_digi where customerid=" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable testimonial = new DataTable();
            da.Fill(testimonial);
            if (testimonial.Rows.Count > 0)
            {
                List<testimonials> testimoniallist = new List<testimonials>();
                for (int i = 0; i <= testimonial.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (testimonial.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + testimonial.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }

                    testimoniallist.Add(new testimonials
                    {
                        name = testimonial.Rows[i]["name"].ToString(),
                        image = imgurl,
                        description = testimonial.Rows[i]["testimonialdes"].ToString()
                    });

                }
                model.testimonials = testimoniallist;

            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable gallery = new DataTable();
            da.Fill(gallery);
            if (gallery.Rows.Count > 0)
            {
                List<Gallery> Gallery = new List<Gallery>();
                for (int i = 0; i <= gallery.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (gallery.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + gallery.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Gallery.Add(new Gallery
                    {
                        Image = imgurl,
                        title = gallery.Rows[i]["imagedesc"].ToString(),

                    });
                }

                model.gallery = Gallery;
            }


            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT galleryid, imagedesc, imageurl, imagename FROM public.gallery_digi where customerid =" + dt.Rows[0]["customerid"].ToString() + " and isfor='DO'" + "order by galleryid desc limit 10", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable concept = new DataTable();
            da.Fill(concept);
            if (concept.Rows.Count > 0)
            {
                List<Concept> Concept = new List<Concept>();
                for (int i = 0; i <= concept.Rows.Count - 1; i++)
                {
                    string imgurl = "";
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                    {
                        imgurl = "https://digitalcard.live/" + concept.Rows[i]["imageurl"].ToString().Replace("http://dataupdate.in/", "");

                    }
                    if (concept.Rows[i]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                    {
                        imgurl = "https://imonline24.com/" + concept.Rows[i]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");

                    }
                    Concept.Add(new Concept
                    {
                        Image = imgurl,
                        title = concept.Rows[i]["imagedesc"].ToString(),

                    });

                }

                model.concept = Concept;
            }
            if (agcon.State == ConnectionState.Closed) { agcon.Open(); }
            cmd = new NpgsqlCommand("SELECT video_id, url, id, title , edate, efrom, action  FROM public.video_digi where id=" + dt.Rows[0]["customerid"].ToString() + "", agcon);
            da = new NpgsqlDataAdapter(cmd);
            DataTable video = new DataTable();
            da.Fill(video);
            if (video.Rows.Count > 0)
            {
                List<video> videos = new List<video>();
                for (int i = 0; i <= video.Rows.Count - 1; i++)
                {
                    char[] delimiters = new char[] { '=', '\\' };
                    if (video.Rows[i]["url"].ToString().Contains("=") == true)
                    {
                        string[] parts = video.Rows[i]["url"].ToString().Split(delimiters,
                                     StringSplitOptions.RemoveEmptyEntries);
                        videos.Add(new video
                        {
                            url = parts[1].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }
                    else
                    {

                        string[] parts = video.Rows[i]["url"].ToString().Split('/');
                        videos.Add(new video
                        {
                            url = parts[3].ToString(),
                            action = Convert.ToInt32(video.Rows[i]["action"].ToString()),
                            efrom = video.Rows[i]["efrom"].ToString(),
                            edate = video.Rows[i]["edate"].ToString(),
                            title = video.Rows[i]["title"].ToString(),
                        });
                    }


                }
                model.videos = videos;
            }
            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;

            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult DigitalCard22()
        {
            return View();
        }

        public ActionResult PrivacyPolicy(string customerid)
        {
            Session["url"] = "DigitalCard21?customerid=" + customerid;
            Session["customerid"] = customerid;
            combo model = new combo();
            customerinfo cust = new customerinfo();
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid, c.customername, c.personalcontact, c.emailid, c.website, c.fblink, c.designation, c.address, d.callme, d.whatsapp, d.direction, d.twitter, d.linkedin, d.imageurl, d.about, d.company, d.solution, b.firbasetoken from customermaster c join digitalcard_customer d on c.customerid = d.customerid left join devise_info b on c.customerid = b.cust_id and b.firbasetoken <> '' and b.product_code=125 where c.auth_key = '" + customerid + "' and d.isfor = 'DO'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["firbasetoken"] = dt.Rows[0]["firbasetoken"].ToString();
                cust.customername = dt.Rows[0]["customername"].ToString();
                cust.callme = dt.Rows[0]["callme"].ToString();
                cust.emailid = dt.Rows[0]["emailid"].ToString();
                cust.whatsapp = dt.Rows[0]["whatsapp"].ToString();
                if (dt.Rows[0]["website"].ToString().Contains("http://") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("http://www.") == true)
                {
                    cust.website = dt.Rows[0]["website"].ToString();
                }
                else if (dt.Rows[0]["website"].ToString().Contains("www.") == true)
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                else
                {
                    cust.website = "http://" + dt.Rows[0]["website"].ToString();
                }
                cust.fblink = dt.Rows[0]["fblink"].ToString();
                cust.twilink = dt.Rows[0]["twitter"].ToString();
                cust.linkedin = dt.Rows[0]["linkedin"].ToString();
                cust.direction = dt.Rows[0]["direction"].ToString();
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://dataupdate.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://dataupdate.in/", "");
                    cust.imageurl = "https://digitalcard.live/" + imgurl;
                }
                if (dt.Rows[0]["imageurl"].ToString().Contains("http://mobiapi.dataupload.in/") == true)
                {
                    string imgurl = dt.Rows[0]["imageurl"].ToString().Replace("http://mobiapi.dataupdate.in/", "");
                    cust.imageurl = "https://imonline24.com/" + imgurl;
                }
                cust.designation = dt.Rows[0]["designation"].ToString();
                cust.address = dt.Rows[0]["address"].ToString();
                cust.about = dt.Rows[0]["about"].ToString();
                cust.vision = dt.Rows[0]["company"].ToString();
                cust.service = dt.Rows[0]["solution"].ToString();
                Session["customerid1"] = dt.Rows[0]["customerid"].ToString();

            }
            ViewBag.id = Session["customerid"];
            List<customerinfo> custlist = new List<customerinfo>();
            custlist.Add(cust);
            model.customerinfo = custlist;
            ViewBag.id = Session["customerid1"];
            if (agcon.State == ConnectionState.Open) { agcon.Close(); }
            if (supercon.State == ConnectionState.Open) { supercon.Close(); }

            //supercon.Close();
            //agcon.Close();
            return View(model);
        }

        public ActionResult sendenquiry(string name, string email, string number, string query, string toname, string toemail)
        {
            Response.Cookies["msg"].Value = "Enquiry Send Successfully....";
            var result = sendmail(name, email, number, query, toname, toemail);
            
            return Redirect("http://digitalcard.live/Home/" + Session["url"].ToString());
        }

        public string sendgridapi()
        {
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM public.tbl_sendgrid where customerid=0 ", supercon);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["api"].ToString();
            }
            return "";
        }

        public async Task sendmail(string name,string email,string number,string query,string toname, string toemail)
        {
            try
            {
                if (Session["firbasetoken"] != null)
                {
                    if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
                    NpgsqlCommand cmd = new NpgsqlCommand("insert into tbl_cardenquiry (name, email, contact, query, toname, toemail, edate, customerid) values ('" + name + "','" + email + "','" + number + "','" + query + "','" + toname + "','" + toemail + "',current_timestamp,'" + Session["customerid1"].ToString() + "')", supercon);
                    cmd.ExecuteNonQuery();
                    string msg1 = name + " Enquired From Digital Card ";
                    areamapping(msg1);
                }


                // Sendgrid Api key goes here can be defined in the web.config
                //i have hard coded it

                string date = System.DateTime.Today.ToString("dd/MM/yyyy");

                var apiKey = sendgridapi();
                //"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";
                //creating a sendgrid client to connect to the service provider
                var client = new SendGridClient(apiKey);
                //from which email you have to send the email
                var from = new EmailAddress("noreply@zaidicorp.in", name);
                //mail subject can be dynamic as per need i have hard coded it
                var subject = "Digital Card Enquiry From " + name + "";

                //where a reply from user to be delivered
                var replyto = new EmailAddress(email);
                //recipient email id to emailid
                //can be dynamic i have hard corded
                var to = new EmailAddress(toemail, toname);
                //email content
                var plainTextContent = "";
                //var htmlContent = "<strong>This email is from SENDGRID API V3</strong> " + DateTime.UtcNow +" " + DateTime.Now.ToLongDateString();
                //var body = "<b>Name:- " + name + ",</b><br/><p>Email Id:- " + email + "</p><p>Mobile No:- " + number + "</p><p>Query:- " + query + "</p><p><br><b>Thanks & Regards,</b></p><p><b> " + name + " </b></div> ";

                // var body = "<div style='margin: 20px; border: 1px solid #dadada;box-shadow: 0px 0px 2px 2px #e2e2e2;'><div style= 'background:#00529c;'><span style='color:#fff; font-size:20px;text-align: center;margin: auto;display: block;padding:20px'>Enquiry Mail</span ></div><div style ='background:#fdc236;'><span style ='color:#000; font-size:20px;text-align: center;margin: auto;display: block;padding:20px'>" + name + " </span></div><div style='background:#ececec;padding:20px;'><span> Date: " + date + " </span><br><span> Email: " + email + " </span><br><span> Mobile No: " + number + " </span><br><span> Message: " + query + "  </span><br></div><div style ='background:#fdc236;padding:20px;'><span> Thanks & Regards, </span><br><span> " + name + " </ span ></div></div> ";


                //var body= "<link href='https://fonts.googleapis.com/css?family=Grenze:100,200,300,400,500,600,700,800,900&display=swap' rel='stylesheet'><div style ='margin: 20px;border: 1px solid #e2e2e2;box-shadow: 0px 0px 2px 2px #e4e4e4;background:#fbfbfb;'><div><img src ='http://digitalcard.live/images/enquiry.png' style ='margin: auto;text-align: center;display: block;'><img src ='http://digitalcard.live/images/clipart.png' style = 'margin: auto;text-align: center;display: block;'></div><div><div style ='padding:20px;'><span style ='font-family: Grenze;font-size:20px;'> Date:" + date + " </span><br><span style='font-family: Grenze;font-size:20px;'> Email: " + email +"  </span><br><span style='font-family: Grenze;font-size:20px;'> Mobile No: " + number + " </span><br><span style='font-family: Grenze;font-size:20px;'> Message: " + query+ " </span><br><br><span style ='font-family: Grenze;font-size:20px;'> Thanks & Regards, </span><br><span style ='font-family: Grenze;font-size:20px;'> " + name + " </span></div></div>";

                var body = "<table border='0' cellpadding='0' cellspacing='0' width='100%' style='width:400px;text-align:center;margin:auto;'><tr><td style='text-align:left;'><table align='center' border='0' cellpadding='0' cellspacing='0' width='600' style='border:1px solid #cccccc;border-collapse:collapse;width:400px;text-align:center;margin:auto;'><tr style ='background:#fdc236;'><td style='text-align:center;padding-top:20px;font-size:20px'>Enquiry Message</td></tr><tr><td><div id='container' style='overflow:hidden;background:#e8e2e4;'><div id='triangle-topleft' style='width:0;height:0;border-top:35px solid #fdc236;border-right:900px solid #e8e2e4;'></div></div></td></tr><tr><th style='background:#e8e2e4;'><br><img src='http://digitalcard.live/images/Enquiry.jpg' class='enquiry_image' style='text-align:center;margin:auto;width:30%;'><br></th></tr><tr style ='background:#e8e2e4;'><td style='text-align:left;'><table style='border: 1px solid #d2d2d2;padding:15px;margin-bottom:50px;width:450px;text-align:center;margin:auto;position:relative;'><tr><td colspan = '2' class='enquiry' style='text-align:center;margin-top:-33px;position:absolute;left:43.5%;width:195px;background:#e0e0e0;font-size:22px;padding:0px;'>Enquiry Details</td></tr><tr><td style='text-align:left;font-size:18px;'>Date:-</td><td style='text-align:left;font-size:18px;color:#000;'>" + date + "</td></tr><tr><td style='text-align:left;font-size:18px;'>Email:-</td><td style='text-align:left;font-size:18px;color:#000;'>" + email + "</td></tr><tr><td style='text-align:left;font-size:18px;'> Mobile No:-</td><td style='text-align:left;font-size:18px;color:#000;'>" + number + "</td></tr><tr><td style='text-align:left;font-size:18px;'> Message:- </td><td style='text-align:left;font-size:18px;color:#000;'>" + query + "</td></tr></table></td></tr><tr><td style ='text-align:left;'><div id='container1' style='overflow:hidden;background:#e8e2e4;'><div id='triangle-topleft1' style='width:0;height:0;border-bottom:40px solid #fdc236;border-right: 900px solid #e8e2e4;'></div></div></td></tr><tr><td style='text-align:center;background:#fdc236;padding-bottom:20px;font-size:18px;'>Thanks & Regards,<br/>" + name + "</td></tr></table></td></tr></table>";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, body);
                msg.ReplyTo = replyto;
                //readig response from the api not implement complete
                Response r = await client.SendEmailAsync(msg);

            }
            catch (Exception ex)
            {

                Response.Cookies["msg"].Value= "Somathing Wrong!!!!";
            }
           
           
        }

        public async Task areamapping(string msg)
        {
            string id = "";
            //string start = HttpContext.Session["start"].ToString();
            // string query = HttpContext.Session["query"].ToString();
            string applicationID = "AAAA2ELh4MY:APA91bHpjaZRGqWN2xXFWpBONK6FKC1jPHmcxbrCjQ_aiYWIuv-TwMbWbpRErX6GR9Qyfd6-9fx7PZb7xErIvwxdrTFCB77-NakAvzh8i8Ay7MmnCUX9OSyvxm-0cjHkng6_XgYurY3i"; //"AAAAWcRxpmI:APA91bE9jJtdDmyUIrEN4ysXu1QygtioOm0mbOkH-yDtcwSEDhIA104Q22bdXp-jnks7rsqaz78ZImKJolCq6NnWHq0TxA5DRwyL8G-aD0BQxRlmqhtW9SPpWaiPWfiyQO8gNQE_UAZU"; //"AAAAWcRxpmI:APA91bGjPunNYmuh0dIjfUOjZfBUn7rI46iSE37vGV5dsFto3pCjyWhEONfmfwjxxIRdnGt27SUdB36LO8HioRl4p8mpqfDeOnmdGGVUQ3kPdzD-Rs9ql_-rcfZej8DqsycAf94iNzTA";
            //AAAA2ELh4MY:APA91bHpjaZRGqWN2xXFWpBONK6FKC1jPHmcxbrCjQ_aiYWIuv-TwMbWbpRErX6GR9Qyfd6-9fx7PZb7xErIvwxdrTFCB77-NakAvzh8i8Ay7MmnCUX9OSyvxm-0cjHkng6_XgYurY3i
            string senderId = "928835035334"; //"385547871842";

            string FireBaseToken = Session["firbasetoken"].ToString();


            FirebasePushNotification(applicationID, senderId, "Enquiry From Digital Card", msg, "", FireBaseToken, "Open", "");


        }

        public void FirebasePushNotification(string applicationID, string senderId, string Title, string Message, string url, string FireBaseToken, string action, string hyperlink)
        {

            try
            {

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var data = new
                {
                    to = FireBaseToken,
                    data = new
                    {
                        title = Title,
                        is_background = true,
                        message = Message,
                        Url = url,
                        button = action,
                        Hyperlink = hyperlink

                    }
                };


                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();

                                //Label3.Text = sResponseFromServer;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult clearcookies()
        {
            Response.Cookies["msg"].Expires = DateTime.Now.AddDays(-1);
            return Redirect("http://digitalcard.live/Home/" + Session["url"].ToString());
        }

        public ActionResult savecontact()
        {
            if (supercon.State == ConnectionState.Closed) { supercon.Open(); }
            NpgsqlCommand cmd = new NpgsqlCommand("select c.customerid,c.customername,c.personalcontact,c.emailid,c.website,c.fblink,c.designation,c.address,d.callme,d.whatsapp,d.direction,d.twitter,d.linkedin,d.imageurl from customermaster c join digitalcard_customer d on c.customerid=d.customerid where c.auth_key='" + Session["customerid"].ToString() + "'", supercon);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            var filename2 = "" + dt.Rows[0]["customername"].ToString() + ".vcf";
           

            StringBuilder sw = new StringBuilder();
            sw.AppendLine("BEGIN:VCARD");
            sw.AppendLine("VERSION:3.0");
            sw.AppendLine("N:" + dt.Rows[0]["customername"].ToString() + ";;;");
            sw.AppendLine("FN:" + dt.Rows[0]["customername"].ToString());
            sw.AppendLine("ORG:" + dt.Rows[0]["emailid"].ToString());
            sw.AppendLine("EMAIL;TYPE=work:" + dt.Rows[0]["emailid"].ToString());
            sw.AppendLine("TEL;TYPE=work:" + dt.Rows[0]["personalcontact"].ToString());
      
          //  sw.AppendLine("TEL;TYPE=VOICE,CELL;VALUE=text:" + dt.Rows[0]["personalcontact"].ToString());
            sw.AppendLine("TITLE:" + dt.Rows[0]["customername"].ToString());
            sw.AppendLine("END:VCARD");
            Response.Clear();
            Response.ContentType = "text/x-vcard";
            Response.AddHeader("content-disposition", "inline; filename=" + filename2);
            Response.Write(sw.ToString());
            Response.Flush();
            Response.End();
            Response.Clear();
            return Redirect("http://digitalcard.live/Home/" + Session["url"].ToString());
        }
    }
}
