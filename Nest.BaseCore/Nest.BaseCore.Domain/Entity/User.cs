using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nest.BaseCore.Domain
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("tbUser")]
    public class User
    {
        [Key]
        public string id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string roleId { get; set; }
        public string name { get; set; }
        public string gateId { get; set; }
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }
    }
}
