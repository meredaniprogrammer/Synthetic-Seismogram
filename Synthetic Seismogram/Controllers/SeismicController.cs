using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using DataTablesParser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synthetic_Seismogram.Components;
using Synthetic_Seismogram.Data;
using Synthetic_Seismogram.Models;
using Synthetic_Seismogram.ViewModels;

namespace Synthetic_Seismogram.Controllers
{
    [Authorize]
    public class SeismicController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UtilityHelper _utilityHelper;
        public SeismicController(ApplicationDbContext context, UtilityHelper utilityHelper)
        {
            _context = context;
            _utilityHelper = utilityHelper;
        }
        // GET: SeismicController
        public ActionResult Index()
        {
            return View();
        } 
        public ActionResult ViewRfData(int id)
        {
            ViewBag.Id = id;
            //var rfs = _context.AcousticImpedances.Where(p => p.ReflectiveCoefficientId == id);
            return View();
        } 
        public ActionResult ViewSyntheticData(int id)
        {
            ViewBag.Id = id;
            //var rfs = _context.AcousticImpedances.Where(p => p.ReflectiveCoefficientId == id);
            return View();
        }

        [HttpPost]
        public ActionResult GetDataUploads()
        {
            var rfs = _context.ReflectiveCoefficients.ToList();
            var rfvm = new List<RCViewModel>();
            foreach (var item in rfs)
            {
                var newrc = new RCViewModel { Date = item.Date.ToShortDateString(), Dt = item.Dt.ToString(), Id = item.Id, Name = item.Name,DateUpdate=item.Date };
                rfvm.Add(newrc);
            }
            var parser = new Parser<RCViewModel>(Request.Form, rfvm.ToList().OrderBy(p=>p.DateUpdate).AsQueryable());
           
            return Json(parser.Parse());
        } 
        [HttpPost]
        public ActionResult GetAiUploads(int Id)
        {
            var Ai = _context.AcousticImpedances.Where(p => p.ReflectiveCoefficientId == Id);
            var parser = new Parser<AcousticImpedance>(Request.Form, Ai.ToList().OrderBy(p=>p.TWT).AsQueryable());
           
            return Json(parser.Parse());
        }
         [HttpPost]
        public ActionResult GetSyUploads(int Id)
        {
            var Ai = _context.Synthetics.Where(p => p.ReflectiveCoefficientId == Id);
            var parser = new Parser<Synthetic>(Request.Form, Ai.ToList().OrderBy(p=>p.TWT).AsQueryable());
           
            return Json(parser.Parse());
        }

        // POST: SeismicController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormFile uploadeddata,ReflectiveCoefficient reflectiveCoefficient)
        {
            var synthetics= new List<Synthetic>();
            var Acoustics = new List<AcousticImpedance>();
            var res = new List<CsvModel>();
          
            
            try
            {

                using (var reader = new StreamReader(uploadeddata.OpenReadStream()))

                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<CsvModel>();
                    res = records.ToList();
                }
                var AIArray = new float[res.Count()];
                var TWTArray = new float[res.Count()];
                var count = 0;
                foreach (var row in res)
                {

                    float floattwt;
                    float floatAi;
                    try
                    {
                         floattwt = float.Parse(row.TWT);
                    }
                    catch (Exception)
                    {
                        TempData["Error"] = $"Time Value: {row.TWT} is not in the correct format, Please follow Format";
                        return RedirectToAction(nameof(Index));
                    } 
                    try
                    {
                         floatAi = float.Parse(row.AI);
                    }
                    catch (Exception)
                    {

                        TempData["Error"] = $"AI value: {row.AI} is not in the correct format, Please follow Format";
                        return RedirectToAction(nameof(Index));
                    }
                    var acoustic = new AcousticImpedance() { AI=floatAi, TWT=floattwt };

                    Acoustics.Add(acoustic);
                    AIArray[count] = floatAi;
                    TWTArray[count] = floattwt;
                    count++;
                }
                var dd = 0;
                var result = await _utilityHelper.getSyntheticFromPython(reflectiveCoefficient.Dt, TWTArray,AIArray );
                if (result!=null)
                {
                    foreach (var num in result)
                    {
                        //var newfloat = float.Parse(num);
                        var newsynthetic = new Synthetic { Sy = num };
                        synthetics.Add(newsynthetic);
                        dd++;
                    }
                    reflectiveCoefficient.AcousticImpedances = Acoustics;
                    reflectiveCoefficient.Synthetics = synthetics;
                    reflectiveCoefficient.Date = DateTime.Now;
                    _context.Add(reflectiveCoefficient);
                    _context.SaveChanges();
                    TempData["Success"] = "Upload and Synthetic data generated successfully";
                    return RedirectToAction(nameof(Index));
                }
                TempData["Error"] = "Unable to process file";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Unable to process file: {ex.Message}";
                return RedirectToAction(nameof(Index));

            }

          // return RedirectToAction(nameof(Index));
        }

        // GET: SeismicController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SeismicController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SeismicController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SeismicController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
