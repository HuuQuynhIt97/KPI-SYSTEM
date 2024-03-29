﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using A4KPI.Constants;
using A4KPI.Data;
using A4KPI.DTO;
using A4KPI.Helpers;
using A4KPI.Models;
using A4KPI._Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using A4KPI._Repositories.Interface;

namespace A4KPI._Services.Services
{
   
    public class ResultOfMonthService :  IResultOfMonthService
    {
        private OperationResult operationResult;

        private readonly IResultOfMonthRepository _repo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MapperConfiguration _configMapper;
        public ResultOfMonthService(
            IResultOfMonthRepository repo,
            IMapper mapper,
             IHttpContextAccessor httpContextAccessor,
            MapperConfiguration configMapper
            )
        {
            _repo = repo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _configMapper = configMapper;
        }

        public async Task<List<ResultOfMonthDto>> GetAllByMonth(int objectiveId, DateTime currentTime)
        {
            var month = currentTime.Month;
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            int accountId = JWTExtensions.GetDecodeTokenById(accessToken);
            return await _repo.FindAll(x => x.CreatedBy == accountId && objectiveId == x.ObjectiveId && month == x.Month).ProjectTo<ResultOfMonthDto>(_configMapper).ToListAsync();

        }
        public async Task<OperationResult> UpdateResultOfMonthAsync(ResultOfMonthRequestDto model)
        {
            try
            {
                var item = await _repo.FindByIdAsync(model.Id);
                if (item == null)
                {
                    _repo.Add(new ResultOfMonth
                    {
                        Month = model.Period,
                        Title = model.Title,
                        ObjectiveId = model.ObjectiveId,
                        CreatedBy = model.CreatedBy
                    });
                }
                else
                {
                    item.ObjectiveId = model.ObjectiveId;
                    item.Title = model.Title;
                    item.CreatedBy = model.CreatedBy;
                    _repo.Update(item);
                }

                await _repo.SaveAll();
                operationResult = new OperationResult
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = MessageReponse.AddSuccess,
                    Success = true,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }
    }
}
