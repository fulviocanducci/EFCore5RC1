using System;

namespace EFCore5RC1.Models
{
    public class Animal : IModified
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }


    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Local { get; set; }
        public Address Work { get; set; }
    }
    public class Address
    {
        public string Number { get; set; }
        public string Street { get; set; }
    }
}
