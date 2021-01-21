using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Timespawn.EntityTween.Editor.CI
{
    public static class BuildUtils
    {
        private const string TEMP_DEFAULT_SCENE_PATH_NAME = "Assets/Temp/DefaultScene.unity";

        public static void BuildDefaultScene()
        {
            string[] args = GetExecuteMethodArguments(typeof(BuildUtils).FullName + "." + nameof(BuildDefaultScene));
            string outputPathName = args.ElementAtOrDefault(0);
            string platform = args.ElementAtOrDefault(1);

            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
            Directory.CreateDirectory(Path.GetDirectoryName(TEMP_DEFAULT_SCENE_PATH_NAME));
            EditorSceneManager.SaveScene(scene, TEMP_DEFAULT_SCENE_PATH_NAME);
            Build(new string[] { TEMP_DEFAULT_SCENE_PATH_NAME }, outputPathName, PlatformToBuildTarget(platform));
        }

        public static void Build(string[] scenes, string outputPathName, BuildTarget buildTarget, BuildOptions buildOptions = BuildOptions.None)
        {
            string log = $"Building for {buildTarget} to {outputPathName} with scenes:";
            foreach (string scene in scenes)
            {
                log += $"\n\t{scene}";
            }

            Debug.Log(log);
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                locationPathName = outputPathName,
                options = buildOptions,
                scenes = scenes,
                target = buildTarget,
                targetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget),
            };
            
            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        }

        private static BuildTarget PlatformToBuildTarget(string platform)
        {
            if (platform.Contains("windows"))
            {
                return BuildTarget.StandaloneWindows64;
            }

            if (platform.Contains("webgl"))
            {
                return BuildTarget.WebGL;
            }

            if (platform.Contains("android"))
            {
                return BuildTarget.Android;
            }

            if (platform.Contains("ios"))
            {
                return BuildTarget.iOS;
            }

            Debug.LogError($"Error parsing the platform string \"{platform}\" to BuildTarget.");

            return BuildTarget.NoTarget;
        }

        private static string[] GetExecuteMethodArguments(string methodFullName)
        {
            List<string> optionArgs = new List<string>();
            string[] allArgs = Environment.GetCommandLineArgs();

            bool isOptionFound = false;
            foreach (string arg in allArgs)
            {
                if (!isOptionFound)
                {
                    if (arg.Length < 2)
                    {
                        continue;
                    }

                    if (arg.ToLower() == methodFullName.ToLower())
                    {
                        isOptionFound = true;
                    }
                }
                else
                {
                    if (arg[0] == '-')
                    {
                        break;
                    }

                    optionArgs.Add(arg);
                }
            }

            return optionArgs.ToArray();
        }
    }
}