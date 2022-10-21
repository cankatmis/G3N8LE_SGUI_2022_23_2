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
    [Table("reservations")]
    public class Reservations
    {
        public Reservations()
        {
            this.ConnectorReservationsServices = new HashSet<ReservationsServices>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        public DateTime DateTime { get; set; }


        [NotMapped]
        [JsonIgnore]
        public virtual Students Student { get; set; }


        [ForeignKey(nameof(Student))]
        public int? StudentId { get; set; }



        [NotMapped]
        [JsonIgnore]
        public virtual Teachers Teacher { get; set; }


        [ForeignKey(nameof(Teacher))]
        public int? TeacherId { get; set; }



        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ReservationsServices> ConnectorReservationsServices { get; }


        public override string ToString()
        {
            return $"\n{this.Id,3} | {this.StudentId,-20} {this.DateTime.Year,10}.{this.DateTime.Month}.{this.DateTime.Day} \t{this.TeacherId,15}";
        }
    }
}
