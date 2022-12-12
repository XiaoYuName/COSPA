#if (UNITY_IOS || UNITY_TVOS) && UNITY_2017_1_OR_NEWER
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;

//-----------------------------------------------------------------------------
// Copyright 2012-2021 RenderHeads Ltd.  All rights reserved.
//-----------------------------------------------------------------------------

namespace RenderHeads.Media.AVProVideo.Editor
{
	public class PostProcessBuild_iOS
	{
		const string PluginName = "AVProVideo.framework";

		private class Platform
		{
			public static Platform iOS  = new Platform(BuildTarget.iOS,      "iOS",  "2a1facf97326449499b63c03811b1ab2");
			public static Platform tvOS = new Platform(BuildTarget.tvOS,     "tvOS", "bcf659e3a94d748d6a100d5531540d1a");

			public BuildTarget target { get; }
			public string      name   { get; }
			public string      guid   { get; }

			public static Platform GetPlatformForTarget(BuildTarget target)
			{
				switch (target)
				{
					case BuildTarget.iOS:
						return Platform.iOS;
					
					case BuildTarget.tvOS:
						return Platform.tvOS;
					
					default:
						return null;
				}
			}

			private Platform(BuildTarget target, string name, string guid)
			{
				this.target = target;
				this.name = name;
				this.guid = guid;
			}
		}

		private static string PluginPathForPlatform(Platform platform)
		{
			// See if we can find the plugin by GUID
			string pluginPath = AssetDatabase.GUIDToAssetPath(platform.guid);
			
			// If not, try and find it by name
			if (pluginPath.Length == 0)
			{
				Debug.LogWarningFormat("[AVProVideo] Failed to find plugin by GUID, will attempt to find it by name.");
				string[] guids = AssetDatabase.FindAssets(PluginName);
				if (guids != null && guids.Length > 0)
				{
					foreach (string guid in guids)
					{
						string assetPath = AssetDatabase.GUIDToAssetPath(guid);
						if (assetPath.Contains(platform.name))
						{
							pluginPath = assetPath;
							break;
						}
					}
				}
			}

			if (pluginPath.Length > 0)
			{
				Debug.LogFormat("[AVProVideo] Found plugin at '{0}'", pluginPath);
			}

			return pluginPath;
		}

		// Converts the Unity asset path to the expected path in the built Xcode project.
		private static string ConvertPluginAssetPathToXcodeProjectFrameworkPath(string pluginPath)
		{
			List<string> components = new List<string>(pluginPath.Split(new char[] { '/' }));
			components[0] = "Frameworks";
			#if UNITY_2019_1_OR_NEWER
				string frameworkPath = string.Join("/", components);
			#else
				string frameworkPath = string.Join("/", components.ToArray());
			#endif
			return frameworkPath;
		}

		[PostProcessBuild]
		public static void ModifyProject(BuildTarget target, string path)
		{
			Debug.Log("[AVProVideo] Post-processing Xcode project.");
			Platform platform = Platform.GetPlatformForTarget(target);
			if (platform == null)
			{
				Debug.LogWarningFormat("[AVProVideo] Unknown build target: {0}", target.ToString());
				return;
			}

			string projectPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
			PBXProject project = new PBXProject();
			project.ReadFromFile(projectPath);

			// Attempt to find the plugin path
			string pluginPath = PluginPathForPlatform(platform);
			if (pluginPath.Length > 0)
			{
				#if UNITY_2019_3_OR_NEWER
					string targetGuid = project.GetUnityMainTargetGuid();
				#else
					string targetGuid = project.TargetGuidByName(PBXProject.GetUnityTargetName());
				#endif
				
				string frameworkPath = ConvertPluginAssetPathToXcodeProjectFrameworkPath(pluginPath);
				string fileGuid = project.FindFileGuidByProjectPath(frameworkPath);
				if (fileGuid != null)
				{
					PBXProjectExtensions.AddFileToEmbedFrameworks(project, targetGuid, fileGuid);
				}
				else
				{
					Debug.LogWarningFormat("[AVProVideo] Failed to find {0} in the generated project. You will need to manually set {0} to 'Embed & Sign' in the Xcode project's framework list.", PluginName);
				}
				
				project.SetBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");
				project.WriteToFile(projectPath);
			}
			else
			{
				Debug.LogErrorFormat("Failed to find '{0}' for '{1}' in the Unity project. Something is horribly wrong, please reinstall AVPro Video.", PluginName, platform);
			}
		}
	}
}

#endif // (UNITY_IOS || UNITY_TVOS) && UNITY_2017_1_OR_NEWER
