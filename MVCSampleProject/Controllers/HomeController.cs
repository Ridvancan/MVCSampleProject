using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCSampleProject.Models;

namespace MVCSampleProject.Controllers
{
    public class HomeController : Controller
    {


        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public string TekGunCalis(List<Personel> perList, List<Working> workList)
        {
            string listetek = " ";
            List<Personel> tempPerList = new List<Personel>(perList);
            List<Working> tempWorkList = new List<Working>(workList);

            for (int i = 0; i < 6; i++)
            {
                Random rndm = new Random();
                int h = tempWorkList.Count();
                int isAta = rndm.Next(0, h);
                listetek += $"{tempPerList[i].AdSoyad} : {tempWorkList[isAta].isAdi} Bölümünde Çalışacak.\n";
                perList.ElementAt(i).isYuku += tempWorkList[isAta].zorluk;
                perList.ElementAt(i).toplamİsYuku += tempWorkList[isAta].zorluk;
                tempWorkList.RemoveAt(isAta);
                Thread.Sleep(10);// seed

            }

            return listetek;

        }
            public string CiftGunCalis(List<Personel> perList, List<Working> workList)
        {
            string listecift = "";
            foreach (var per in perList)
            {

                int balanceWork = 7 - per.isYuku;
                IEnumerable<string> isListesi = workList.Where(x => x.zorluk == balanceWork).Select(x => x.isAdi).ToList();
                string newWork = isListesi.FirstOrDefault();
                per.isYuku += balanceWork;
                per.toplamİsYuku += balanceWork;
                //per.isYuku *= gün;
                listecift += $" {per.AdSoyad} : {newWork} Bölümünde Çalışacak.\n";


            }
            foreach (var per in perList)
            {
                per.isYuku = 0;
            }
            return listecift;
        }

        public IActionResult for14days()
        {
            string result = "";
            
            List<Personel> calisanListe = new List<Personel>();
            List<Working> isListesi = new List<Working>();
            Personel per1 = new Personel() { AdSoyad = "Rıdvan Can Kıran", isYuku = 0 };
            Personel per2 = new Personel() { AdSoyad = "Yunus Emre Gezer", isYuku = 0 };
            Personel per3 = new Personel() { AdSoyad = "Mehmet Ali Uyar", isYuku = 0 };
            Personel per4 = new Personel() { AdSoyad = "Şahin Tutarsız", isYuku = 0 };
            Personel per5 = new Personel() { AdSoyad = "Hasan Ali Ayrık", isYuku = 0 };
            Personel per6 = new Personel() { AdSoyad = "Numan Akdağ", isYuku = 0 };
            calisanListe.Add(per1);
            calisanListe.Add(per2);
            calisanListe.Add(per3);
            calisanListe.Add(per4);
            calisanListe.Add(per5);
            calisanListe.Add(per6);



            Working work1 = new Working() { isAdi = "Lojistik", zorluk = 1 };
            Working work2 = new Working() { isAdi = "Planlama", zorluk = 2 };
            Working work3 = new Working() { isAdi = "Üretim", zorluk = 3 };
            Working work4 = new Working() { isAdi = "Kalite", zorluk = 4 };
            Working work5 = new Working() { isAdi = "İnsan Kaynakları", zorluk = 5 };
            Working work6 = new Working() { isAdi = "İş Güvenliği", zorluk = 6 };
            isListesi.Add(work1);
            isListesi.Add(work2);
            isListesi.Add(work3);
            isListesi.Add(work4);
            isListesi.Add(work5);
            isListesi.Add(work6);
            foreach (var iss in isListesi)
            {
                result += $"{iss.isAdi} Bölümünün Zorluk Derecesi : {iss.zorluk}\n";
            }

            for (int i = 1; i < 14; i++)
            {
                
                if (i % 2 != 0)
                {
                    result += $"{i}. Gün\n";
                    result   +=  TekGunCalis(calisanListe, isListesi);
                }
                else

                {
                    result += $"{i}. Gün\n";
                    result += CiftGunCalis(calisanListe, isListesi);
                }


                
            }
            foreach (var per in calisanListe)
            {
                result += $"\n{per.AdSoyad} Toplamda {per.toplamİsYuku} birimlik iş yaptı.\n";
            }

            return Content(result);
        }
        
        
      
            public IActionResult formonth()
            {
                List<Personel> calisanListe = new List<Personel>();
                List<Working> isListesi = new List<Working>();
                Personel per1 = new Personel() { AdSoyad = "Rıdvan Can Kıran", isYuku = 0 };
                Personel per2 = new Personel() { AdSoyad = "Yunus Emre Gezer", isYuku = 0 };
                Personel per3 = new Personel() { AdSoyad = "Mehmet Ali Uyar", isYuku = 0 };
                Personel per4 = new Personel() { AdSoyad = "Şahin Tutarsız", isYuku = 0 };
                Personel per5 = new Personel() { AdSoyad = "Hasan Ali Ayrık", isYuku = 0 };
                Personel per6 = new Personel() { AdSoyad = "Numan Akdağ", isYuku = 0 };
                calisanListe.Add(per1);
                calisanListe.Add(per2);
                calisanListe.Add(per3);
                calisanListe.Add(per4);
                calisanListe.Add(per5);
                calisanListe.Add(per6);



                Working work1 = new Working() { isAdi = "Lojistik", zorluk = 1 };
                Working work2 = new Working() { isAdi = "Planlama", zorluk = 2 };
                Working work3 = new Working() { isAdi = "Üretim", zorluk = 3 };
                Working work4 = new Working() { isAdi = "Kalite", zorluk = 4 };
                Working work5 = new Working() { isAdi = "İnsan Kaynakları", zorluk = 5 };
                Working work6 = new Working() { isAdi = "İş Güvenliği", zorluk = 6 };
                isListesi.Add(work1);
                isListesi.Add(work2);
                isListesi.Add(work3);
                isListesi.Add(work4);
                isListesi.Add(work5);
                isListesi.Add(work6);
                string result="";
            foreach (var iss in isListesi)
            {
                result += $"{iss.isAdi} Bölümünün Zorluk Derecesi : {iss.zorluk}\n";
            }
            for (int i = 1; i < 31; i++)
            {
                if (i % 2 != 0)
                {
                    result += $"\n{i}. Gün\n";
                    result += TekGunCalis(calisanListe, isListesi);
                }
                else
                {
                    result += $"{i}. Gün\n";
                    result += CiftGunCalis(calisanListe, isListesi);
                }
                
                }
            foreach (var per in calisanListe)
            {
                result += $"\n{per.AdSoyad} Toplamda {per.toplamİsYuku} birimlik iş yaptı.\n";
            }
            return Content(result);
            }
        }
    }
