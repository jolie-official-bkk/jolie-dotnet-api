namespace JolieApi.ViewModels
{
    public class PlaceOrderRequest
    {
        public int user_id { get; set; }
        public string natural_hair_type { get; set; }
        public string hair_structure { get; set; }
        public string scalp_moisture { get; set; }
        public List<string> hair_treat { get; set; }
        public List<string> hair_goal { get; set; }
        public List<string> formula { get; set; }
        public string color { get; set; }
        public string scent { get; set; }
        public string shampoo_name { get; set; }
    }
}
