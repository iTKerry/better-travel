using System;
using BetterExtensions.Domain.Base;
using BetterTravel.DataAccess.Abstractions.Entities.Enumerations;
using BetterTravel.DataAccess.Abstractions.Enums;
using BetterTravel.DataAccess.Abstractions.Events;
using BetterTravel.DataAccess.Abstractions.ValueObjects;

namespace BetterTravel.DataAccess.Abstractions.Entities
{
    public class HotTour : AggregateRoot
    {
        protected HotTour()
        {
        }
        
        public HotTour(
            HotTourInfo info,
            HotelCategoryType hotelCategory,
            Duration duration, 
            Price price, 
            Country country, 
            Resort resort,
            DepartureLocation departureLocation, 
            DateTime departureDate)
        {
            Info = info;
            HotelCategory = hotelCategory;
            Duration = duration;
            Price = price;
            Country = country;
            Resort = resort;
            DepartureLocation = departureLocation;
            DepartureDate = departureDate;
        }
        
        public DateTime DepartureDate { get; }
        public HotelCategoryType HotelCategory { get; }
        
        public virtual HotTourInfo Info { get; }
        public virtual DepartureLocation DepartureLocation { get; }
        public virtual Duration Duration { get; }
        public virtual Price Price { get; }
        public virtual Country Country { get; }
        public virtual Resort Resort { get; }
        
        public void NotifyFound() => 
            RaiseDomainEvent(new HotTourFound(this));
    }
}