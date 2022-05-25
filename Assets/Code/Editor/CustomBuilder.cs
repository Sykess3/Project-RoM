using System.Diagnostics;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace RoM.Code.Editor
{
    public static class CustomBuilder
    {
        public const string SERVER_EXE_PATH_RELATIVE_TO_PROJECT_PATH_WINDOWS = "./builds/windows/server/Server.exe";
        public const string CLIENT_EXE_PATH_RELATIVE_TO_PROJECT_PATH_WINDOWS = "./builds/windows/client/RoM.exe";
        
        public static readonly string ScenesInBuildPath = Application.dataPath + "/Scenes/BuildInclusive";

        public static readonly string ServerExeFullPath =
            EditorScriptsHelper.ProjectPath + SERVER_EXE_PATH_RELATIVE_TO_PROJECT_PATH_WINDOWS.Substring(1);

        public static readonly string ClientExeFullPath =
            EditorScriptsHelper.ProjectPath + CLIENT_EXE_PATH_RELATIVE_TO_PROJECT_PATH_WINDOWS.Substring(1);

        [MenuItem("Build/Server-Windows")]
        public static void PerformBuildWindows_Server()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(NamedBuildTarget.Server, BuildTarget.StandaloneWindows);
            EditorUserBuildSettings.standaloneBuildSubtarget = StandaloneBuildSubtarget.Server;

            string[] scenes = EditorScriptsHelper.GetAllScenes(ScenesInBuildPath);
            BuildPipeline.BuildPlayer(scenes, SERVER_EXE_PATH_RELATIVE_TO_PROJECT_PATH_WINDOWS, BuildTarget.StandaloneWindows,
                BuildOptions.None);
        }
        
        [MenuItem("Build/Windows")]
        public static void PerformBuildPC()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
            EditorUserBuildSettings.standaloneBuildSubtarget = StandaloneBuildSubtarget.Player;
            
            string[] scenes = EditorScriptsHelper.GetAllScenes(ScenesInBuildPath);
            BuildPipeline.BuildPlayer(scenes, CLIENT_EXE_PATH_RELATIVE_TO_PROJECT_PATH_WINDOWS, BuildTarget.StandaloneWindows, BuildOptions.None);
        }
        
        [MenuItem("Multiplayer/StartServer")]
        public static void StartServer()
        {
            Process.Start(ServerExeFullPath);
        }
        
        [MenuItem("Multiplayer/StartClient")]
        public static void StartClient()
        {
            Process.Start(ClientExeFullPath);
        }
    }
}