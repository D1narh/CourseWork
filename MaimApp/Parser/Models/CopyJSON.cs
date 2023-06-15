using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Parser.Models
{
    public class Rootobject
    {
        public string jsonrpc { get; set; }
        public Response response { get; set; }
        public object[] errors { get; set; }
        public object[] messages { get; set; }
        public string hash { get; set; }
        public string trace { get; set; }
    }

    public class Response
    {
        public City[] cities { get; set; }
        public Hotel[] hotels { get; set; }
        public object[] additional_hotel_ids { get; set; }
        public bool isMember { get; set; }
    }

    public class City
    {
        public string id { get; set; }
        public string name { get; set; }
        public int country_id { get; set; }
        public string coords_ya { get; set; }
        public string url { get; set; }
        public string genitive { get; set; }
        public string distance { get; set; }
        public string country_name { get; set; }
        public string country_url { get; set; }
        public int hotels_num { get; set; }
        public float[] coords { get; set; }
        public int?[] types { get; set; }
        public string local_time { get; set; }
    }

    public class Hotel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public string address { get; set; }
        public Times times { get; set; }
        public int type_id { get; set; }
        public int category_id { get; set; }
        public int[] type_ids { get; set; }
        public int[] category_ids { get; set; }
        public int city_id { get; set; }
        public int region_id { get; set; }
        public string city_name { get; set; }
        public string city_url { get; set; }
        public int country_id { get; set; }
        public string country_url { get; set; }
        public string country_name { get; set; }
        public int district_id { get; set; }
        public int?[] district_ids { get; set; }
        public int stars { get; set; }
        public int breakfast { get; set; }
        public int breakfast_price { get; set; }
        public string url { get; set; }
        public string alt_name { get; set; }
        public int leader { get; set; }
        public int partner { get; set; }
        public int top { get; set; }
        public int partner_by_fee { get; set; }
        public int partner_by_adv { get; set; }
        public int adv { get; set; }
        public int weight { get; set; }
        public int supplier { get; set; }
        public int sales_closed { get; set; }
        public float min_price { get; set; }
        public int position { get; set; }
        public string coords_ya { get; set; }
        public int rooms_num { get; set; }
        public float rating { get; set; }
        public int number_reviews { get; set; }
        public Reviews_Summary reviews_summary { get; set; }
        public string currency { get; set; }
        public int bookable { get; set; }
        public int _readonly { get; set; }
        public int show_phones { get; set; }
        public int opening_date { get; set; }
        public int overhaul_date { get; set; }
        public int opening_or_overhaul_date { get; set; }
        public float[] coords { get; set; }
        public float center_distance { get; set; }
        public string panorama_position { get; set; }
        public int chain_id { get; set; }
        public int state_hotel { get; set; }
        public object[] virtual_tours { get; set; }
        public Image image { get; set; }
        public Image1[] images { get; set; }
        public object facilities { get; set; }
        public object distances { get; set; }
        public int?[] services { get; set; }
        public int?[] service_categories { get; set; }
        public int sea_views { get; set; }
        public int?[] rooms { get; set; }
        public object rooms_amenities { get; set; }
        public int has_rooms_with_bathroom { get; set; }
        public int has_placements_with_breakfast_buffet { get; set; }
        public int payment { get; set; }
        public int pets { get; set; }
        public string[] phones { get; set; }
        public int pageviews { get; set; }
        public int distance_to_sea { get; set; }
        public object sea_distances { get; set; }
        public string[] languages { get; set; }
        public float original_rating { get; set; }
        public Min_Price_Data min_price_data { get; set; }
        public object rel_owner { get; set; }
        public int views { get; set; }
        public Displayed_Phones displayed_phones { get; set; }
    }

    public class Times
    {
        public string in_from { get; set; }
        public string in_to { get; set; }
        public string out_from { get; set; }
        public string out_to { get; set; }
    }

    public class Reviews_Summary
    {
        public int number_reviews { get; set; }
        public int number_scores { get; set; }
        public float rating { get; set; }
        public float quality_of_sleep { get; set; }
        public float location { get; set; }
        public float service { get; set; }
        public float value_for_money { get; set; }
        public float cleanness { get; set; }
        public float meal { get; set; }
    }

    public class Image
    {
        public string path { get; set; }
        public string thumb_path { get; set; }
        public string preview_path { get; set; }
        public string mobile_path { get; set; }
        public string mobile_preview_path { get; set; }
    }

    public class Min_Price_Data
    {
        public int hotel_id { get; set; }
        public float price { get; set; }
        public string currency { get; set; }
        public string _in { get; set; }
        public string _out { get; set; }
        public int has_free { get; set; }
        public string updated { get; set; }
        public string current_price_date { get; set; }
        public int room_id { get; set; }
        public string placement_id { get; set; }
    }

    public class Displayed_Phones
    {
        public int display_type { get; set; }
        public string[] numbers { get; set; }
    }

    public class Image1
    {
        public string path { get; set; }
        public string thumb_path { get; set; }
        public string preview_path { get; set; }
        public string type { get; set; }
        public int?[] type_ids { get; set; }
    }

}
