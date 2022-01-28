using System;
using System.IO;

namespace GameOfLife
{
    public static class Config
    {
        public static string GetPath()
        {
            string path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

            path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;

            return path;
        }
    }
}
