namespace Nest.BaseCore.Cache
{
    /// <summary>
    /// Redis缓存键 帮助类
    /// </summary>
    public class RedisCommon
    {
        #region 缓存键配置

        /// <summary>
        /// 登录缓存key
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetTokenKey(string token)
        {
            return $"token_{token}";
        }

        /// <summary>
        /// 缓存车辆路径
        /// </summary>
        /// <param name="carNo">车牌</param>
        /// <param name="direction">行驶方向0上学1放学</param>
        /// <returns></returns>
        public static string GetCarPath(string carNo,int direction)
        {
            return $"CarPath_{carNo}_{direction}";
        }
        #endregion
    }
}
