using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace G3N8LE_ADT_2022_23_1.Models
{  
    
    [Table("teachers")]
    public class Teachers
    {
        public Teachers()
        {
            this.Reservations = new HashSet<Reservations>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [MaxLength(50)]
        [Required]
        public string Name { get; set; }


        [MaxLength(100)]
        [Required]
        public string Branch { get; set; }


        [Required]
        public int Duration { get; set; }



        [Required]
        public int Price { get; set; }


        public override string ToString()
        {
            return $"\n{this.Id,3} |  {this.Duration} course hours {this.Price,10} USD {this.Branch,10}\t {this.Name,-1}";
        }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Reservations> Reservations { get; }
    }
}
