using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WolServer.Models
{
    public class WolModel
    {
        [Required]
        public string Host { get; set; }
        [Required]
        [Range(1, 65535)]
        public ushort Port { get; set; }
        [Required]
        [RegularExpression("^(((([0-9A-Fa-f]{2})(-[0-9A-Fa-f]{2}){5}))|([0-9A-Fa-f]{12})|((([0-9A-Fa-f]{2})(:[0-9A-Fa-f]{2}){5})))$", ErrorMessage = "Invalid MacAddress")]
        public string MacAddr { get; set; }
    }
}
