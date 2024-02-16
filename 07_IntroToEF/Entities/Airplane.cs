using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _07_IntroToEF.Entities
{
    public class Airplane
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Model { get; set; }
        public int MaxPassanger { get; set; }

        //Navigation properties

        //Relational type : Many to Many (*....*)
        public ICollection<Flight> Flights { get; set; }
    }
}
