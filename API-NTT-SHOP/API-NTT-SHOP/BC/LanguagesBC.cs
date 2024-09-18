using API_NTT_SHOP.DAC;
using API_NTT_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.BC
{
    public class LanguagesBC
    {
        private readonly LanguagesDAC languagesDAC = new LanguagesDAC();

        public GetAllLanguagesResponse GetAllLanguages()
        {
            GetAllLanguagesResponse getAllLanguagesResponse = new GetAllLanguagesResponse();

            getAllLanguagesResponse.languages = languagesDAC.GetAllLanguages();

            if (getAllLanguagesResponse.languages != null && getAllLanguagesResponse.languages.Count > 0)
            {
                getAllLanguagesResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getAllLanguagesResponse.message = "No content";
                getAllLanguagesResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getAllLanguagesResponse;
        }

        public GetLanguageResponse GetLanguage(GetLanguageRequest request)
        {
            GetLanguageResponse getLanguageResponse = new GetLanguageResponse();
            getLanguageResponse.language = languagesDAC.GetLanguage(request.idLanguage);

            if (GetLanguageValidation(request))
            {
                bool correctOperation = Convert.ToBoolean(getLanguageResponse.language.idLanguage);
                getLanguageResponse.httpCode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                getLanguageResponse.message = "No content";
                getLanguageResponse.httpCode = System.Net.HttpStatusCode.NoContent;
            }

            return getLanguageResponse;
        }

        public GenericResponse UpdateLanguage(UpdateLanguageRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (UpdateLanguageValidation(request))
            {
                bool correctOperation = languagesDAC.UpdateLanguage(request.language);

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

        internal GenericResponse InsertLanguage(InsertLanguageRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (InsertLanguageValidation(request))
            {
                bool correctOperation = languagesDAC.InsertLanguage(request.language);

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

        internal GenericResponse DeleteLanguage(DeleteLanguageRequest request)
        {
            GenericResponse genericResponse = new GenericResponse();

            if (DeleteLanguageValidation(request))
            {
                bool correctOperation = languagesDAC.DeleteLanguage(request.idLanguage);

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


        //VALIDATIONS
        private bool UpdateLanguageValidation(UpdateLanguageRequest request)
        {
            if (request != null
                && request.language != null
                && !string.IsNullOrWhiteSpace(request.language.description)
                && !string.IsNullOrWhiteSpace(request.language.iso)
                && request.language.idLanguage > 0
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InsertLanguageValidation(InsertLanguageRequest request)
        {
            if (request != null
                && request.language != null
                && !string.IsNullOrWhiteSpace(request.language.description)
                && !string.IsNullOrWhiteSpace(request.language.iso)
                )

            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DeleteLanguageValidation(DeleteLanguageRequest request)
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

        private bool GetLanguageValidation(GetLanguageRequest request)
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
    }
}