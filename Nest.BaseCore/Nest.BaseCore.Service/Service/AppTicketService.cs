using Nest.BaseCore.Common;
using Nest.BaseCore.Domain;
using Nest.BaseCore.Domain.RequestModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nest.BaseCore.Cache;

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
        /// 获取秘钥
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ApiResultModel<string> GetAppSecret(AddAppTicketRequestModel requestModel)
        {
            var result = new ApiResultModel<string>();

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

            var nons = Utils.getNoncestr();
            var secret = MD5Helper.GetMd5($"{requestModel.AppId}{requestModel.ClientType}{nons}");

            var ticket = _db.AppTicket.FirstOrDefault(x => x.AppId == requestModel.AppId && x.ClientType == requestModel.ClientType);
            if (ticket == null)
            {
                ticket = new AppTicket()
                {
                    Id = GuidTool.GetGuid(),
                    AppId = requestModel.AppId,
                    ClientType = requestModel.ClientType,
                    Noncestr = nons,
                    AppSecret = secret,
                    LastUpdateTime = DateTime.Now
                };
                _db.AppTicket.Add(ticket);
                _db.Entry(ticket).State = EntityState.Added;
                _db.SaveChanges();
            }
            else
            {
                ticket.Noncestr = nons;
                ticket.AppSecret = secret;
                ticket.LastUpdateTime = DateTime.Now;

                _db.AppTicket.Attach(ticket);
                _db.Entry(ticket).Property(x => x.Noncestr).IsModified = true;
                _db.Entry(ticket).Property(x => x.AppSecret).IsModified = true;
                _db.Entry(ticket).Property(x => x.LastUpdateTime).IsModified = true;
                _db.SaveChanges();
            }

            //缓存
            var redisKey = RedisCommon.GetSecretKey(MD5Helper.GetMd5($"{requestModel.AppId}{requestModel.ClientType}"));
            RedisClient.Set(RedisDatabase.DB_AuthorityService, secret, redisKey, 60);//1小时

            result.Data = secret;
            result.Code = ApiResultCode.Success;
            return result;
        }
    }
}
