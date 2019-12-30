using Nest.BaseCore.Common;
using Nest.BaseCore.Domain;
using Nest.BaseCore.Domain.RequestModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nest.BaseCore.Cache;
using Nest.BaseCore.Domain.ResponseModel;

namespace Nest.BaseCore.Service
{
    public class AppTicketService : IAppTicketService
    {
        private readonly MainContext _db;

        public AppTicketService(MainContext db)
        {
            _db = db;
        }

        /// <summary>
        /// 生成票据
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ApiResultModel<AddAppTicketResponseModel> GetAppTicket(AddAppTicketRequestModel requestModel)
        {
            var result = new ApiResultModel<AddAppTicketResponseModel>() { Message = "生成票据失败" };

            if (requestModel.AppId.IsNullOrEmpty())
            {
                result.Message = "AppId不能为空";
                return result;
            }
            if (requestModel.ClientType.IsNullOrEmpty())
            {
                result.Message = "客户端类型不能为空";
                return result;
            }
            if (requestModel.DeviceNo.IsNullOrEmpty())
            {
                result.Message = "客户端设备号不能为空";
                return result;
            }

            var nonce = Utils.getNonce();
            var ticket = AuthenticationHelper.GetTicket(requestModel.AppId, requestModel.ClientType, requestModel.DeviceNo, nonce);
            var secret = AuthenticationHelper.GetAppSecret(requestModel.AppId, requestModel.ClientType, requestModel.DeviceNo, nonce);
            var resultData = new AddAppTicketResponseModel()
            {
                Ticket = ticket,
                AppSecret = secret
            };
            AppTicket model = _db.AppTicket.FirstOrDefault(x => x.AppId == requestModel.AppId && x.ClientType == requestModel.ClientType && x.DeviceNo == requestModel.DeviceNo);
            if (model == null)
            {
                model = new AppTicket()
                {
                    Id = GuidTool.GetGuid(),
                    AppId = requestModel.AppId,
                    ClientType = requestModel.ClientType,
                    DeviceNo = requestModel.DeviceNo,
                    Noncestr = nonce,
                    AppSecret = secret,
                    Ticket = ticket,
                    LastUpdateTime = DateTime.Now
                };
                _db.AppTicket.Add(model);
                _db.Entry(model).State = EntityState.Added;
                _db.SaveChanges();
            }
            else
            {
                model.Noncestr = nonce;
                model.AppSecret = secret;
                model.Ticket = ticket;
                model.LastUpdateTime = DateTime.Now;

                _db.AppTicket.Attach(model);
                _db.Entry(model).Property(x => x.Noncestr).IsModified = true;
                _db.Entry(model).Property(x => x.AppSecret).IsModified = true;
                _db.Entry(model).Property(x => x.Ticket).IsModified = true;
                _db.Entry(model).Property(x => x.LastUpdateTime).IsModified = true;
                _db.SaveChanges();
            }

            //缓存
            var redisKey = RedisCommon.GetTicketKey(ticket);
            var redisData = model.MapTo<AppTicketModel>();
            RedisClient.Set(RedisDatabase.DB_AuthorityService, redisKey, redisData, 60);//1小时

            result.Data = resultData; ;
            result.Code = ApiResultCode.Success;
            return result;
        }
    }
}
