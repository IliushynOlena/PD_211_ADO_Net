using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _07_IntroToEF.Entities
{
    public class Flight
    {
        //Primary key naming : Id/id/ID / EntityName+Id = FlightId
        [Key]//Primary key
        public int Number { get; set; }
        [Required, MaxLength(100)]
        public string DepartureCity { get; set; }
        [Required, MaxLength(100)]
        public string ArrivalCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }


        //Navigation properties
        //Relational type : One to Many (1....*)
        //Foreign key naming : RelatedEntityName + RelatedEntityPrimaryKeyName
        public int AirplaneId { get; set; }//foreign key
        public Airplane Airplane { get; set; }//null
        //Relational type : Many to Many (*....*)

          //Navigation properties
        public ICollection<Client> Clients { get; set; }

    }
}
