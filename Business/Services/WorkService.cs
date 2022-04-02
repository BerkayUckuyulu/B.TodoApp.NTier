using AutoMapper;
using Business.Extensions;
using Business.Interfaces;
using Business.ValidationRules;
using Common.ResponseObjects;
using DataAccess.Interfaces;
using DataAccess.UnitOfWork;
using Dtos.Interfaces;
using Dtos.WorkDtos;
using Entities.Conrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class WorkService : IWorkService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<WorkCreateDto> _createDtoValidator;
        private readonly IValidator<WorkUpdateDto> _updateDtoValidator;

        public WorkService(IUow uow, IMapper mapper, IValidator<WorkCreateDto> createDto, IValidator<WorkUpdateDto> updateDto)
        {
            _uow = uow;
            _mapper = mapper;
            _createDtoValidator = createDto;
            _updateDtoValidator = updateDto;
        }

        public async Task<IResponse<WorkCreateDto>> Create(WorkCreateDto workCreateDto)
        {
            var validationResult = _createDtoValidator.Validate(workCreateDto);


            if (validationResult.IsValid)
            {
                await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(workCreateDto));
                await _uow.SaveChanges();
                return new Response<WorkCreateDto>(ResponseType.Success, workCreateDto);
            }
            else
            {
                //List<CustomValidationError> errors = new List<CustomValidationError>();
                //foreach (var item in validationResult.Errors)
                //{
                //    errors.Add(new CustomValidationError()
                //    {
                //        ErrorMessage = item.ErrorMessage,
                //        PropertyName = item.PropertyName
                //    });
                //}
                //Yukarıdaki kod bloguna özel ConvertToCustomValidationError isimli metodu yazdık.FluentValidationun ValidaitonResultlarına bu metodu extent ettik.

                return new Response<WorkCreateDto>(ResponseType.ValidationError, workCreateDto, validationResult.ConvertToCustomValidationError());
            }



        }

        public async Task<IResponse<List<WorkListDto>>> GetAll()
        {
            //var list= await _uow.GetRepository<Work>().GetAll();
            //var workListDtos = new List<WorkListDto>();

            //if (list !=null && list.Count>0 )
            //{
            //    foreach (var work in list)
            //    {
            //        workListDtos.Add(new WorkListDto() { Definition = work.Definition, Id = work.Id, IsCompleted = work.IsCompleted });
            //    }
            //}
            //return workListDtos;
            var data = _mapper.Map<List<WorkListDto>>(await _uow.GetRepository<Work>().GetAll());
            return new Response<List<WorkListDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var work = await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);
            //return new WorkListDto() { Definition = work.Definition, IsCompleted = work.IsCompleted };
            var data = _mapper.Map<IDto>(work);
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} Bulunamadı.");
            }
            else
                return new Response<IDto>(ResponseType.Success, data);
        }


        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);
            if (removedEntity != null)
            {
                _uow.GetRepository<Work>().Remove(removedEntity);
                await _uow.SaveChanges();

                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id}Bulunamadı.");

            }


        }

        public async Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto workUpdateDto)
        {
            var validateResult = _updateDtoValidator.Validate(workUpdateDto);

            if (validateResult.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Work>().Find(workUpdateDto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Work>().Update(_mapper.Map<Work>(workUpdateDto), updatedEntity);
                    await _uow.SaveChanges();
                    return new Response<WorkUpdateDto>(ResponseType.Success, workUpdateDto);
                }
                return new Response<WorkUpdateDto>(ResponseType.NotFound, $"{workUpdateDto.Id} bulunamadı.");


            }
            else
            {
                //List<CustomValidationError> errors = new List<CustomValidationError>();
                //foreach (var item in validateResult.Errors)
                //{
                //    errors.Add(new CustomValidationError()
                //    {
                //        ErrorMessage = item.ErrorMessage,
                //        PropertyName = item.PropertyName
                //    });
                //}
                return new Response<WorkUpdateDto>(ResponseType.ValidationError, workUpdateDto, validateResult.ConvertToCustomValidationError());

            }

            //_uow.GetRepository<Work>().Update(new Work() { Id = workUpdateDto.Id, Definition = workUpdateDto.Definition, IsCompleted = workUpdateDto.IsCompleted });

        }
    }
}
