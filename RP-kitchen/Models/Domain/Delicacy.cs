﻿namespace RP_kitchen.Models.Domain
{
    public class Delicacy
    {
        public Guid Id { get; set; }
        public string Catagory { get; set; }
        public string Name { get; set; }
        public  string? Picture { get; set; }
        public  DateTime Date { get; set; }
        public  double Price { get; set; }  

    }
}
