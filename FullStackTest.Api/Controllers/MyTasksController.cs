using AutoMapper;
using FullStackTest.Api.Models;
using FullStackTest.Api.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;

namespace FullStackTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyTasksController : ControllerBase
    {
        private readonly IMyTaskService _myTaskService;
        private readonly IMapper _mapper;
        private readonly ILogger<MyTasksController> _logger;

        public MyTasksController(IMyTaskService myTaskService, IMapper mapper, ILogger<MyTasksController> logger)
        {
            _myTaskService = myTaskService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<TasksController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogInformation("Fetching all tasks");

                var result = await _myTaskService.GetTasksAsync();

                return Ok(_mapper.Map<IEnumerable<MyTaskDto>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching tasks");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/<TasksController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching task with ID: {id}");

                if (id <= 0)
                {
                    _logger.LogWarning("Invalid task ID provided: {id}", id);
                    return BadRequest("Invalid task ID.");
                }

                var result = await _myTaskService.GetTaskByIdAsync(id);

                if (result == null)
                    return NotFound();

                return Ok(_mapper.Map<MyTaskDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching task by ID");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST api/<TasksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTaskRequest createTaskRequest)
        {
            try
            {
                _logger.LogInformation("Creating a new task");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for CreateTaskRequest");
                    return BadRequest(ModelState);
                }

                var myTask = _mapper.Map<MyTask>(createTaskRequest);

                var taskId = await _myTaskService.AddTaskAsync(myTask);

                if (taskId <= 0)
                {
                    _logger.LogError("Failed to create task, returned ID is not valid: {taskId}", taskId);
                    return BadRequest("Failed to create task.");
                }

                return CreatedAtAction(nameof(Get), new { id = taskId }, taskId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a task");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT api/<TasksController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateTaskRequest updateTaskRequest)
        {
            try
            {
                _logger.LogInformation($"Updating task with ID: {updateTaskRequest.Id}");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for UpdateTaskRequest");
                    return BadRequest(ModelState);
                }

                var task = await _myTaskService.GetTaskByIdAsync(updateTaskRequest.Id);

                if (task == null)
                {
                    _logger.LogWarning("Task with ID {Id} not found for update", updateTaskRequest.Id);
                    return NotFound();
                }

                var myTask = _mapper.Map<MyTask>(updateTaskRequest);

                await _myTaskService.UpdateTaskAsync(myTask);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a task");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting task with ID: {id}");

                if (id <= 0)
                {
                    _logger.LogWarning("Invalid task ID provided for deletion: {id}", id);
                    return BadRequest("Invalid task ID.");
                }

                var task = await _myTaskService.GetTaskByIdAsync(id);

                if (task == null)
                {
                    _logger.LogWarning("Task with ID {Id} not found for deletion", id);
                    return NotFound();
                }

                await _myTaskService.DeleteTaskAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a task");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
