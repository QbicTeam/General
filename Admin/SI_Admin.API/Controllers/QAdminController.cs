using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SI_Admin.API.DTO;
using SI_Admin.API.Data;
//using SI_Admin.API.Model;
using Framework.DataTypes.Model.Base;
using Framework.DataTypes.Model.Licenciamiento;
using Framework.DataTypes.Model.Infraestructura;

namespace SI_Admin.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class QAdminController: ControllerBase
    {
        
        private readonly IQAdminRepository _repo;
        private readonly IMapper _mapper;

        public QAdminController(IQAdminRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
/*
        [HttpGet("{codeID}")]
        public async Task<IActionResult> GetReviews(string codeID)
        {
            // var users = await _repo.GetUsers(userParams);
            // var usersToReturn = _mapper.Map<IEnumerable<UsersForListDto>>(users);

            // Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            var reviews = await _repo.GetReviews(codeID);
            var reviewsDTO = _mapper.Map<IEnumerable<ReviewListDTO>>(reviews);

            return Ok(reviewsDTO);
        }      
        [HttpGet("{codeID}/summary")]
        public async Task<IActionResult> GetSumary(string codeID)
        {
            // Debe de sacar un promedio de los reviews del codigo solicitadp
            var reviews = await _repo.GetSumary(codeID);
            var sum = new Summary();
            
            sum.QuantityReviews = reviews.Count();
            if (sum.QuantityReviews == 0) 
                return Ok(sum);    
        
            // sum.Avarage = Convert.ToInt32(reviews.Average(r => r.Rank));
            var avg = reviews.Average(r => r.Rank);
            var e = Math.Truncate(avg);
            var d = avg - e;
            var r = 0.0;
            if (d > .5) r = e + 1.0;
            if (d < .5) r = e;
            if (d == .5) r = avg;

            sum.Avarage = r;

             return Ok(sum);
        }  

        //[HttpPost("{id}/Like/{recipientId}")]
        [HttpPost()]
        public async Task<IActionResult> SaveReview([FromBody]ReviewForCreateDTO review)
        {

            var rvw = _mapper.Map<Review>(review);
            rvw.Created = DateTime.Now;

            _repo.Add<Review>(rvw);

            if (await _repo.SaveAll())
            {
                // foreach(ReviewFeatureForCreateDTO f in review.Features){
                //     var rf = _mapper.Map<ReviewFeature>(f);
                //     _repo.Add<ReviewFeature>(rf);
                // }

                // await _repo.SaveAll();
                return Ok();

            }
                
            return BadRequest("Not Saved");
        }
        */

        [HttpPost("clientes")]
        public async Task<IActionResult> SaveCliente([FromBody]ClienteParaRegistroDTO cliente)
        {

            var cli = _mapper.Map<Cliente>(cliente);
            //rvw.Created = DateTime.Now;
            var paq = await _repo.GetPaquete(cliente.PaqueteId);
            cli.Licencia = new Licencia() {
                PaqueteInicial = paq,
                CostoInicial = paq.Costo,
                NumUsuariosTotal = paq.NumUsuarios,
                NumNegociosTotal = paq.NumNegocios,
                // Apps = _mapper.Map<ICollection<LicenciaApp>>(paq.Apps), //paq.Apps,
                Apps = _mapper.Map<ICollection<PaqueteApp>, ICollection<LicenciaApp>>(paq.Apps), 
                CostoTotalActual = paq.Costo,
                FechaAlta = DateTime.Now
            };
            //cli.Licencia.Apps = _mapper.Map<ICollection<LicenciaApp>>(paq.Apps);

            var cliAct = new ClienteActualizacion() {
                Tipo = 1,
                Cliente = cli,
                // Apps = _mapper.Map<ICollection<ClienteActualizacionApp>>(paq.Apps), //paq.Apps,
                Apps = _mapper.Map<ICollection<PaqueteApp>, ICollection<ClienteActualizacionApp>>(paq.Apps), 
                Fecha = DateTime.Now,
                Status = 1

            };


            _repo.Add<Cliente>(cli);
            _repo.Add<ClienteActualizacion>(cliAct);

            if (await _repo.SaveAll())
            {
                return  Ok();

            }
                
            return BadRequest("Not Saved");
        }

        [HttpGet("paquetes")]
        public async Task<IActionResult> GetPaquetes()
        {
            var result = await _repo.GetPaquetes();
            var resultDTO = _mapper.Map<IEnumerable<PaqueteParaLista>>(result);

            return Ok(resultDTO);
        }


        [HttpGet("clientes/actualizaciones/{status}")]
        public async Task<IActionResult> GetClienteActualizaciones(int status)
        {
            var result = await _repo.GetActualizacionesClientes(status);
            var resultDTO = _mapper.Map<IEnumerable<ActualizacionClienteParaListaDTO>>(result);

            return Ok(resultDTO);
        }  

        [HttpGet("clientes/{cliId}/actualizaciones/{actId}")]
        public async Task<IActionResult> GetClienteActualizacion(int cliId, int actId)
        {
            var result = await _repo.GetActualizacionCliente(actId);
            if (result.ClienteId != cliId)
                return Conflict("Cliente de solicitud y del dato no cuadran");

            var resultDTO = _mapper.Map<ClienteActualizacionDTO>(result);

            return Ok(resultDTO);
        }        

        [HttpPost("clientes/{id}/negocio")]
        public async Task<IActionResult> AddClienteNegocio(int id, [FromBody]ClienteNegocioParaCreacionDTO negocio)
        {

            var neg = _mapper.Map<ClienteNegocio>(negocio);
            
            var cli = await _repo.GetCliente(negocio.ClienteId);
            cli.Negocios.Add(neg);

            if (await _repo.SaveAll())
            {
                return  Ok();
            }
                
            return BadRequest("Not Saved");
        }        

        [HttpPut("clientes/{cliId}/actualizaciones/{actId}")]
        public async Task<IActionResult> PutClienteActualizacion(int cliId, int actId, [FromBody]ClienteActualizacionParaActualizacionDTO cliAct)
        {
            var result = await _repo.GetActualizacionCliente(actId);
            if (result.ClienteId != cliId)
                return Conflict("Cliente de solicitud y del dato no cuadran");

            // Ahorita solo cambiara el estatus a Operado
            result.Status = cliAct.Status;    

            if (await _repo.SaveAll())
            {
                return  Ok();
            }
                
            return BadRequest("Not Saved");
        }             
    }
}