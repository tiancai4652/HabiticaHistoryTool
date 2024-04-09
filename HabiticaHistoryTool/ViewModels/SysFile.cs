using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabiticaHistoryTool.ViewModels
{
    public class SysFile
    {
        SysFile() { }

        static SysFile _Instance;
        public static SysFile Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SysFile();
                    _Instance.FilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data", "gem");
                    if (!File.Exists(_Instance.FilePath))
                    {
                        var dic = Path.GetDirectoryName(_Instance.FilePath);
                        if (!Directory.Exists(dic))
                        {
                            Directory.CreateDirectory(dic);
                        }

                    }
                }
                return _Instance;
            }

        }
        public string FilePath { get; set; }
    }
}
