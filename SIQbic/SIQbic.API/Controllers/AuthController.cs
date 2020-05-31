using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SIQbic.API.Data;
using SIQbic.API.Dtos;
using SIQbic.API.Model;
using System.Collections.Generic;
using System.Linq;
using SIQbic.API.Model.Enums;

namespace SIQbic.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            this._repo = repo;          
            this._config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDto)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();

            if (await this._repo.UserExists(userForRegisterDto.UserName))
            {
                return BadRequest("This username already exists");
            }

            var userToCreate = new User 
            {
                UserName = userForRegisterDto.UserName,
                DisplayName = userForRegisterDto.DisplayName,
                PhoneNumber = userForRegisterDto.Phone,
                QuestionResponses = new List<QuestionResponse>()
            };

            var data = await this._repo.GetQuestions();
            var questions = data.ToList();

            var q1 = questions.FirstOrDefault(q => q.Id == Convert.ToInt32(userForRegisterDto.Question1.Split(":")[1].Trim()));
            var q2 = questions.FirstOrDefault(q => q.Id == Convert.ToInt32(userForRegisterDto.Question2.Split(":")[1]));
            var q3 = questions.FirstOrDefault(q => q.Id == Convert.ToInt32(userForRegisterDto.Question3.Split(":")[1]));

            userToCreate.QuestionResponses.Add(new QuestionResponse {
                Question = q1,
                Response = userForRegisterDto.Response1
            });
            userToCreate.QuestionResponses.Add(new QuestionResponse {
                Question = q2,
                Response = userForRegisterDto.Response2
            });
            userToCreate.QuestionResponses.Add(new QuestionResponse {
                Question = q3,
                Response = userForRegisterDto.Response3
            });

            var userCreated = await this._repo.Register(userToCreate, userForRegisterDto.Password);

            await this._repo.UpdateRegisterCodeRecord(userForRegisterDto.RegistrationCode, RegistrationCodeStatusType.Activated.ToString(), userCreated.Id);

            
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult>Login(UserForLoginDTO userForLoginDTO)
        {
            var userFromRepo = await _repo.Login(userForLoginDTO.UserName.ToLower(), userForLoginDTO.Password);

            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName),
                new Claim(ClaimTypes.GivenName, userFromRepo.DisplayName)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config.GetSection("AppSettings:Token").Value));

            var creds =  new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions()
        {
            List<QuestionForList> result = new List<QuestionForList>();
            
            var data = await this._repo.GetQuestions();

            foreach(var itm in data)
            {
                result.Add(new QuestionForList{
                    Id = itm.Id,
                    DisplayValue = itm.DisplayText
                });
            }

            return Ok(result);
        }

        [HttpGet("onboard/{rcode}")]
        public async Task<ActionResult> StartRegistrationProcess(string rcode)
        {
            string CODE_NOT_VALID_MESSAGE = "502 - Codigo de registro no valido.";
            string CODE_CANCELLED_MESSAGE = "503 - El codigo fue cancelado.";
            string CODE_DUE_DATE_MESSAGE ="504 - El codigo ya expiro.";
            string CODE_ALREADY_ACTIVADED_MESSAGE= "505 - Este codigo ya fue utilizado.";
            string CODE_IN_WAITING_APPROVAL = "506 - Invitacion en proceso de aprobacion";

            UserToStartRegister result = new UserToStartRegister();

            var data = await this._repo.GetInvitations();
            var regCode = data.ToList().FirstOrDefault(rc => rc.Code == rcode);

            if (regCode == null)
            {
                return BadRequest(CODE_NOT_VALID_MESSAGE);
            }
            else
            {
                RegistrationCodeStatusType userRegCodeStatus = (RegistrationCodeStatusType) Enum.Parse(typeof(RegistrationCodeStatusType), regCode.Status);

                if (userRegCodeStatus ==  RegistrationCodeStatusType.Cancelled)
                {
                    return StatusCode(500, CODE_CANCELLED_MESSAGE);
                }
                else if (userRegCodeStatus == RegistrationCodeStatusType.Activated)
                {
                    return StatusCode(500, CODE_ALREADY_ACTIVADED_MESSAGE);
                }
                else if (userRegCodeStatus == RegistrationCodeStatusType.Requested)
                {
                    return StatusCode(500, CODE_IN_WAITING_APPROVAL);
                }
                else if (regCode.DueDate < DateTime.Now)
                {
                    return StatusCode(500, CODE_DUE_DATE_MESSAGE);
                }
                else
                {
                    return Ok (new UserToStartRegister{
                        Email = regCode.InvitedEmail
                     });
                }
            }

        }

        [HttpPost("invites/{rcode}")]
        public async Task<ActionResult> RequestInvite(string rcode)
        {
            string CODE_NOT_VALID_MESSAGE = "502 - Codigo de registro no valido.";

            UserToStartRegister result = new UserToStartRegister();

            var data = await this._repo.GetInvitations();
            var regCode = data.FirstOrDefault(rc => rc.Code == rcode);

            if (regCode == null)
            {
                return BadRequest(CODE_NOT_VALID_MESSAGE);
            }
            else
            {
               
                RegistrationCodeStatusType userRegCodeStatus = (RegistrationCodeStatusType) Enum.Parse(typeof(RegistrationCodeStatusType), regCode.Status);

                this._repo.RequestInvitation(regCode.SponsorEmail, regCode.RoleId);
                return Ok();
            }

        }

        [HttpPost("invites")]
        public async Task<ActionResult> CreateInvite(InviteForCreationDTO inviteForCreation)
        {

            RegistrationCode regCode = new RegistrationCode {
                DateCreated = DateTime.Now,
                InvitedEmail = inviteForCreation.InvitedEmail,
                SponsorEmail = inviteForCreation.SponsorEmail,
                RoleId = inviteForCreation.RoleId,
                Status = RegistrationCodeStatusType.Created.ToString()
            };

            await this._repo.CreateInvitation(regCode);

            return Ok();

        }


    }
}