using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.Optimization;

namespace SPE
{
    public class BundleMe
    {
        public static void AutoBundle()
        {
            //AUTOBUNDLE CONFIG
            ScriptBundle BundleCommon = new ScriptBundle("~/AutoBundle/Common");

            if (Directory.Exists(HttpContext.Current.Server.MapPath("~/ScriptBundle/$Common")))
            {
                foreach (string includeFile in Directory.EnumerateFiles(HttpContext.Current.Server.MapPath("~/ScriptBundle/$Common")))
                {
                    string fileFinal = (from i in includeFile.Split('\\') select i).Last();
                    BundleCommon.Include("~/ScriptBundle/$Common" + fileFinal);
                }
            }

            BundleTable.Bundles.Add(BundleCommon);

            foreach (string dir in Directory.GetDirectories(HttpContext.Current.Server.MapPath("~/Views")))
            {
                string dirName = dir.Split('\\').Last();
                bool existFiles = false;
                ScriptBundle BundleController = new ScriptBundle("~/AutoBundle/" + dirName);
                if (Directory.Exists(HttpContext.Current.Server.MapPath("~/ScriptBundle/" + dirName + "/$Common")))
                {
                    foreach (string includeFile in Directory.EnumerateFiles(HttpContext.Current.Server.MapPath("~/ScriptBundle/" + dirName + "/_Common")))
                    {
                        existFiles = true;
                        string fileFinal = includeFile.Split('\\').Last();
                        BundleController.Include("~/ScriptBundle/" + dirName + "/$Common/" + fileFinal);
                    }
                }
                if (existFiles)
                {
                    BundleTable.Bundles.Add(BundleController);
                }

                foreach (string file in Directory.EnumerateFiles(dir))
                {
                    string fileName = file.Split('\\').Last().Split('.').First();
                    bool existFilesAction = false;
                    ScriptBundle BundleAction = new ScriptBundle("~/AutoBundle/" + dirName + "/" + fileName);
                    if (Directory.Exists(HttpContext.Current.Server.MapPath("~/ScriptBundle/" + dirName + "/" + fileName)))
                    {
                        foreach (string includeFile in Directory.EnumerateFiles(HttpContext.Current.Server.MapPath("~/ScriptBundle/" + dirName + "/" + fileName)))
                        {
                            existFilesAction = true;
                            string fileFinal = includeFile.Split('\\').Last();
                            BundleAction.Include("~/ScriptBundle/" + dirName + "/" + fileName + "/" + fileFinal);
                        }
                    }

                    if (existFilesAction)
                    {
                        BundleTable.Bundles.Add(BundleAction);
                    }

                };
            };
        }
    }
}