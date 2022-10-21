using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace G3N8LE_ADT_2022_23_1.Models
{
    [Table("classes")]
    public class Classes
    {
        public Classes()
        {
            this.ConnectorReservationsServices = new HashSet<ReservationsServices>();

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Range(1, 10)]
        public int Grading { get; set; }

        [Required]
        public int Price { get; set; }



        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ReservationsServices> ConnectorReservationsServices { get; }

        public override string ToString()
        {
            return $"\n{this.Id,3} | {this.Grading,2}/10 {this.Price,7} USD \t{this.Name,-1}";
        }
    }
}
