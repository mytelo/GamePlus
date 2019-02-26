using System.IO;

namespace WeGamePlus
{
    public class HttpUploadingFile
    {
        public HttpUploadingFile(string fileName, string fieldName)
        {
            this.FileName = fileName;
            this.FieldName = fieldName;
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                this.Data = buffer;
            }
        }

        public HttpUploadingFile(byte[] data, string fileName, string fieldName)
        {
            this.Data = data;
            this.FileName = fileName;
            this.FieldName = fieldName;
        }

        public string FileName { get; set; }

        public string FieldName { get; set; }

        public byte[] Data { get; set; }
    }
}

