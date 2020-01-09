using AutoMapper;
using AtlantisPortals.API.Models;
using AtlantisPortals.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Controllers
{
    [Route("api/system")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISystemRepository _SystemRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SystemController(ISystemRepository systemRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _SystemRepo = systemRepository ??
                throw new ArgumentNullException(nameof(systemRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }
        [HttpGet("ministryLookup")]
        public async Task<ActionResult<IEnumerable<MinistryDto>>> GetMinistryLookup()
        {
            var ministries = await _SystemRepo.GetMinistryLookups();

            var ministriesToReturn = _mapper.Map<IEnumerable<MinistryDto>>(ministries);

            return Ok(ministriesToReturn);
        }
        [HttpGet("receiptTypeLookup")]
        public async Task<ActionResult<IEnumerable<ReceiptTypeDto>>> GetReceiptTypeLookup()
        {
            var typesFromRepo = await _SystemRepo.GetReceiptTypeLookups();

            var typesToReturn = _mapper.Map<IEnumerable<ReceiptTypeDto>>(typesFromRepo);

            return Ok(typesToReturn);
        }
        [AllowAnonymous]
        [HttpGet("allLookups")]
        public async Task<IActionResult> GetAllLookups()
        {
            var ministriesFromRepo = await _SystemRepo.GetMinistryLookups();

            var ministries = _mapper.Map<IEnumerable<MinistryDto>>(ministriesFromRepo);

            var typesFromRepo = await _SystemRepo.GetReceiptTypeLookups();

            var receiptTypes = _mapper.Map<IEnumerable<ReceiptTypeDto>>(typesFromRepo);

            return Ok(new { ministries, receiptTypes });
        }
    }
}
