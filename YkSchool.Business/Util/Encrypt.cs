using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Util {
    /// <summary>
    /// 加密操作
    /// </summary>
    public class Encrypt {

        #region Md5加密

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        public static string Md5By16( string text ) {
            return Md5By16( text, Encoding.UTF8 );
        }

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">字符编码</param>
        public static string Md5By16( string text,Encoding encoding ) {
            return Md5( text,encoding, 4, 8 );
        }

        /// <summary>
        /// Md5加密
        /// </summary>
        private static string Md5( string text, Encoding encoding,int? startIndex,int? length ) {
            if ( string.IsNullOrWhiteSpace( text ) )
                return string.Empty;
            var md5 = new MD5CryptoServiceProvider();
            string result;
            try {
                if( startIndex == null )
                    result = BitConverter.ToString( md5.ComputeHash( encoding.GetBytes( text ) ) );
                else
                    result = BitConverter.ToString( md5.ComputeHash( encoding.GetBytes( text ) ), startIndex.SafeValue(), length.SafeValue() );
            }
            finally {
                md5.Clear();
            }
            return result.Replace( "-", "" );
        }

        /// <summary>
        /// Md5加密，返回32位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        public static string Md5By32( string text ) {
            return Md5By32( text, Encoding.UTF8 );
        }

        /// <summary>
        /// Md5加密，返回32位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">字符编码</param>
        public static string Md5By32( string text, Encoding encoding ) {
            return Md5( text, encoding, null, null );
        }


        public static string Md5ToUp(string s)
        {
            char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                    'A', 'B', 'C', 'D', 'E', 'F' };
            try
            {

                byte[] btInput = System.Text.Encoding.UTF8.GetBytes(s);
                // 获得MD5摘要算法的 MessageDigest 对象
                MD5 mdInst = System.Security.Cryptography.MD5.Create();
                // 使用指定的字节更新摘要
                mdInst.ComputeHash(btInput);
                // 获得密文
                byte[] md = mdInst.Hash;
                // 把密文转换成十六进制的字符串形式
                int j = md.Length;
                char[] str = new char[j * 2];
                int k = 0;
                for (int i = 0; i < j; i++)
                {
                    byte byte0 = md[i];
                    str[k++] = hexDigits[(int)(((byte)byte0) >> 4) & 0xf];
                    str[k++] = hexDigits[byte0 & 0xf];
                }
                return new string(str);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return null;
            }
        }

        #endregion

         

        #region ========加密========

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string DesEncrypt(string Text)
        {
            return DesEncrypt(Text, "QfPass");
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DesEncrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion

        #region ========解密========

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string DesDecrypt(string Text)
        {
            return DesDecrypt(Text, "QfPass");
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DesDecrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion
    }
}
