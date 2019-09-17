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

        #endregion
    }
}
