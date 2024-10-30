using Microsoft.AspNetCore.Mvc;
using ToDo.BLL.Service;
using ToDo.Models;
using WebAppToDo.Models.ViewModels;

namespace WebAppToDo.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ILogger _logger;
        private readonly IItemService _itemService;

        public ToDoController(ILogger<ToDoController> logger, IItemService itemService)
        {
            _itemService = itemService;
            _logger = logger;   
        }
        public IActionResult ToDo()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VMItem model)
        {
            try
            {
                var newModel = new Item()
                {
                    name = model.name,
                    description = model.description,
                    isCompleted = model.isCompleted,
                    startDate = model.startDate,
                    finishDate = model.finishDate
                };

                bool response = await _itemService.Insert(newModel);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    valor = response
                });

            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> getItems()
        {
            try
            {
                var query = await _itemService.GetAll();

                var list = query.Select(c => new VMItem()
                {
                    id = c.id,
                    name = c.name,
                    description = c.description,
                    isCompleted = c.isCompleted,
                    startDate = c.startDate,
                    finishDate = c.finishDate
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, list);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] VMItem model)
        {
            try
            {
                var newModel = new Item()
                {
                    id = model.id,
                    name = model.name,
                    description = model.description,
                    isCompleted = model.isCompleted,
                    startDate = model.startDate,
                    finishDate = model.finishDate
                };

                bool respuesta = await _itemService.Update(newModel);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    valor = respuesta,
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool response = await _itemService.Delete(id);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    valor = response
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }
    }
}
