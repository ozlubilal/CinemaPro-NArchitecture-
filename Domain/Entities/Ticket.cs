using Core.Persistence.Repositories;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Domain.Entities
{
    public class Ticket : Entity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid FilmSessionId { get; set; }
        public decimal Price { get; set; }

        // JSON formatına dönüştürülmüş koltuk numaraları
        public string SelectedSeatsJson { get; set; }

        // Veritabanına eşlenmeyecek, JSON'dan dönüştürülmüş koltuk numaraları
        [NotMapped]
        public List<string> SelectedSeats
        {
            get { return string.IsNullOrEmpty(SelectedSeatsJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(SelectedSeatsJson); }
            set { SelectedSeatsJson = JsonSerializer.Serialize(value); }
        }

        public virtual FilmSession FilmSession { get; set; }

        public Ticket()
        {
        }

        public Ticket(string firstName, string lastName, string phoneNumber, Guid filmSessionId, List<string> seatNumber, decimal price)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            FilmSessionId = filmSessionId;
            SelectedSeats = seatNumber;
            Price = price;
        }
    }
}
