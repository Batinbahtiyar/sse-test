using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Pages;

namespace sse_library
{
    public class SseServiceClient
    {
        private List<SseDataInfo> _sseOkunanVeriler = new List<SseDataInfo>();

        public SseDataInfo[] GetSseData()
        {
            return _sseOkunanVeriler.ToArray();
        }

        public async Task Run()
        {
            using (var client = new HttpClient())
            {
                using (var stream = await client.GetStreamAsync("https://gip.epias.com.tr/gunici/SseServlet"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        while (true)
                        {
                            var sseResponse = reader.ReadLine();
                            var saatlikTabelaVerisi = "";


                            if (string.IsNullOrEmpty(sseResponse))
                                continue;
                            if (sseResponse.Contains("DİKKAT!!!"))
                                continue;

                            if (sseResponse.StartsWith("event:"))
                            {
                                if (sseResponse.Contains("SaatlikTabela"))
                                {

                                }
                            }
                            else if (sseResponse.StartsWith("id:"))
                            {

                            }
                            else if (sseResponse.StartsWith("data:"))
                            {


                                var pozisyon = sseResponse.IndexOf("data: ");
                                var veriBaslangic = pozisyon + 6;

                                saatlikTabelaVerisi = sseResponse.Substring(veriBaslangic, sseResponse.Length -
                                    veriBaslangic);

                                Console.WriteLine("Okunan veri:" + saatlikTabelaVerisi);


                                var sseVerisi = Newtonsoft.Json.JsonConvert
                                    .DeserializeObject<SseDataInfo>(saatlikTabelaVerisi);

                                if (sseVerisi != null)
                                {
                                    Console.WriteLine("KontratAd:" + sseVerisi.KontratAd);
                                    Console.WriteLine("BestBuyPrice:" + sseVerisi.BestBuyPrice);
                                    Console.WriteLine("BestSellQuantity" + sseVerisi.BestSellQuantity);
                                    Console.WriteLine("BestSellPrice" + sseVerisi.BestSellPrice);
                                    Console.WriteLine("BestBuyQuantity" + sseVerisi.BestBuyQuantity);


                                    sseVerisi.EventDate = DateTime.Now;


                                    var mevcutVeri = _sseOkunanVeriler
                                        .Find(j => j.KontratAd == sseVerisi.KontratAd);

                                    if (mevcutVeri == null)
                                    {
                                        _sseOkunanVeriler.Add(sseVerisi);
                                    }
                                    else
                                    {
                                        // kontrat daha önce eklenmiş
                                        var mevcutVeriPozisyon = _sseOkunanVeriler
                                            .FindIndex(f => f.KontratAd == sseVerisi.KontratAd);

                                        _sseOkunanVeriler.RemoveAt(mevcutVeriPozisyon);

                                        // yeni geleni ekle...
                                        _sseOkunanVeriler.Add(sseVerisi);
                                    }

                                }

                            }

                            Thread.Sleep(1000);
                        }
                    }
                }
            }
        }
    }
}