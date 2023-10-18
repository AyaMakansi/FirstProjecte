using FirstProjecte.Services;
using Microsoft.AspNetCore.Mvc;


namespace FirstProjecte.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class EmployeeController :ControllerBase
{
        private readonly IEmployee _employeeservice;
    
        private new List<string> _allowedExtentions = new List<string> { ".png", ".jpg" };
     
        private long _maxAllowedImageSize = 1048576;//1MB

        private readonly IWebHostEnvironment _hostingEnvironment;
        public EmployeeController(IEmployee employeeservices,IWebHostEnvironment hostingEnvironment)
        {
            _employeeservice = employeeservices;
            _hostingEnvironment = hostingEnvironment;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var employee = await _employeeservice.GetAll();
            return Ok(employee);
        }
       
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] EmployeeDto dto)
        {   
                string uploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "images");
                string? filepath = Path.Combine(uploadFolder, dto.Image.FileName);
                using var stream = new FileStream(filepath, FileMode.Create);
               
                   await  dto.Image.CopyToAsync(stream);
                if (!_allowedExtentions.Contains(Path.GetExtension(dto.Image.FileName).ToLower()))
                                return BadRequest("Only .png and .jpg images are allowed!");
               if(dto.Image.Length > _maxAllowedImageSize)
                                return BadRequest("max allowed size for poster is 1MB!");
         
            var employee = new Employee
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Age = dto.Age,
                Address = dto.Address,
                
                Image= filepath,
            };
            await _employeeservice.Add(employee);
            return Ok(employee);
        }
    
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromForm] EmployeeDto dto, int id)
        {  
           
            string uploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "images");
                     string filepath = Path.Combine(uploadFolder, dto.Image.FileName);
                           using var stream = new FileStream(filepath, FileMode.Create);
                          
                              await  dto.Image.CopyToAsync(stream);
                          
                 if(!_allowedExtentions.Contains(Path.GetExtension(dto.Image.FileName).ToLower()))
                                return BadRequest("Only .png and .jpg images are allowed!");
                   if(dto.Image!.Length> _maxAllowedImageSize)
                                return BadRequest("max allowed size for image is 1MB!");
            
          var employee = await _employeeservice.GetById(id);
                 if (employee==null)
                 {
                     return NotFound($"No Employee ID:{id}");
                 }
    
                 employee.FullName = dto.FullName;
                employee.Email = dto.Email;
                 employee.Address=dto.Address;
                 employee.Age = dto.Age;
                 employee.Image =filepath ;
             
             
                 _employeeservice.Update(employee);
                 return Ok(employee);
    
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var employee = await _employeeservice.GetById(id);
            if (employee==null)
            {
                return NotFound($"No Employee ID:{id}");
            }
            
            _employeeservice.Delete(employee);
            return Ok(employee);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name)
        {
            try
            {
                var result = await _employeeservice.Search(name);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
       
    }