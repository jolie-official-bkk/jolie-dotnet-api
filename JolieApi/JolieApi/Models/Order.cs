using System;
using System.ComponentModel.DataAnnotations;

namespace JolieApi.Models
{
    public class Order
    {
        [Key]
        public int order_id { get; set; }
        [Required]
        public int user_id { get; set; }
        [StringLength(255, MinimumLength = 3)]
        public string natural_hair_type { get; set; }
        [StringLength(255, MinimumLength = 3)]
        public string hair_structure { get; set; }
        [StringLength(255, MinimumLength = 3)]
        public string scalp_moisture { get; set; }
        public List<string> hair_treat { get; set; }
        public List<string> hair_goal { get; set; }
        public List<string> formula { get; set; }
        [StringLength(9, MinimumLength = 6)]
        public string color { get; set; }
        [StringLength(255, MinimumLength = 3)]
        public string scent { get; set; }
        [StringLength(255, MinimumLength = 3)]
        public string shampoo_name { get; set; }
        public DateTime created_at { get; set; }
    }
}

