using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZaidiCorp.Models
{
    public class ZaidiCorp_
    {
        public List<slider> slider { set; get; }
        public Aboutus aboutus { set; get; }
        public List<products> products { set; get; }
        public List<testimonials> testimonials { set; get; }
        public List<Team> team { set; get; }
        public List<Training> training { set; get; }

        public Contactus contact { set; get; }

        public List<Gallery> gallery { set; get; }
        public List<whatsapp> whatsapp { set; get; }


    }
    public class slider
    {
        public string slider_img { set; get; }

    }

    public class Aboutus
    {
        public string img { set; get; }
        public string description { set; get; }
    }

    public class products
    {
        public string primary_img { set; get; }
        public string secondary_img { set; get; }
        public string title { set; get; }
        public string price { set; get; }
        public string gst { set; get; }
        public string total { set; get; }
        public int id { get; set; }
    }

    public class testimonials
    {
        public string name { set; get; }
        public string image { set; get; }
        public string description { set; get; }
    }

    public class Team
    {

        public string img { set; get; }

        public string name { set; get; }
        public string designation { set; get; }
        public string department { set; get; }
        public string area { set; get; }
        public string number { set; get; }
        public string emailId { set; get; }
    }

    public class Training
    {
        public int id { get; set; }
        public string primary_img { set; get; }
        public string secondary_img { set; get; }
        public string title { set; get; }
        public string price { set; get; }
        public string gst { set; get; }
        public string total { set; get; }
    }

  

    public class Contactus
    {
        public string Email_id { get; set; }
        public string Address { get; set; }
        public string mobile1 { get; set; }
        public string mobile2 { get; set; }
    }

    public class Gallery
    {
        public string title { set; get; }
        public string Image { set; get; }
    }

    public class Concept
    {
        public string title { set; get; }
        public string Image { set; get; }
    }

    public class customerinfo
    {
        public int customerid { set; get; }
        public string personalcontact { set; get; }
        public string emailid { set; get; }
        public string customername { get; set; }
        public string cityname { get; set; }
        public string statename { get; set; }
        public string pincode { get; set; }
        public string callme { get; set; }
        public string whatsapp { get; set; }
        public string direction { get; set; }
        public string designation { get; set; }
        public string about { get; set; }
        public string fblink { get; set; }
        public string twilink { get; set; }
        public string linkedin { get; set; }
        public string imageurl { get; set; }
        public string website { get; set; }
        public string vision { get; set; }
        public string service { get; set; }
        public string address { get; set; }
    }

    public class video
    {
        public int action { set; get; }
        public string url { set; get; }
        public string efrom { set; get; }
        public string edate { set; get; }
        public string title { get; set; }

    }

    public class combo
    {
        public List<customerinfo> customerinfo { get; set; }
        public List<testimonials> testimonials { get; set; }
        public List<Gallery> gallery { get; set; }
        public List<Concept> concept { get; set; }
        public List<video> videos { get; set; }
    }
}