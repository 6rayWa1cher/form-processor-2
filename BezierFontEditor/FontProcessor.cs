using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BezierFontEditor
{
    public class FontProcessor
    {
        public BezierFont LoadFont(string path)
        {
            string[] lines = File.ReadAllLines(path);
            StringBuilder sb = new StringBuilder();
            foreach(string line in lines)
            {
                sb.Append(line);
            }
            return JsonConvert.DeserializeObject<BezierFont>(sb.ToString());
        }

        public void SaveFont(string path, BezierFont font)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            string jsonString = JsonConvert.SerializeObject(font);
            using (FileStream fs = File.Create(path))
            {
                AddText(fs, jsonString);
            }
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
