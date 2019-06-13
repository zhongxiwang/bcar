using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

namespace WxUilt
{
    public static class Request
    {
        public static async Task<string> Get(string url)
        {
            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
                Request.Method = "GET";
                var response=await Request.GetResponseAsync();

                using (System.IO.StreamReader sr=new System.IO.StreamReader(response.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        
        public static async Task<string> Post (string url,Hashtable par)
        {
            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
                Request.Method = "POST";
                var staream=await Request.GetRequestStreamAsync();
                var b= EncodePars(par);
                Request.ContentLength = b.Length;
                await staream.WriteAsync(b, 0, b.Length);
                staream.Close();
                var response = await Request.GetResponseAsync();

                using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }


        /// <summary>
        /// mvc 4 api的方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static async Task<string> SendPostRequest(string url, IEnumerable<KeyValuePair<string, string>> ds)
        {
            HttpClient myHttpClient = new HttpClient();
            myHttpClient.BaseAddress = new Uri(url);
            var content = new FormUrlEncodedContent(ds);
            HttpResponseMessage response = myHttpClient.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }

        public static async Task<string> Post(string url, string json)
        {
            try
            {
                DateTime dt = DateTime.Now;
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
                Request.Method = "POST";
                var re = DateTime.Now - dt;
                var staream = await Request.GetRequestStreamAsync();
                var b = EncodePars(json);
                Request.ContentLength = b.Length;
                Request.ContentType = "application/json;charset=utf-8";
                
                await staream.WriteAsync(b, 0, b.Length);
                staream.Close();
                var response = await Request.GetResponseAsync();

                using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 传送json数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static async Task<string> SendJsonPost(string url,string json)
        {
            try
            {
                DateTime dt = DateTime.Now;
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
                Request.Method = "POST";
                var re = DateTime.Now - dt;
                var staream = await Request.GetRequestStreamAsync();
                var b = EncodePars(json);
                Request.ContentLength = b.Length;
                Request.ContentType = "application/json;charset=utf-8";

                await staream.WriteAsync(b, 0, b.Length);
                staream.Close();
                var response = await Request.GetResponseAsync();

                using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 格局微信格式上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data">数据流</param>
        /// <param name="filename">filename</param>
        /// <returns></returns>
        public static string Post(string url, byte[] data, string filename)
        {
            string modelId = Guid.NewGuid().ToString();// "4f1e2e3d-6231-4b13-96a4-835e5af10394";
            string updateTime = DateTime.Now.ToShortTimeString();
            //string encrypt = "f933797503d6e2c36762428a280e0559";

            #region 定义请求体中的内容 并转成二进制

            string boundary = "ceshi";
            string Enter = "\r\n";

            string modelIdStr = "--" + boundary + Enter
                    + "Content-Disposition: form-data; name=\"modelId\"" + Enter + Enter
                    + modelId + Enter;

            string fileContentStr = "--" + boundary + Enter
                    + "Content-Type:application/octet-stream" + Enter
                    + "Content-Disposition: form-data; name=\"media\"; filename=\"" + filename + "\"" + Enter + Enter;

            string updateTimeStr = Enter + "--" + boundary + Enter
                    + "Content-Disposition: form-data; name=\"updateTime\"" + Enter + Enter
                    + updateTime;

            //string encryptStr = Enter + "--" + boundary + Enter
            //        + "Content-Disposition: form-data; name=\"encrypt\"" + Enter + Enter
            //        + encrypt + Enter + "--" + boundary + "--";


            var modelIdStrByte = Encoding.UTF8.GetBytes(modelIdStr);//modelId所有字符串二进制

            var fileContentStrByte = Encoding.UTF8.GetBytes(fileContentStr);//fileContent一些名称等信息的二进制（不包含文件本身）

            var updateTimeStrByte = Encoding.UTF8.GetBytes(updateTimeStr);//updateTime所有字符串二进制

            //var encryptStrByte = Encoding.UTF8.GetBytes(encryptStr);//encrypt所有字符串二进制


            #endregion


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "multipart/form-data;boundary=" + boundary;

            Stream myRequestStream = request.GetRequestStream();//定义请求流

            #region 将各个二进制 安顺序写入请求流 modelIdStr -> (fileContentStr + fileContent) -> uodateTimeStr -> encryptStr

            myRequestStream.Write(modelIdStrByte, 0, modelIdStrByte.Length);

            myRequestStream.Write(fileContentStrByte, 0, fileContentStrByte.Length);
            myRequestStream.Write(data, 0, data.Length);

            myRequestStream.Write(updateTimeStrByte, 0, updateTimeStrByte.Length);

            //myRequestStream.Write(encryptStrByte, 0, encryptStrByte.Length);

            #endregion

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();//发送

            Stream myResponseStream = response.GetResponseStream();//获取返回值
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        private static byte[] EncodePars(Hashtable Pars)
        {
            return Encoding.UTF8.GetBytes(ParsToString(Pars));
        }
        private static byte[] EncodePars(string json)
        {
            return Encoding.UTF8.GetBytes(json);
        }
        private static String ParsToString(Hashtable Pars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                sb.Append(k + "=" + Pars[k].ToString());
            }
            return sb.ToString();
        }
    }
}
