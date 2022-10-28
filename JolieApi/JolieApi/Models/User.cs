using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JolieApi.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }
        [StringLength(255, MinimumLength = 3)]
        public string email { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string password { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string first_name { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string last_name { get; set; }
        public DateTime date_of_birth { get; set; }
        public Gender gender { get; set; }
        [Required]
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        male, female, other
    }
}

