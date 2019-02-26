using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace WeGamePlus
{
    public class HttpClient
    {
        public event EventHandler<StatusUpdateEventArgs> StatusUpdate;

        public HttpClient() : this(null)
        {
        }

        public HttpClient(string url) : this(url, null)
        {
        }

        public HttpClient(string url, HttpClientContext context) : this(url, context, false)
        {
        }

        public HttpClient(string url, HttpClientContext context, bool keepContext)
        {
            this.DefaultLanguage = "zh-CN";
            this.DefaultEncoding = Encoding.UTF8;
            this.Accept = "*/*";
            this.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            this.Files = new List<HttpUploadingFile>();
            this.PostingData = new Dictionary<string, string>();
            this.Url = url;
            this.Context = context;
            this.KeepContext = keepContext;
            if (this.Context == null)
            {
                this.Context = new HttpClientContext();
            }
        }

        public void AttachFile(string fileName, string fieldName)
        {
            HttpUploadingFile item = new HttpUploadingFile(fileName, fieldName);
            this.Files.Add(item);
        }

        public void AttachFile(byte[] data, string fileName, string fieldName)
        {
            HttpUploadingFile item = new HttpUploadingFile(data, fileName, fieldName);
            this.Files.Add(item);
        }

        private HttpWebRequest CreateRequest()
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(this.Url);
            request.AllowAutoRedirect = false;
            request.CookieContainer = new CookieContainer();
            request.Headers.Add("Accept-Language", this.DefaultLanguage);
            request.Accept = this.Accept;
            request.UserAgent = this.UserAgent;
            request.KeepAlive = false;
            if (this.Context.Cookies != null)
            {
                request.CookieContainer.Add(this.Context.Cookies);
            }
            if (!string.IsNullOrEmpty(this.Context.Referer))
            {
                request.Referer = this.Context.Referer;
            }
            if (this.Verb == HttpVerb.HEAD)
            {
                request.Method = "HEAD";
                return request;
            }
            if ((this.PostingData.Count > 0) || (this.Files.Count > 0))
            {
                this.Verb = HttpVerb.POST;
            }
            if (this.Verb == HttpVerb.POST)
            {
                request.Method = "POST";
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                if (this.Files.Count > 0)
                {
                    string str2 = "\r\n";
                    string str3 = Guid.NewGuid().ToString().Replace("-", "");
                    request.ContentType = "multipart/form-data; boundary=" + str3;
                    foreach (string str4 in this.PostingData.Keys)
                    {
                        writer.Write("--" + str3 + str2);
                        writer.Write("Content-Disposition: form-data; name=\"{0}\"{1}{1}", str4, str2);
                        writer.Write(this.PostingData[str4] + str2);
                    }
                    foreach (HttpUploadingFile file in this.Files)
                    {
                        writer.Write("--" + str3 + str2);
                        writer.Write("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", file.FieldName, file.FileName, str2);
                        writer.Write("Content-Type: application/octet-stream" + str2 + str2);
                        writer.Flush();
                        stream.Write(file.Data, 0, file.Data.Length);
                        writer.Write(str2);
                        writer.Write("--" + str3 + str2);
                    }
                }
                else
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                    StringBuilder builder = new StringBuilder();
                    foreach (string str5 in this.PostingData.Keys)
                    {
                        builder.AppendFormat("{0}={1}&", UrlEncode(str5), UrlEncode(this.PostingData[str5]));
                    }
                    if (builder.Length > 0)
                    {
                        builder.Length--;
                    }
                    writer.Write(builder.ToString());
                }
                Random random = new Random();
                object[] objArray1 = new object[] { random.Next(0, 0xff), ".", random.Next(0, 0xff), ".", random.Next(0, 0xff), ".", random.Next(0, 0xff) };
                string str = string.Concat(objArray1);
                request.Headers["X-Forwarded-For"] = str;
                request.Headers["Client_Ip"] = str;
                writer.Flush();
                using (Stream stream2 = request.GetRequestStream())
                {
                    stream.WriteTo(stream2);
                }
            }
            if ((this.StartPoint != 0) && (this.EndPoint != 0))
            {
                request.AddRange(this.StartPoint, this.EndPoint);
                return request;
            }
            if ((this.StartPoint != 0) && (this.EndPoint == 0))
            {
                request.AddRange(this.StartPoint);
            }
            return request;
        }

        public byte[] GetBytes()
        {
            HttpWebResponse response = this.GetResponse();
            int contentLength = (int) response.ContentLength;
            MemoryStream stream = new MemoryStream();
            byte[] buffer = new byte[0x100];
            Stream responseStream = response.GetResponseStream();
            if (responseStream != null)
            {
                for (int i = responseStream.Read(buffer, 0, buffer.Length);
                    i > 0;
                    i = responseStream.Read(buffer, 0, buffer.Length))
                {
                    stream.Write(buffer, 0, i);
                    this.OnStatusUpdate(new StatusUpdateEventArgs((int) stream.Length, contentLength));
                }

                responseStream.Close();
            }

            return stream.ToArray();
        }

        private string GetEncodingFromBody(byte[] data)
        {
            string str = null;
            string str2 = Encoding.ASCII.GetString(data);
            {
                int index = str2.IndexOf("charset=", StringComparison.Ordinal);
                if (index != -1)
                {
                    int num2 = str2.IndexOf("\"", index, StringComparison.Ordinal);
                    if (num2 != -1)
                    {
                        int startIndex = index + 8;
                        str = str2.Substring(startIndex, (num2 - startIndex) + 1);
                        char[] trimChars = { '>', '"' };
                        str = str.TrimEnd(trimChars);
                    }
                }
            }
            return str;
        }

        private string GetEncodingFromHeaders()
        {
            string str = null;
            string str2 = this.ResponseHeaders["Content-Type"];
            if (str2 != null)
            {
                int index = str2.IndexOf("charset=", StringComparison.Ordinal);
                if (index != -1)
                {
                    str = str2.Substring(index + 8);
                }
            }
            return str;
        }

        public HttpWebResponse GetResponse()
        {
            HttpWebResponse response = (HttpWebResponse) this.CreateRequest().GetResponse();
            this.ResponseHeaders = response.Headers;
            if (this.KeepContext)
            {
                this.Context.Cookies = response.Cookies;
                this.Context.Referer = this.Url;
            }
            return response;
        }

        public Stream GetStream() => 
            this.GetResponse().GetResponseStream();

        public string GetString()
        {
            Encoding defaultEncoding;
            byte[] bytes = this.GetBytes();
            string encodingFromHeaders = this.GetEncodingFromHeaders();
            if (encodingFromHeaders == null)
            {
                encodingFromHeaders = this.GetEncodingFromBody(bytes);
            }
            if (encodingFromHeaders == null)
            {
                defaultEncoding = this.DefaultEncoding;
            }
            else
            {
                try
                {
                    defaultEncoding = Encoding.GetEncoding(encodingFromHeaders);
                }
                catch (ArgumentException)
                {
                    defaultEncoding = this.DefaultEncoding;
                }
            }
            return defaultEncoding.GetString(bytes);
        }

        public string GetString(Encoding encoding)
        {
            byte[] bytes = this.GetBytes();
            return encoding.GetString(bytes);
        }

        public int HeadContentLength()
        {
            this.Reset();
            HttpVerb verb = this.Verb;
            this.Verb = HttpVerb.HEAD;
            using (HttpWebResponse response = this.GetResponse())
            {
                this.Verb = verb;
                return (int) response.ContentLength;
            }
        }

        private void OnStatusUpdate(StatusUpdateEventArgs e)
        {
            EventHandler<StatusUpdateEventArgs> statusUpdate = this.StatusUpdate;
            if (statusUpdate != null)
            {
                statusUpdate(this, e);
            }
        }

        public void Reset()
        {
            this.Verb = HttpVerb.GET;
            this.Files.Clear();
            this.PostingData.Clear();
            this.ResponseHeaders = null;
            this.StartPoint = 0;
            this.EndPoint = 0;
        }

        public void SaveAsFile(string fileName)
        {
            this.SaveAsFile(fileName, FileExistsAction.Overwrite);
        }

        public bool SaveAsFile(string fileName, FileExistsAction existsAction)
        {
            byte[] bytes = this.GetBytes();
            if (existsAction != FileExistsAction.Overwrite)
            {
                if (existsAction == FileExistsAction.Append)
                {
                    using (BinaryWriter writer2 = new BinaryWriter(new FileStream(fileName, FileMode.Append, FileAccess.Write)))
                    {
                        writer2.Write(bytes);
                    }
                    return true;
                }
                if (System.IO.File.Exists(fileName))
                {
                    return false;
                }
                using (BinaryWriter writer3 = new BinaryWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write)))
                {
                    writer3.Write(bytes);
                }
                return true;
            }
            using (BinaryWriter writer = new BinaryWriter(new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write)))
            {
                writer.Write(bytes);
            }
            return true;
        }

        public static string UrlEncode(string str)
        {
            StringBuilder builder = new StringBuilder();
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append("%" + Convert.ToString(bytes[i], 0x10));
            }
            return builder.ToString();
        }

        public bool KeepContext { get; set; }

        public string DefaultLanguage { get; set; }

        public Encoding DefaultEncoding { get; set; }

        public HttpVerb Verb { get; set; }

        public List<HttpUploadingFile> Files { get; }

        public Dictionary<string, string> PostingData { get; }

        public string Url { get; set; }

        public WebHeaderCollection ResponseHeaders { get; private set; }

        public string Accept { get; set; }

        public string UserAgent { get; set; }

        public HttpClientContext Context { get; set; }

        public int StartPoint { get; set; }

        public int EndPoint { get; set; }
        
    }
}

