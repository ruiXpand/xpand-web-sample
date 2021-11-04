using System;
using System.ComponentModel.DataAnnotations;

namespace XpandDEVWebCourse.Data
{
    public class Cars
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int NrBolts { get; set; }
        public int ExternalId { get; set; }
    }
}
