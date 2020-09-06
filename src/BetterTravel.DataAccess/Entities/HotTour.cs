using System;
using BetterTravel.DataAccess.Entities.Base;
using BetterTravel.DataAccess.Entities.Enumerations;
using BetterTravel.DataAccess.Enums;
using BetterTravel.DataAccess.Events;
using BetterTravel.DataAccess.ValueObjects;

namespace BetterTravel.DataAccess.Entities
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
            RaiseDomainEvent(new HotTourFound(Info.Name));
    }
}