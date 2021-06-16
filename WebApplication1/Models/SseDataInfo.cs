using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    [Serializable]
    public class SseDataInfo
    {
        [DisplayName("Kontrat")]
        public string KontratAd { get; set; }


        [DisplayName("Alış Fiyatı")]
        public string BestBuyPrice { get; set; }


        [DisplayName("Alış Adedi")]
        public string BestBuyQuantity { get; set; }


        [DisplayName("Satış Fiyatı")]
        public string BestSellPrice { get; set; }


        [DisplayName("SATIŞ ALANI DESEK DAHA İİY OLURMU ARKADAŞ ")]
        public string BestSellQuantity { get; set; }


        [DisplayName("Yüklenme Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime EventDate { get; set; }



    }
}
