using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssessment.Core.EnumHelper;

namespace TechnicalAssessment.Core.Helper
{
    public class BaseController : ControllerBase
    {
        protected readonly ILogger _logger;

        protected BaseController(ILogger logger)
        {
            _logger = logger;
        }
        private IActionResult ReturnHttpMessage<T>(ApiResponseCodes codes, ApiResponse<T> response)
        {
            switch (codes)
            {
                case ApiResponseCodes.EXCEPTION:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, response);
                case ApiResponseCodes.UNAUTHORIZED:
                    return this.StatusCode(StatusCodes.Status401Unauthorized, response);
                case ApiResponseCodes.NOT_FOUND:
                case ApiResponseCodes.INVALID_REQUEST:
                case ApiResponseCodes.ERROR:
                case ApiResponseCodes.FAIL:
                    return this.StatusCode(StatusCodes.Status400BadRequest, response);
                case ApiResponseCodes.OK:
                default:
                    return Ok(response);
            }
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ApiResponse(dynamic data = default(object), string[] messages = null,
            ApiResponseCodes codes = ApiResponseCodes.OK, int? totalCount = 0)
        {

            if (codes == ApiResponseCodes.OK)
            {
                ApiResponse<dynamic> response = new ApiResponse<dynamic>
                {
                    Payload = data,
                    Code = codes,
                    Description = messages?.FirstOrDefault()
                };

                response.TotalCount = totalCount ?? 0;
                return ReturnHttpMessage(codes, response);
            }
            else
            {
                ApiResponse<dynamic> response = new ApiResponse<dynamic>
                {
                    Payload = data,
                    Code = codes,
                    Errors = messages?.ToList(),
                    Description = messages?.FirstOrDefault()
                };

                response.TotalCount = totalCount ?? 0;
                return ReturnHttpMessage(codes, response);
            }
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ApiResponse<T>(T data = default(T), string message = null,
            ApiResponseCodes codes = ApiResponseCodes.OK, int? totalCount = 0, params string[] errors) where T : class
        {
            ApiResponse<T> response = new ApiResponse<T>
            {
                TotalCount = totalCount ?? 0,
                Errors = errors.ToList(),
                Payload = data,
                Code = !errors.Any() ? codes : codes == ApiResponseCodes.OK ? ApiResponseCodes.ERROR : codes
            };

            if (response.Code == ApiResponseCodes.ERROR)
            {
                response.Description = message;
                return Ok(response);
            }

            response.Description = message ?? response.Code.GetDescription();
            return Ok(response);
        }


        protected string GetModelStateValidationError()
        {
            string message = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.Exception == null ? e.ErrorMessage : e.Exception.Message).FirstOrDefault()!;
            return message;
        }

        protected IActionResult HandleError(Exception ex, string customErrorMessage = null)
        {
            _logger.LogError(ex, ex.Message);


            ApiResponse<string> rsp = new ApiResponse<string>();
            rsp.Code = ApiResponseCodes.ERROR;
#if DEBUG
            rsp.Errors = new List<string>() { $"Error: {(ex?.InnerException?.Message ?? ex.Message)} --> {ex?.StackTrace}" };
            return Ok(rsp);
#else
             rsp.Errors = new List<string>() {  customErrorMessage ?? "An error occurred while processing your request!"};
             return Ok(rsp);
#endif
        }
        protected IActionResult Success<T>(T data, int? count = null)
        {
            return Ok(new GenericResponse<T>
            {
                Payload = data,
                TotalCount = count
            });
        }

        protected IActionResult Error<T>(string errorMessage)
        {
            _logger.LogWarning("API Error: {Error}", errorMessage);
            return BadRequest(new GenericResponse<T>
            {
                ErrorMessage = errorMessage
            });
        }
    }
}
