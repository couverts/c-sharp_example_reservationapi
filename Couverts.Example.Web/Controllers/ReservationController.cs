using System;
using System.Linq;
using System.Web.Mvc;
using Couverts.Example.Services;
using Couverts.Example.Services.Calls.CreateReservation;
using Couverts.Example.Services.Calls.GetAvailableTimes;
using Couverts.Example.Services.Calls.GetBasicInfo;
using Couverts.Example.Services.Calls.GetConfigForDay;
using Couverts.Example.Services.Calls.GetInputFields;
using Couverts.Example.Services.Results;
using Couverts.Example.Web.Helpers;
using Couverts.Example.Web.Models;

namespace Couverts.Example.Web.Controllers {
    public class ReservationController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var defaultDate = DateTime.Now;
            var configuration = ApiServiceClient.Execute(new GetBasicInfoCall());
            var configForDay = ApiServiceClient.Execute(new GetConfigForDayCall(defaultDate));

            var viewModel = new IndexModel
            {
                RestaurantName = configuration.RestaurantName,
                IntroText = configuration.IntroText.Dutch,
                Date = defaultDate,
                Persons = configForDay.MinimumNumberOfPeople
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(IndexModel viewModel)
        {
            return RedirectToAction("Reservation", "Reservation", new
            {
                Date = DateTime.Parse($"{viewModel.Date.ToShortDateString()} {viewModel.Time}"),
                Persons = viewModel.Persons
            });
        }

        [HttpGet]
        public ActionResult AvailableTimes(DateTime date, int persons)
        {
            var availableTimes = ApiServiceClient.Execute(new GetAvailableTimesCall(date, persons));
            var viewModel = new AvailableTimesModel
            {
                Times = availableTimes.Times.Select(t => t.ToString()),
                Message = availableTimes.NoTimesAvailable != null ? availableTimes.NoTimesAvailable.Message.Dutch : ""
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Reservation(DateTime date, int persons) {
            var viewModel = new ReservationModel {
                DateTime = date,
                Persons = persons
            };

            var fieldsConfiguration = ApiServiceClient.Execute(new GetInputFieldsCall(date));

            ModelMapper.MapFieldsToViewModel(fieldsConfiguration, viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Reservation(ReservationModel viewModel) {
            //Re-fill the fields.
            var fieldsConfiguration = ApiServiceClient.Execute(new GetInputFieldsCall(viewModel.DateTime));

            ModelMapper.MapFieldsToViewModel(fieldsConfiguration, viewModel);

            //pre-fill the last known value.
            foreach (var fieldId in Request.Form.AllKeys) {
                var viewModelField = viewModel.Fields.SingleOrDefault(f => f.Id == fieldId);
                if (viewModelField != null) {
                    viewModelField.Value = viewModelField.Type == ReservationModel.DynamicField.FieldType.Checkbox 
                        ? Request.Form[fieldId].Split(',')[0] 
                        : Request.Form[fieldId];
                }
            }

            //Validation.
            foreach (var dynamicField in viewModel.Fields) {
                if (dynamicField.Required && String.IsNullOrWhiteSpace(dynamicField.Value)) {
                    ModelState.AddModelError(dynamicField.Id, $"{dynamicField.Title} is vereist." );
                }
            }

            if (!ModelState.IsValid) {
                return View(viewModel);
            }

            var reservationRequest = ModelMapper.CreateReservationRequestFromViewModel(viewModel);
            var response = ApiServiceClient.Execute(new CreateReservationCall(reservationRequest));

            return View("Success", new SuccessModel {
                Message = response.ConfirmationText.Dutch
            });
        }

        public ActionResult Success(SuccessModel viewModel) {
            return View(viewModel);
        }
    }
}