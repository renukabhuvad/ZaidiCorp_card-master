using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using ZaidiCorp.Models;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.RegularExpressions;
using Dapper;

namespace ZaidiCorp.DBService
{
    public  class DB_Connection
    {
        public static IDbConnection OpenConnection => connection();
        public static IDbConnection adminConnection => superconnection();
        protected static NpgsqlConnection  connection()
        {
            NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["addnewmail"].ToString());
            return con;
        }
        protected static NpgsqlConnection superconnection()
        {
            NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["superadmin"].ToString());
            return con;
        }
    }

    public class slider_: DB_Connection
    {
        public static List<slider> slider_img()
        {
            var con = connection();DataTable dt_slider = new DataTable();
            NpgsqlCommand cmd_slider = new NpgsqlCommand("SELECT image_path FROM public.content_upload where   category='7' and status=1 and cust_id=" + 1003 + " ", con);

            try
            {
                con.Open();
                NpgsqlDataAdapter da_slider = new NpgsqlDataAdapter(cmd_slider);
                con.Close();
                da_slider.Fill(dt_slider);
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            var slider_data = new List<slider>();
            if(dt_slider.Rows.Count>=1)
            {
                for(int k=0;k<=dt_slider.Rows.Count-1;k++)
                {
                    slider_data.Add(new slider
                    {
                        slider_img = dt_slider.Rows[k]["image_path"].ToString(),
                     });
                   
                }
            }
            return slider_data;
        }
    }

    public class Aboutus_ : DB_Connection
    {
        public static Aboutus Aboutus()
        {
            var con = connection(); DataTable dt_aboutus = new DataTable(); int i = 0;
            NpgsqlCommand cmd_aboutus = new NpgsqlCommand("SELECT  image_path, description FROM public.content_upload where   category='4' and status=1 and cust_id=" + 1003 + " ", con);

            try
            {
                con.Open();
                NpgsqlDataAdapter da_aboutus = new NpgsqlDataAdapter(cmd_aboutus);
                con.Close();
                da_aboutus.Fill(dt_aboutus);
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            var Aboutus_data = new Aboutus();
            if (dt_aboutus.Rows.Count >= 1)
            {
                for (int k = 0; k <= dt_aboutus.Rows.Count - 1; k++)
                {
                    Aboutus_data.img = dt_aboutus.Rows[k]["image_path"].ToString();
                    Aboutus_data.description = dt_aboutus.Rows[k]["description"].ToString();
                }
            }
            return Aboutus_data;
        }
    }

    public class Contactus_ : DB_Connection
    {
        public static Contactus contactus()
        {
            var con = DB_Connection.connection();DataTable dt_contact = new DataTable();
            NpgsqlCommand cmd_contact = new NpgsqlCommand("SELECT email_id, phone1,  address  FROM public.contactus where cust_id=" + 1003 + "  ;", con);
            try
            {
                con.Open();
                NpgsqlDataAdapter da_contact = new NpgsqlDataAdapter(cmd_contact);
                con.Close();
                da_contact.Fill(dt_contact);
            }
            catch(Exception ex) { }
            finally
            {
                con.Close();
            }
            var contactus = new Contactus();
            if (dt_contact.Rows.Count>=1)
            {
                for(int k=0;k<=dt_contact.Rows.Count-1;k++)
                {
                    contactus.Email_id = dt_contact.Rows[k]["email_id"].ToString();
                    contactus.mobile1 = dt_contact.Rows[k]["phone1"].ToString();
                    contactus.Address = dt_contact.Rows[k]["address"].ToString();
                }
            }
            return contactus;
        }
    }

    public class Contactsus_ : DB_Connection
    {
        public static Contactus contactsus()
        {
            var con = DB_Connection.connection(); DataTable dt_contact = new DataTable();
            NpgsqlCommand cmd_contact = new NpgsqlCommand("insert into public.contactus ");
            //NpgsqlCommand cmd_contact = new NpgsqlCommand("SELECT email_id, phone1,  address  FROM public.contactus where cust_id=" + 1003 + "  ;", con);
            try
            {
                con.Open();
                NpgsqlDataAdapter da_contact = new NpgsqlDataAdapter(cmd_contact);
                con.Close();
                da_contact.Fill(dt_contact);
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            var contactus = new Contactus();
            if (dt_contact.Rows.Count >= 1)
            {
                for (int k = 0; k <= dt_contact.Rows.Count - 1; k++)
                {
                    contactus.Email_id = dt_contact.Rows[k]["email_id"].ToString();
                    contactus.mobile1 = dt_contact.Rows[k]["phone1"].ToString();
                    contactus.Address = dt_contact.Rows[k]["address"].ToString();
                }
            }
            return contactus;
        }
    }

    public class IndexProducts_ : DB_Connection
    {
        public static List<products> IndexProducts()
        {
            var con = connection(); DataTable dt_products = new DataTable();
            NpgsqlCommand cmd_products = new NpgsqlCommand("SELECT id, primage_img, secondary_img, title,  price, gst, total  FROM public.products where cust_id=" + 1003 + " and status=1 and category='P' limit 3 ", con);

            try
            {
                con.Open();
                NpgsqlDataAdapter da_products = new NpgsqlDataAdapter(cmd_products);
                con.Close();
                da_products.Fill(dt_products);
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            var products_data = new List<products>();
            if (dt_products.Rows.Count >= 1)
            {
                for (int k = 0; k <= dt_products.Rows.Count - 1; k++)
                {
                    products_data.Add(new products
                    {
                        primary_img = dt_products.Rows[k]["primage_img"].ToString(),
                        secondary_img = dt_products.Rows[k]["secondary_img"].ToString(),
                    });

                }
            }
            return products_data;
        }
    }

    public class Products_: DB_Connection
    {
        public static List<products> Products()
        {
            var con = connection(); DataTable dt_products = new DataTable();
            NpgsqlCommand cmd_products = new NpgsqlCommand("SELECT id, primage_img, secondary_img, title,  price, gst, total  FROM public.products where cust_id=" + 1003 + " and status=1 and category='P' ", con);

            try
            {
                con.Open();
                NpgsqlDataAdapter da_products = new NpgsqlDataAdapter(cmd_products);
                con.Close();
                da_products.Fill(dt_products);
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            var products_data = new List<products>();
            if (dt_products.Rows.Count >= 1)
            {
                for (int k = 0; k <= dt_products.Rows.Count - 1; k++)
                {
                    products_data.Add(new products
                    {
                        id = Convert.ToInt32(dt_products.Rows[k]["id"].ToString()),
                        title = dt_products.Rows[k]["title"].ToString(),
                        gst = dt_products.Rows[k]["gst"].ToString(),
                        primary_img = dt_products.Rows[k]["primage_img"].ToString(),
                        secondary_img = dt_products.Rows[k]["secondary_img"].ToString(),
                        price=dt_products.Rows[k]["price"].ToString(),
                        total= dt_products.Rows[k]["total"].ToString(),

                    });
                   
                }
            }
            return products_data;
        }
    }

    public class  Testimonials_: DB_Connection
    {
        public static List<testimonials> testimonials()
        {
            var con = connection();
            DataTable dt_testimonial = new DataTable();

            NpgsqlCommand cmd_testimonial = new NpgsqlCommand("SELECT  name, image_path, description FROM public.content_upload where category='3' and status=1 and cust_id=" + 1003 + " ", con);

            try
            {
                con.Open();
                NpgsqlDataAdapter da_testimonial = new NpgsqlDataAdapter(cmd_testimonial);
                con.Close();
                da_testimonial.Fill(dt_testimonial);
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            var testimonials_data = new List<testimonials>();
            if (dt_testimonial.Rows.Count >= 0)
            {
                for (int k = 0; k <= dt_testimonial.Rows.Count - 1; k++)
                {
                    testimonials_data.Add(new testimonials
                    {
                    name = dt_testimonial.Rows[k]["name"].ToString(),
                    image = dt_testimonial.Rows[k]["image_path"].ToString(),
                    description = dt_testimonial.Rows[k]["description"].ToString(),
                });
  
                }
            }
            return testimonials_data;
        }
    }

    public class Team_ : DB_Connection
    {
        public static List<Team> Team()
        {
            var con = connection(); DataTable dt_grid = new DataTable();
            NpgsqlCommand cmd_grid = new NpgsqlCommand("SELECT  name, mobile, emailid, designation, department,area, description, image FROM public.team where custid=" + 1003 + " and active_status=1 and websiteview=1 ", con);
            try
            {
                con.Open();
                NpgsqlDataAdapter da_grid = new NpgsqlDataAdapter(cmd_grid);
                con.Close();
                da_grid.Fill(dt_grid);
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            var team = new List<Team>();
            if (dt_grid.Rows.Count > 0)
            {
                for (int k = 0; k <= dt_grid.Rows.Count - 1; k++)
                {

                    team.Add(new Team
                    {
                        name = dt_grid.Rows[k]["name"].ToString(),
                        number = dt_grid.Rows[k]["mobile"].ToString(),
                        emailId = dt_grid.Rows[k]["emailid"].ToString(),
                        department = dt_grid.Rows[k]["department"].ToString(),
                        designation = dt_grid.Rows[k]["designation"].ToString(),
                        area = dt_grid.Rows[k]["area"].ToString(),
                        img = dt_grid.Rows[k]["image"].ToString(),
                });
                }
            }
            return team;
        }

    }

    public class IndexTraining_ : DB_Connection
    {
        public static List<Training> IndexTraining()
        {
            var con = connection(); DataTable dt_program = new DataTable();
            NpgsqlCommand cmd_grid = new NpgsqlCommand("SELECT id, primage_img, secondary_img, title, price, gst, total  FROM public.products where cust_id=" + 1003 + " and status=1 and category='T' limit 3 ", con);
            try
            {
                con.Open();
                NpgsqlDataAdapter da_grid = new NpgsqlDataAdapter(cmd_grid);
                con.Close();
                da_grid.Fill(dt_program);
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            var program = new List<Training>();
            if (dt_program.Rows.Count > 0)
            {
                for (int k = 0; k <= dt_program.Rows.Count - 1; k++)
                {

                    program.Add(new Training
                    {
                        id = Convert.ToInt32(dt_program.Rows[k]["id"].ToString()),
                        primary_img = dt_program.Rows[k]["primage_img"].ToString(),
                        secondary_img = dt_program.Rows[k]["secondary_img"].ToString(),
                        title = dt_program.Rows[k]["title"].ToString(),
                        price = dt_program.Rows[k]["price"].ToString(),
                        gst = dt_program.Rows[k]["gst"].ToString(),
                        total = dt_program.Rows[k]["total"].ToString(),

                    });
                }
            }
            return program;
        }
    }


    public class Training_ : DB_Connection
    {
        public static List<Training> Training()
        {
            var con = connection(); DataTable dt_program = new DataTable();
            NpgsqlCommand cmd_grid = new NpgsqlCommand("SELECT id, primage_img, secondary_img, title, price, gst, total  FROM public.products where cust_id=" + 1003 + " and status=1 and category='T' ", con);
            try
            {
                con.Open();
                NpgsqlDataAdapter da_grid = new NpgsqlDataAdapter(cmd_grid);
                con.Close();
                da_grid.Fill(dt_program);
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            var program = new List<Training>();
            if (dt_program.Rows.Count > 0)
            {
                for (int k = 0; k <= dt_program.Rows.Count - 1; k++)
                {

                    program.Add(new Training
                    {
                        id= Convert.ToInt32(dt_program.Rows[k]["id"].ToString()),
                        primary_img = dt_program.Rows[k]["primage_img"].ToString(),
                        secondary_img = dt_program.Rows[k]["secondary_img"].ToString(),
                        title = dt_program.Rows[k]["title"].ToString(),
                        price = dt_program.Rows[k]["price"].ToString(),
                        gst = dt_program.Rows[k]["gst"].ToString(),
                        total = dt_program.Rows[k]["total"].ToString(),
                       
                    });
                }
            }
            return program;
        }
    }
    
    public class Gallery_ : DB_Connection
    {
        public static List<Gallery> gallery()
        {
            var con = DB_Connection.connection();
            DataTable dt_gallery = new DataTable();
            NpgsqlCommand cmd_grid = new NpgsqlCommand("SELECT  name, image_path FROM public.content_upload where  category ='1' and status=1 and cust_id=" + 1003 + " ", con);

            try
            {
                con.Open();
                NpgsqlDataAdapter da_gallery = new NpgsqlDataAdapter(cmd_grid);
                con.Close();
                da_gallery.Fill(dt_gallery);
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            var grid_gallery = new List<Gallery>();
            if (dt_gallery.Rows.Count >= 0)
            {
                for (int k = 0; k <= dt_gallery.Rows.Count - 1; k++)
                {
                    grid_gallery.Add(new Gallery()
                    {

                        
                        title = dt_gallery.Rows[k]["name"].ToString(),
                   
                        Image = dt_gallery.Rows[k]["image_path"].ToString(),

                    });
                }
            }
            return grid_gallery;
        }
    }



    public class whatsappdata_ : DB_Connection
    {
        public IEnumerable<whatsapp> Getwhatsapp()
        {           
             return DB_Connection.OpenConnection.Query<whatsapp>(@"SELECT  name, mobile, emailid, designation, department,area, description, image FROM public.team where custid=" + 1003 + " and active_status=1 and whatasapp_view=1");
            
        }
    }
 }