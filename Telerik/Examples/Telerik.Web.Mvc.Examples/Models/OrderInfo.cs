namespace Telerik.Web.Mvc.Examples.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class OrderInfo
    {
        [ScaffoldColumn(false)]
        public int OrderInfoID
        {
            get;
            set;
        }

        [DisplayName("Delay")]
        [DataType(DataType.Time)]
        public DateTime? Delay
        {
            get;
            set;
        }

        [DisplayName("Delivery date")]
        [DataType(DataType.Date)]
        public DateTime? DeliveryDate
        {
            get;
            set;
        }

        [DisplayName("Order date time")]
        [DataType(DataType.DateTime)]
        public DateTime? OrderDateTime
        {
            get;
            set;
        }
    }
}