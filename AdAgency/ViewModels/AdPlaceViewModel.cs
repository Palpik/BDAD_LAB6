using System;

namespace AdAgency.ViewModels
{
    public class AdPlaceViewModel
    {
        public int Id { get; set; }
        public string Place { get; set; } 
        public string Desc { get; set; }
        public decimal Cost { get; set; }
        public string TypeName { get; set; }
        public int TypeId { get; set;}
    }
}
