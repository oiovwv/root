using System;
using System.IO;
using System.Text;

namespace Biz
{
    public class CText
    {
        private string m_strFilePath;
        private StreamWriter m_streamWriter;

        public string Path
        {
            set { m_strFilePath = value; }
            get { return m_strFilePath; }
        }

        public void Open()
        {
            Open(m_strFilePath);
        }

        public void Open(string fullPath)
        {
            m_strFilePath = fullPath;
            if (File.Exists(fullPath) == true)
            {
                m_streamWriter = File.AppendText(fullPath);
            }
            else
            {
                m_streamWriter = new StreamWriter(fullPath, true, Encoding.UTF8);
            }
        }

        public void WriteLine(string text)
        {
            m_streamWriter.WriteLine(text);
        }

        public void WriteLog(string text)
        {
            m_streamWriter.WriteLine(DateTime.Now.ToString() + "|" + text);
        }

        public void Close()
        {
            m_streamWriter.Close();
        }

        ~CText()
        {
            m_streamWriter = null;
        }
    }
}
