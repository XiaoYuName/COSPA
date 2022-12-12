using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

//-----------------------------------------------------------------------------
// Copyright 2015-2021 RenderHeads Ltd.  All rights reserved.
//-----------------------------------------------------------------------------

namespace RenderHeads.Media.AVProVideo.Editor
{
	/// <summary>
	/// Editor for the MediaPlayer component
	/// </summary>
	public partial class MediaPlayerEditor : UnityEditor.Editor
	{
		private void OnInspectorGUI_Override_Apple(Platform platform)
		{
			GUILayout.Space(8f);

			string optionsVarName = MediaPlayer.GetPlatformOptionsVariable(platform);

			EditorGUILayout.BeginVertical(GUI.skin.box);
			SerializedProperty textureFormatProp = serializedObject.FindProperty(optionsVarName + ".textureFormat");
			if (textureFormatProp != null)
			{
				EditorGUILayout.PropertyField(textureFormatProp, new GUIContent("Texture Format", "BGRA32 is the most compatible.\nYCbCr420 uses ~50% of the memory of BGRA32 and has slightly better performance however it does require shader support, recommended for iOS and tvOS."));
			}

			SerializedProperty flagsProp = serializedObject.FindProperty(optionsVarName + ".flags");
			MediaPlayer.OptionsApple.Flags flags = flagsProp != null ? (MediaPlayer.OptionsApple.Flags)flagsProp.intValue : 0;

			// Texture flags
			if (flagsProp != null)
			{
				bool generateMipmaps = flags.GenerateMipmaps();
				generateMipmaps = EditorGUILayout.Toggle(new GUIContent("Generate Mipmaps"), generateMipmaps);
				flags = flags.SetGenerateMipMaps(generateMipmaps);
			}

			// Audio
			SerializedProperty audioMode = serializedObject.FindProperty(optionsVarName + ".audioMode");
			if (audioMode != null)
			{
				EditorGUILayout.PropertyField(audioMode, new GUIContent("Audio Mode", "Unity mode does not work with HLS video"));
			}

			// Platform specific flags
			if (flagsProp != null)
			{
				if (platform == Platform.MacOSX || platform == Platform.iOS)
				{
					bool b = flags.AllowExternalPlayback();
					b = EditorGUILayout.Toggle(new GUIContent("Allow External Playback", "Enables support for playback on external devices via AirPlay."), b);
					flags = flags.SetAllowExternalPlayback(b);
				}

				if (platform == Platform.iOS)
				{
					bool b = flags.ResumePlaybackAfterAudioSessionRouteChange();
					b = EditorGUILayout.Toggle(new GUIContent("Resume playback after audio route change", "The default behaviour is for playback to pause when the audio route changes, for instance when disconnecting headphones."), b);
					flags = flags.SetResumePlaybackAfterAudioSessionRouteChange(b);
				}
			}

			SerializedProperty maximumPlaybackRateProp = serializedObject.FindProperty(optionsVarName + ".maximumPlaybackRate");
			if (maximumPlaybackRateProp != null)
			{
				EditorGUILayout.Slider(maximumPlaybackRateProp, 2.0f, 10.0f, new GUIContent("Max Playback Rate", "Increase the maximum playback rate before which playback switches to key-frames only."));
			}

			GUILayout.Space(8f);

			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField("HLS Options", EditorStyles.boldLabel);

			SerializedProperty preferredMaximumResolutionProp = serializedObject.FindProperty(optionsVarName + ".preferredMaximumResolution");
			if (preferredMaximumResolutionProp != null)
			{
				EditorGUILayout.PropertyField(preferredMaximumResolutionProp, new GUIContent("Preferred Maximum Resolution", "The desired maximum resolution of the video."));
				if ((MediaPlayer.OptionsApple.Resolution)preferredMaximumResolutionProp.intValue == MediaPlayer.OptionsApple.Resolution.Custom)
				{
					SerializedProperty customPreferredMaximumResolutionProp = serializedObject.FindProperty(optionsVarName + ".customPreferredMaximumResolution");
					if (customPreferredMaximumResolutionProp != null)
					{
						EditorGUILayout.PropertyField(customPreferredMaximumResolutionProp, new GUIContent(" "));
					}
				}
			}

			SerializedProperty preferredPeakBitRateProp = serializedObject.FindProperty(optionsVarName + ".preferredPeakBitRate");
			SerializedProperty preferredPeakBitRateUnitsProp = serializedObject.FindProperty(optionsVarName + ".preferredPeakBitRateUnits");
			if (preferredPeakBitRateProp != null && preferredPeakBitRateUnitsProp != null)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PropertyField(preferredPeakBitRateProp, new GUIContent("Preferred Peak BitRate", "The desired limit of network bandwidth consumption for playback, set to 0 for no preference."));
				EditorGUILayout.PropertyField(preferredPeakBitRateUnitsProp, new GUIContent());
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.EndVertical();

			EditorGUILayout.EndVertical();

			// Set the flags

			if (flagsProp != null)
			{
				flagsProp.intValue = (int)flags;
			}

			SerializedProperty httpHeadersProp = serializedObject.FindProperty(optionsVarName + ".httpHeaders.httpHeaders");
			if (httpHeadersProp != null)
			{
				OnInspectorGUI_HttpHeaders(httpHeadersProp);
			}

			SerializedProperty keyAuthProp = serializedObject.FindProperty(optionsVarName + ".keyAuth");
			if (keyAuthProp != null)
			{
				OnInspectorGUI_HlsDecryption(keyAuthProp);
			}
		}

		private void OnInspectorGUI_Override_MacOSX()
		{
			OnInspectorGUI_Override_Apple(Platform.MacOSX);
		}

		private void OnInspectorGUI_Override_iOS()
		{
			OnInspectorGUI_Override_Apple(Platform.iOS);
		}

		private void OnInspectorGUI_Override_tvOS()
		{
			OnInspectorGUI_Override_Apple(Platform.tvOS);
		}
	}
}
