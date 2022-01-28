
using System;
using System.IO;
using System.Windows.Media;

namespace GameOfLife
{
    public static class Config
    {
        public static string GetPath()
        {
            string path = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

            path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;

            return path;
        }
    }
}
