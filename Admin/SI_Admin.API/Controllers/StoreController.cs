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

    public class StoreController: ControllerBase
    {
        
        private readonly IStoreRepository _repo;
        private readonly IMapper _mapper;

        public StoreController(IStoreRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("packages/selection")]
        public async Task<IActionResult> GetPaquetesSeleccion()
        {

            
            var result = await _repo.GetPackages();
            var resultDTO = _mapper.Map<IEnumerable<PackageForSelectioncsDTO>>(result);

            return Ok(resultDTO);
        }  

        [HttpGet("packages")]
        public async Task<IActionResult> GetPaquetesContCorto()
        {
            var result = await _repo.GetPackages();
            var resultDTO = _mapper.Map<IEnumerable<PackageForSummaryDTO>>(result);

            return Ok(resultDTO);
        }  

        [HttpGet("packages/{id}/detail")]
        public async Task<IActionResult> GetPaquetesContLargo(int id)
        {
            var result = await _repo.GetPackage(id);
            var resultDTO = _mapper.Map<PackageForDetailDTO>(result);

            return Ok(resultDTO);
        }                  
    }
}