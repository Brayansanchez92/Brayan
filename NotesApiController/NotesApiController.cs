using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Requests.NotesRequest;
using Sabio.Services;
using Sabio.Services.NotesService;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;

namespace Sabio.Web.Api.Controllers.NotesApiController
{
    [Route("api/notes")]
    [ApiController]
    public class NotesApiController : BaseApiController
    {
        private INotesService _service = null;
        private IAuthenticationService<int> _authService = null;

        public NotesApiController(INotesService service, ILogger<NotesApiController> logger
          , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }
        [HttpPost]
        public ActionResult AddNotes(NotesAddRequests model)
        {
            ObjectResult result = null;
            int EntityTypeId = _authService.GetCurrentUserId();
            int id = _service.AddNotes(model);
            try
            {
                ItemResponse<int> response = new ItemResponse<int>();
                response.Item = id;
                result = Created201(response);
            }
            catch (Exception ex)
            {

                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }
            return result;
        }


         [HttpPut("{id:int}")]
          public ActionResult<ItemResponse<int>> Update(NotesUpdateRequest model)

          {
                _service.Update(model);
                SuccessResponse response = new SuccessResponse();
                return Ok(response);
          }


        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<Notes>> Get(int id)
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                Notes notes = _service.Get(id);


                if (notes == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Friend not found.");

                }
                else
                {
                    response = new ItemResponse<Notes> { Item = notes };
                }
            }



            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Errors: ${ex.Message }");

            }



            return StatusCode(iCode, response);
        }


        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<Notes>>> GetNotesCreatedBy(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                int CreadtedBy = _authService.GetCurrentUserId();
                Paged<Notes> page = _service.GetNotesCreatedBy(CreadtedBy, pageIndex, pageSize);

                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application resource not found.");

                }
                else
                {
                    response = new ItemResponse<Paged<Notes>> { Item = page };
                }
            }
            catch (Exception ex)
            {

                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }
            return StatusCode(code, response);
        }

    }


               
}   







    