using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace RoM.Code.Editor
{
    public static class EditorScriptsHelper
    {
        /// <summary>
        /// Returns Application.dataPath without "/Assets" substring
        /// </summary>
        /// <returns></returns>
        public static string ProjectPath =>
            Application.dataPath.Substring(0, Application.dataPath.IndexOf("/Assets", StringComparison.Ordinal));

        /// <summary>
        /// Returns scenes paths relative to assets folder
        /// </summary>
        /// <param name="path"></param>
        public static string[] GetAllScenes(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] sceneFiles = d.GetFiles("*.unity");
            return sceneFiles.Select(x => GetPathRelativeToAssets(path) + "/" + x.Name).ToArray();
        }

        public static string GetPathRelativeToAssets(string path)
        {
            return path.Substring(path.IndexOf("Assets", StringComparison.Ordinal));
        }
    }
}