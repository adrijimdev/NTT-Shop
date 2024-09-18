using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_NTT_SHOP.BC;
using API_NTT_SHOP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_NTT_SHOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersBC usersBC = new UsersBC();

        [HttpGet]
        [Route("getAllUsers")]
        public ActionResult<GetAllUsersResponse> GetAllUsers()
        {
            GetAllUsersResponse response = usersBC.GetAllUsers();
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("getUser")]
        public ActionResult<GetUserResponse> GetUser(GetUserRequest request)
        {
            GetUserResponse response = usersBC.GetUser(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("getUserId")]
        public ActionResult<GetUserResponse> GetUserId(GetUserIdRequest request)
        {
            GetUserResponse response = usersBC.GetUserId(request);
            return HandleResponse(response);
        }

        [HttpPut]
        [Route("updateUser")]
        public ActionResult<GenericResponse> UpdateUser(UpdateUserRequest request)
        {
            GenericResponse response = usersBC.UpdateUser(request);
            return HandleResponse(response);
        }

        [HttpPut]
        [Route("updatePassword")]
        public ActionResult<GenericResponse> UpdatePassword(UpdateUserRequest request)
        {
            GenericResponse response = usersBC.UpdatePassword(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("insertUser")]
        public ActionResult<GenericResponse> InsertUser(InsertUserRequest request)
        {
            GenericResponse response = usersBC.InsertUser(request);
            return HandleResponse(response);
        }

        [HttpDelete]
        [Route("deleteUser/")]
        public ActionResult<GenericResponse> DeleteUser(DeleteUserRequest request)
        {
            GenericResponse response = usersBC.DeleteUser(request);
            return HandleResponse(response);
        }

        // MANAGEMENT USERS

        [HttpGet]
        [Route("getAllManUsers")]
        public ActionResult<GetAllManUsersResponse> GetAllManUsers()
        {
            GetAllManUsersResponse response = usersBC.GetAllManUsers();
            return HandleResponse(response);
        }

        [HttpGet]
        [Route("getManUser")]
        public ActionResult<GetManUserResponse> GetManUser(GetManUserRequest request)
        {
            GetManUserResponse response = usersBC.GetManUser(request);
            return HandleResponse(response);
        }


        [HttpPut]
        [Route("updateManUser")]
        public ActionResult<GenericResponse> UpdateManUser(UpdateManUserRequest request)
        {
            GenericResponse response = usersBC.UpdateManUser(request);
            return HandleResponse(response);
        }

        [HttpPut]
        [Route("updateManUserPassword")]
        public ActionResult<GenericResponse> UpdateManUserPassword(UpdateManUserRequest request)
        {
            GenericResponse response = usersBC.UpdateManUserPassword(request);
            return HandleResponse(response);
        }

        [HttpPost]
        [Route("insertManUser")]
        public ActionResult<GenericResponse> InsertManUser(InsertManUserRequest request)
        {
            GenericResponse response = usersBC.InsertManUser(request);
            return HandleResponse(response);
        }

        [HttpDelete]
        [Route("deleteManUser/")]
        public ActionResult<GenericResponse> DeleteManUser(DeleteManUserRequest request)
        {
            GenericResponse response = usersBC.DeleteManUser(request);
            return HandleResponse(response);
        }

        public ActionResult HandleResponse(BaseModelResponse response)
        {
            if (response.httpCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(response);
            }
            if (response.httpCode == System.Net.HttpStatusCode.NoContent)
            {
                return NoContent();
            }
            if (response.httpCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest();
            }
            if (response.httpCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else
            {
                return Forbid();
            }
        }


    }
}