using AutoMapper;
using EnsekCodingTest.Interface;
using EnsekCodingTest.Models;
using EnsekCodingTest.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace EnsekCodingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private readonly ILogger<MeterReadingController> _logger;
        private readonly IMeterReadingRepository _repo;
        private readonly EnsekDbContext _context;
        private readonly IMapper _mapper;

        public MeterReadingController(ILogger<MeterReadingController> logger, IMeterReadingRepository repo, EnsekDbContext context, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("meter-reading-uploads")]
        public IActionResult AddCsvFileToDb(IFormFile uploadFile)
        {
            string[] response = new string[2];
            try
            {
                if (ModelState.IsValid)
                {
                    response = _repo.CheckMeterReadings(uploadFile);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, null);
            }
            return Created($"Records inserted successfully , Success Entries: {response[0]},Failed Entries: {response[1]}", null);
        }

        [HttpGet]
        [Route("get-test-account-data")]
        public IActionResult GetAllTestAccountRecords()
        {
            List<TestAccount> objTestAccountList = new List<TestAccount>();
            try
            {
                objTestAccountList = _repo.GetTestAccountDeatils();

                var objDtoList = new List<TestAccountDto>();

                foreach (var item in objTestAccountList)
                {
                    objDtoList.Add(_mapper.Map<TestAccountDto>(item));
                }
                return Ok(objDtoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
                return StatusCode(500);
            }
        }
    }
}

