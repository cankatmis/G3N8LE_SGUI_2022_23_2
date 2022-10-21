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
    [Table("connectorTable")]
    public class ReservationsServices
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [ForeignKey(nameof(Reservations))]
        public int? ReservationId { get; set; }


        [NotMapped]
        [JsonIgnore]
        public virtual Reservations Reservations { get; set; }


        [ForeignKey(nameof(Classes))]
        public int? ClassId { get; set; }



        [NotMapped]
        [JsonIgnore]
        public virtual Classes Classes { get; set; }
        public override string ToString()
        {
            return $"{this.Id,3} | {this.ReservationId,5}\t {this.ClassId,10}";
        }
    }
}
