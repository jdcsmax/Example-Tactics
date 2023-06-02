using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using log4net.Config;
using log4net.Unity.Config;
using UnityEngine;

namespace Zinnor.Extensions
{
    public class Log4netConfigurator : IConfigurator
    {
        private const string LogDirectoryName = "Logs";
        private const string LogPathVariableName = "LogOutputPath";
        private const string LogNameVariableName = "LogOutputName";

        private string[] _filePaths;

        public int Order => int.MaxValue - 100;

        public event Action OnChange;

        public void CallChange()
        {
            OnChange?.Invoke();
        }

        public void TryConfigure()
        {
            var document = Document;

            if (document == null)
            {
                return;
            }

            ConfigureEnvironmentVariables();

            XmlConfigurator.Configure(document.DocumentElement);
        }

        private XmlDocument Document
        {
            get
            {
                var files = FilePaths;

                for (var i = 0; i <= files.Length - 1; i++)
                {
                    var file = files[i];

                    try
                    {
                        if (!File.Exists(file))
                        {
                            continue;
                        }

                        var text = File.ReadAllText(file, Encoding.UTF8);
                        var doc = new XmlDocument();
                        doc.LoadXml(text);

                        if (doc.DocumentElement?.Name != "log4net")
                        {
                            continue;
                        }

                        return doc;
                    }
                    catch
                    {
                        //
                    }
                }

                return null;
            }
        }

        private string[] FilePaths
        {
            get
            {
                if (_filePaths != null)
                {
                    return _filePaths;
                }

                ISet<string> filePathSet = new HashSet<string>();

                foreach (string path in Paths)
                {
                    var fullPath = Path.GetFullPath(path);

                    filePathSet.Add(Path.Combine(fullPath,
                        $"logger.{(Application.isEditor ? "editor" : "runtime")}.xml"));
                }

                _filePaths = filePathSet.ToArray();

                return _filePaths;
            }
        }

        private string OutputPath =>
#if UNITY_EDITOR
            Environment.CurrentDirectory + Path.DirectorySeparatorChar + LogDirectoryName;
#else
            Application.persistentDataPath + Path.DirectorySeparatorChar + LogDirectoryName;
#endif

        private string OutputName => Application.productName;

        private string[] Paths => new[]
        {
            Application.dataPath,
            Application.persistentDataPath
        };

        private void ConfigureEnvironmentVariables()
        {
            if (!OutputPath.Equals(VariableUtils.GetEnvironmentVariable(LogPathVariableName)))
            {
                VariableUtils.SetEnvironmentVariable(LogPathVariableName, OutputPath);
            }

            if (!OutputName.Equals(VariableUtils.GetEnvironmentVariable(LogNameVariableName)))
            {
                VariableUtils.SetEnvironmentVariable(LogNameVariableName, OutputName);
            }
        }
    }
}