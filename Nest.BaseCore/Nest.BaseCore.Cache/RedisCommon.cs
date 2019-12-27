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
            return $"token:{token}";
        }

        /// <summary>
        /// 秘钥缓存key
        /// </summary>
        /// <param name="str">AppId等加密串</param>
        /// <returns></returns>
        public static string GetSecretKey(string str)
        {
            return $"secret:{str}";
        }
        #endregion
    }
}
