using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using VA.MyTrips.Web.Components.Definition;
using VA.MyTrips.Web.ViewModels;

namespace VA.MyTrips.Web.Controllers
{
    public class TripController : Controller
    {

        private readonly ITripComponent _tripComponent;
        private readonly ILogger<TripController> _logger;

        public TripController(ITripComponent tripComponent, ILogger<TripController> logger)
        {
            _tripComponent = tripComponent;
            _logger = logger;
        }

        // GET: TripController
        public async Task<ActionResult> Index()
        {
            try
            {
                var vm = await _tripComponent.GetTrips();

                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error -Index()");
                return View("Error");
            }
        }

        // GET: TripController/Details/5
        public async Task<ActionResult> Details(int id)
        {

            try
            {
                var vm = await _tripComponent.GetTripDetails(id);
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error - Details({id})", id);
                return View("Error");
            }
        }


        #region Create  

        // GET: TripController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TripController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] TripDetailedModel collection)
        {
            try
            {
                var newTrip = await _tripComponent.CreateTrip(collection);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error - Create({collection})", collection.ToJsonString());
                return View("Error");
            }
        }


        // POST: TripController/AddPhoto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadPhoto([FromForm] int tripId, List<IFormFile> files)
        {
            try
            {

                var file = files.FirstOrDefault();

                if (file?.Length > 0)
                {
                    using var ms = new MemoryStream();
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    var success = await _tripComponent.UploadPhoto(tripId, fileBytes);

                    if (success)
                        return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error - UploadPhoto()");
            }

            return View("Error");
        }


        #endregion


    }
}
