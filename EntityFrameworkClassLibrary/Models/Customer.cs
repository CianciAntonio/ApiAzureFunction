﻿namespace EntityFrameworkClassLibrary.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public virtual List<Invoice> Invoices { get; set; }
    }
}