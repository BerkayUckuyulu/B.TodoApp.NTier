using AutoMapper;
using Business.Interfaces;
using Business.Services;
using Common.ResponseObjects;
using Dtos.WorkDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Extensions;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkService _workService;


        public HomeController(IWorkService workService)
        {
            _workService = workService;

        }

        public async Task<IActionResult> Index()
        {
            var response = await _workService.GetAll();
            return View(response.Data);
        }

        public IActionResult Create()
        {
            return View(new WorkCreateDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create(WorkCreateDto dto)
        {
            var response = await _workService.Create(dto);
            return this.ResponseRedirectToAction(response, "Index");

            //Alttaki kod blogu controllera extent edilmiş bir metotta.(UI.Extensions)
            //if (response.ResponseType == ResponseType.ValidationError)
            //{
            //    foreach (var error in response.ValidationErrors)
            //    {
            //        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            //    }
            //    return View(dto);
                
            //}

            //else 
            //{
            //    return RedirectToAction("Index");
            //}
           
            
            
        }
               
               
             
        
        public async Task<IActionResult> Update(int id)
        {
           var response=await _workService.GetById<WorkUpdateDto>(id);
            return this.ResponseView(response);


        }
        [HttpPost]
        public async Task<IActionResult> Update(WorkUpdateDto workUpdateDto)
        {

            
               var response= await _workService.Update(workUpdateDto);
            return this.ResponseRedirectToAction(response, "Index");
           

            
        }
        public async Task<IActionResult> Remove(int id)
        {
           var response= await _workService.Remove(id);
            return this.ResponseRedirectToAction(response, "Index");
        }

        public IActionResult NotFound(int code)
        {
            return View();
        }
    }
}
