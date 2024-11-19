using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class User
    {
        public int                  userID             { get; set; }
        public string               userName           { get; set; }
        public string               password           { get; set; }
        public string               email              { get; set; }
        public DateTime             birthDate          { get; set; } 
        public bool                 isAdmin            { get; set; }
        public double               deposit            { get; set; }
        public bool?                isActive           { get; set; }
        public List<Reservation>    reservations       = new List<Reservation>();
        public List<Posting>        postings           = new List<Posting>();
        public List<Film>           insertedFilms      = new List<Film>();
        public List<Operation>      operations         = new List<Operation>();
    }
}
