using AutoMapper;
using Hahn.Application;
using Hahn.Application.DTOs;
using Hahn.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriverController : ControllerBase
{
    private readonly IRepository<Driver> _repository;
    private readonly IMapper _mapper;

    public DriverController(IRepository<Driver> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<DriverDto>> GetAll()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<DriverDto>>(entities);
    }

    [HttpGet("{id}")]
    public async Task<DriverDto> GetById(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<DriverDto>(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create(DriverDto dto)
    {
        var entity = _mapper.Map<Driver>(dto);
        await _repository.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, DriverDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        _mapper.Map(dto, entity);
        await _repository.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
