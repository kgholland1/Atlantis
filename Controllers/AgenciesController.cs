using AutoMapper;
using AtlantisPortals.API.Entities;
using AtlantisPortals.API.Helpers;
using AtlantisPortals.API.Models;
using AtlantisPortals.API.Parameters;
using AtlantisPortals.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisPortals.API.Controllers
{
    [Route("api/agencies")]
    [ApiController]
    public class AgenciesController : ControllerBase
    {
        private readonly IAgencyRepository _AgenciesRepo;
        private readonly ISystemRepository _systemRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPropertyMappingService _propertyMappingService;

        public AgenciesController(IAgencyRepository agencyRepository, ISystemRepository systemRepository, IMapper mapper,
            IPropertyMappingService propertyMappingService, IUnitOfWork unitOfWork)
        {
            _AgenciesRepo = agencyRepository ??
                throw new ArgumentNullException(nameof(agencyRepository));
            _systemRepo = systemRepository ??
                throw new ArgumentNullException(nameof(systemRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ??
                throw new ArgumentNullException(nameof(propertyMappingService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet(Name = "GetAgencies")]
        public async Task<ActionResult<IEnumerable<AgencyDto>>> GetAgencies(
            [FromQuery] AgenciesParam agenciesParameters)
        {
            if (!_propertyMappingService.ValidMappingExistsFor<AgencyDto, Agency>
                (agenciesParameters.OrderBy))
            {
                return BadRequest();
            }

            var agenciesFromRepo = await _AgenciesRepo.GetAgencies(agenciesParameters);

            var previousPageLink = agenciesFromRepo.HasPrevious ?
                CreateAgenciesResourceUri(agenciesParameters,
                ResourceUriType.PreviousPage) : null;

            var nextPageLink = agenciesFromRepo.HasNext ?
                CreateAgenciesResourceUri(agenciesParameters,
                ResourceUriType.NextPage) : null;

            Response.AddPagination(agenciesFromRepo.CurrentPage, agenciesFromRepo.PageSize, agenciesFromRepo.TotalCount,
                agenciesFromRepo.TotalPages, previousPageLink, nextPageLink);


            return Ok(_mapper.Map<IEnumerable<AgencyDto>>(agenciesFromRepo));
        }

        [HttpGet("{id}", Name = "GetAgency")]
        public async Task<ActionResult<AgencyDto>> GetAgency(int id)
        {
            var agencyFromRepo = await _AgenciesRepo.GetAgency(id);

            if (agencyFromRepo == null)
                return NotFound($"Could not find Agency with an ID of {id}");

            return Ok(_mapper.Map<AgencyDto>(agencyFromRepo));
        }

        [HttpPost()]
        public async Task<ActionResult<AgencyDto>> CreateAgency(AgencyForCreationDto agencyForCreationDto)
        {

            var agencyEntity = _mapper.Map<Agency>(agencyForCreationDto);

            //add payment record
            var receiptType = await _systemRepo.GetReceiptType(agencyForCreationDto.ReceiptTypeId);
            if (receiptType == null)
                return BadRequest("Could not find receiptType.");

            agencyEntity.Payment = new Payment { 
                AmountBalance = 0, 
                CurrentReceiptNumber = 0,
                MinUnitThreshold = (agencyForCreationDto.MinUnitThreshold > 0 ? agencyForCreationDto.MinUnitThreshold : 10),
                Unit = 0,
                ReceiptType = receiptType.Type,
                UnitPerReceiptType = receiptType.UnitPerType
            };

            _AgenciesRepo.Add(agencyEntity);

            if (await _AgenciesRepo.Save())
            {

                var AgencyToReturn = _mapper.Map<AgencyDto>(agencyEntity);
                return CreatedAtRoute("GetAgency", new { AgencyToReturn.Id }, AgencyToReturn);
            }

            throw new Exception("Creating Agency failed on save");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAgency(int id,  AgencyForUpdateDto agencyForUpdateDto)
        {

            var agencyFromRepo = await _AgenciesRepo.GetAgency(id);

            if (agencyFromRepo == null)
                return NotFound($"Could not find Agency with an ID of {id}");

            _mapper.Map<AgencyForUpdateDto, Agency>(agencyForUpdateDto, agencyFromRepo);

            if (await _unitOfWork.CompleteAsync())
                return NoContent();

            throw new Exception($"Updating Agency with {id} failed on save");
        }
        private string CreateAgenciesResourceUri(
            AgenciesParam agenciesParameters,
            ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetAgencies",
                      new
                      {
                          orderBy = agenciesParameters.OrderBy,
                          pageNumber = agenciesParameters.PageNumber - 1,
                          pageSize = agenciesParameters.PageSize,
                          mainCategory = agenciesParameters.Ministry,
                          searchQuery = agenciesParameters.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetAgencies",
                      new
                      {
                          orderBy = agenciesParameters.OrderBy,
                          pageNumber = agenciesParameters.PageNumber + 1,
                          pageSize = agenciesParameters.PageSize,
                          mainCategory = agenciesParameters.Ministry,
                          searchQuery = agenciesParameters.SearchQuery
                      });

                default:
                    return Url.Link("GetAgencies",
                    new
                    {
                        orderBy = agenciesParameters.OrderBy,
                        pageNumber = agenciesParameters.PageNumber,
                        pageSize = agenciesParameters.PageSize,
                        mainCategory = agenciesParameters.Ministry,
                        searchQuery = agenciesParameters.SearchQuery
                    });
            }

        }
    }
}
