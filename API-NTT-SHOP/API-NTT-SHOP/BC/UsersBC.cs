using API_NTT_SHOP.DAC;
using API_NTT_SHOP.Models;
using API_NTT_SHOP.NTTSHOP_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API_NTT_SHOP.BC
{
    public class UsersBC
    {
        private readonly UsersDAC usersDAC = new UsersDAC();

        public GetAllUsersResponse GetAllUsers()
        {
            GetAllUsersResponse getAllUsersResponse = new GetAllUsersResponse();

            getAllUsersResponse.users = usersDAC.GetAllUsers();

            if (getAllUsersResponse.users != null && getAllUsersResponse.users.Count > 0)
            {
                getAllUsersResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getAllUsersResponse.message = "No content";
                getAllUsersResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getAllUsersResponse;
        }

        public GetUserResponse GetUser(GetUserRequest request)
        {
            GetUserResponse getUserResponse = new GetUserResponse();
            getUserResponse.user = usersDAC.GetUser(request.PkUser);

            if (GetUserValidation(request))
            {
                bool correctOperation = Convert.ToBoolean(getUserResponse.user.PkUser);
                getUserResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getUserResponse.message = "No content";
                getUserResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getUserResponse;
        }

        public GetUserResponse GetUserId(GetUserIdRequest request)
        {
            GetUserResponse getUserResponse = new GetUserResponse();
            getUserResponse.user = usersDAC.GetUserId(request.login);

            if (GetUserIdValidation(request))
            {
                bool correctOperation = Convert.ToBoolean(getUserResponse.user.PkUser);
                getUserResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getUserResponse.message = "No content";
                getUserResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getUserResponse;
        }

        public GenericResponse UpdateUser(UpdateUserRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (UpdateUserValidation(request))
            {
                bool correctOperation = usersDAC.UpdateUser(request.user);

                if (correctOperation)
                {
                    genericResponse.httpCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    genericResponse.message = "NotFound";
                    genericResponse.httpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            else
            {
                genericResponse.message = "BadRequest";
                genericResponse.httpCode = System.Net.HttpStatusCode.BadRequest;
            }

            return genericResponse;
        }

        public GenericResponse UpdatePassword(UpdateUserRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();
            if (PasswordValidator(request.user.Password))
            {
                bool correctOperation = usersDAC.UpdatePassword(request.user);

                if (correctOperation)
                {
                    genericResponse.httpCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    genericResponse.message = "NotFound";
                    genericResponse.httpCode = System.Net.HttpStatusCode.NotFound;
                }
            }

            return genericResponse;

        }

        internal GenericResponse InsertUser(InsertUserRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (InsertUserValidation(request))
            {
                bool correctOperation = usersDAC.InsertUser(request.user);

                if (correctOperation)
                {
                    genericResponse.httpCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    genericResponse.message = "NotFound";
                    genericResponse.httpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            else
            {
                genericResponse.message = "BadRequest";
                genericResponse.httpCode = System.Net.HttpStatusCode.BadRequest;
            }

            return genericResponse;
        }

        internal GenericResponse DeleteUser(DeleteUserRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (DeleteUserValidation(request))
            {
                bool correctOperation = usersDAC.DeleteUser(request.PkUser);

                if (correctOperation)
                {
                    genericResponse.httpCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    genericResponse.message = "NotFound";
                    genericResponse.httpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            else
            {
                genericResponse.message = "BadRequest";
                genericResponse.httpCode = System.Net.HttpStatusCode.BadRequest;
            }

            return genericResponse;
        }

        // MANAGEMENT USERS
        public GetAllManUsersResponse GetAllManUsers()
        {
            GetAllManUsersResponse getAllManUsersResponse = new GetAllManUsersResponse();

            getAllManUsersResponse.manusers = usersDAC.GetAllManUsers();

            if (getAllManUsersResponse.manusers != null && getAllManUsersResponse.manusers.Count > 0)
            {
                getAllManUsersResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getAllManUsersResponse.message = "No content";
                getAllManUsersResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getAllManUsersResponse;
        }

        public GetManUserResponse GetManUser(GetManUserRequest request)
        {
            GetManUserResponse getManUserResponse = new GetManUserResponse();
            getManUserResponse.manuser = usersDAC.GetManUser(request.PkManuser);

            if (GetManUserValidation(request))
            {
                bool correctOperation = Convert.ToBoolean(getManUserResponse.manuser.PkManuser);
                getManUserResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getManUserResponse.message = "No content";
                getManUserResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getManUserResponse;
        }

        public GenericResponse UpdateManUser(UpdateManUserRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (UpdateManUserValidation(request))
            {
                bool correctOperation = usersDAC.UpdateManUser(request.manuser);

                if (correctOperation)
                {
                    genericResponse.httpCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    genericResponse.message = "NotFound";
                    genericResponse.httpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            else
            {
                genericResponse.message = "BadRequest";
                genericResponse.httpCode = System.Net.HttpStatusCode.BadRequest;
            }

            return genericResponse;
        }

        public GenericResponse UpdateManUserPassword(UpdateManUserRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();
            if (PasswordValidator(request.manuser.Password))
            {
                bool correctOperation = usersDAC.UpdateManUserPassword(request.manuser);

                if (correctOperation)
                {
                    genericResponse.httpCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    genericResponse.message = "NotFound";
                    genericResponse.httpCode = System.Net.HttpStatusCode.NotFound;
                }
            }

            return genericResponse;

        }

        internal GenericResponse InsertManUser(InsertManUserRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (InsertManUserValidation(request))
            {
                bool correctOperation = usersDAC.InsertManUser(request.manuser);

                if (correctOperation)
                {
                    genericResponse.httpCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    genericResponse.message = "NotFound";
                    genericResponse.httpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            else
            {
                genericResponse.message = "BadRequest";
                genericResponse.httpCode = System.Net.HttpStatusCode.BadRequest;
            }

            return genericResponse;
        }

        internal GenericResponse DeleteManUser(DeleteManUserRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (DeleteManUserValidation(request))
            {
                bool correctOperation = usersDAC.DeleteManUser(request.Pkmanuser);

                if (correctOperation)
                {
                    genericResponse.httpCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    genericResponse.message = "NotFound";
                    genericResponse.httpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            else
            {
                genericResponse.message = "BadRequest";
                genericResponse.httpCode = System.Net.HttpStatusCode.BadRequest;
            }

            return genericResponse;
        }


        //////VALIDATIONS

        private bool UpdateUserValidation(UpdateUserRequest request)
        {
            if (request != null
                && request.user != null
                && UpdateControl(request.user.PkUser, request.user.Password, request.user.Email) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool UpdateManUserValidation(UpdateManUserRequest request)
        {
            if (request != null
                && request.manuser != null
                && UniqueControl(request.manuser.Login, request.manuser.Email) == true
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InsertUserValidation(InsertUserRequest request)
        {
            if (request != null
                && request.user != null
                && !string.IsNullOrWhiteSpace(request.user.Login)
                && !string.IsNullOrWhiteSpace(request.user.Password)
                && !string.IsNullOrWhiteSpace(request.user.Name)
                && !string.IsNullOrWhiteSpace(request.user.Surname1)
                && !string.IsNullOrWhiteSpace(request.user.Email)
                && UniqueControl(request.user.Login, request.user.Email) == true
                && PasswordValidator(request.user.Password) == true
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InsertManUserValidation(InsertManUserRequest request)
        {
            if (request != null
                && request.manuser != null
                && !string.IsNullOrWhiteSpace(request.manuser.Login)
                && !string.IsNullOrWhiteSpace(request.manuser.Password)
                && !string.IsNullOrWhiteSpace(request.manuser.Name)
                && !string.IsNullOrWhiteSpace(request.manuser.Surname1)
                && !string.IsNullOrWhiteSpace(request.manuser.Email)
                && UniqueManUserControl(request.manuser.Login, request.manuser.Email) == true
                && PasswordValidator(request.manuser.Password) == true
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DeleteUserValidation(DeleteUserRequest request)
        {
            if (request != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DeleteManUserValidation(DeleteManUserRequest request)
        {
            if (request != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool GetUserValidation(GetUserRequest request)
        {
            using (NTTSHOPContext context = new NTTSHOPContext())
            {
                if (request != null
                && context.Users.Any(x => x.PkUser == request.PkUser))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }                
        }

        private bool GetUserIdValidation(GetUserIdRequest request)
        {
            using (NTTSHOPContext context = new NTTSHOPContext())
            {
                if (request != null
                && context.Users.Any(x => x.Login == request.login))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool GetManUserValidation(GetManUserRequest request)
        {
            if (request != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool UniqueControl(string login, string email)
        {
            using (NTTSHOPContext context = new NTTSHOPContext())
            {
                if (context.Users.Any(x => x.Login == login)
                    || context.Users.Any(x => x.Email == email)
                    )
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }
        }

        private bool UpdateControl(int id, string login, string email)
        {
            using (NTTSHOPContext context = new NTTSHOPContext())
            {
                if (context.Users.Any(x => x.Login == login && x.PkUser != id)
                    || context.Users.Any(x => x.Email == email && x.PkUser != id)
                    )
                    return false;
                else
                    return true;
            }
        }

        private bool UniqueManUserControl(string login, string email)
        {
            using (NTTSHOPContext context = new NTTSHOPContext())
            {
                if (context.Managementusers.Any(x => x.Login == login)
                    || context.Managementusers.Any(x => x.Email == email)
                    )
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }
        }

        private bool PasswordValidator(string password)
        {
            using (NTTSHOPContext context = new NTTSHOPContext())
            {
                Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{10,16}$");
                if (regex.IsMatch(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}