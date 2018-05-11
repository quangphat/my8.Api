using Microsoft.AspNetCore.Mvc;
using my8.Api.Infrastructures;
using my8.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Controllers
{
    public class BaseController : Controller
    {
        public readonly CurrentProcess _process;
        public BaseController(CurrentProcess process)
        {
            _process = process;
        }
        private JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public IActionResult ToResponse()
        {
            var model = new ResponseJsonModel();

            _checkHasError(model);

            return Json(model, _jsonSerializerSettings);
        }
        public IActionResult ToResponse<T>(T data) where T : class
        {
            var model = new ResponseJsonModel<T>();

            if (!_checkHasError(model))
                model.data = data;

            return Json(model, _jsonSerializerSettings);
        }
        public IActionResult ToResponse(bool isSuccess)
        {
            var model = new ResponseActionJsonModel();

            if (!_checkHasError(model))
                model.success = isSuccess;

            return Json(model, _jsonSerializerSettings);
        }
        private bool _checkHasError(ResponseJsonModel model)
        {
            var hasError = _process.HasError;

            if (hasError)
            {
                var errorMessage = _process.ToError();

                model.error = new ErrorJsonModel()
                {
                    code = errorMessage.Message,
                    message = Errors.Get(errorMessage.Message),
                    trace_keys = errorMessage.TraceKeys
                };
            }

            return hasError;
        }

    }
}
