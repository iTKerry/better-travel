using System.Linq;
using BetterTravel.Domain.Entities.Base;

namespace BetterTravel.Domain.Entities
{
    public class HotelCategory : Entity
    {
        public static HotelCategory NoCategory =
            new HotelCategory((int) HotelCategoryType.NoCategory, HotelCategoryType.NoCategory.ToString());
        public static HotelCategory TwoStars =
            new HotelCategory((int) HotelCategoryType.TwoStars, HotelCategoryType.TwoStars.ToString());
        public static HotelCategory ThreeStars =
            new HotelCategory((int) HotelCategoryType.ThreeStars, HotelCategoryType.ThreeStars.ToString());
        public static HotelCategory FourStars =
            new HotelCategory((int) HotelCategoryType.FourStars, HotelCategoryType.FourStars.ToString());
        public static HotelCategory FiveStars =
            new HotelCategory((int) HotelCategoryType.FiveStars, HotelCategoryType.FiveStars.ToString());
        public static HotelCategory HV_1 =
            new HotelCategory((int) HotelCategoryType.HV_1, HotelCategoryType.HV_1.ToString());
        
        public static HotelCategory[] AllCategories = {
            NoCategory, TwoStars, ThreeStars, FourStars, FiveStars, HV_1
        };

        protected HotelCategory()
        {
        }
        
        private HotelCategory(int id, string name) 
            : base(id) =>
            Name = name;

        public string Name { get; }

        public static HotelCategory FromId(int id) =>
            AllCategories.SingleOrDefault(c => c.Id == id) 
            ?? NoCategory;

        public static HotelCategory FromType(HotelCategoryType categoryType) =>
            AllCategories.SingleOrDefault(c => c.Id == (int) categoryType) 
            ?? NoCategory;
    }

    public enum HotelCategoryType : int
    {
        NoCategory = 1,
        TwoStars = 2,
        ThreeStars = 3,
        FourStars = 4,
        FiveStars = 5,
        HV_1 = 26
    }
}