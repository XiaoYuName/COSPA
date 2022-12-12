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
		private readonly static GUIContent[] _audioModesAndroid =
		{
			new GUIContent("System Direct"),
			new GUIContent("Unity"),
			new GUIContent("Facebook Audio 360", "Initialises player with Facebook Audio 360 support"),
		};
		
		private void OnInspectorGUI_Override_Android()
		{
			//MediaPlayer media = (this.target) as MediaPlayer;
			//MediaPlayer.OptionsAndroid options = media._optionsAndroid;

			GUILayout.Space(8f);

			string optionsVarName = MediaPlayer.GetPlatformOptionsVariable(Platform.Android);

			EditorGUILayout.BeginVertical(GUI.skin.box);
			SerializedProperty propVideoApi = serializedObject.FindProperty(optionsVarName + ".videoApi");
			if (propVideoApi != null)
			{
				EditorGUILayout.PropertyField(propVideoApi, new GUIContent("Video API", "The preferred video API to use"));
			}

			SerializedProperty propFileOffset = serializedObject.FindProperty(optionsVarName + ".fileOffset");
			if (propFileOffset != null)
			{
				EditorGUILayout.PropertyField(propFileOffset);
				propFileOffset.intValue = Mathf.Max(0, propFileOffset.intValue);
			}

			SerializedProperty propUseFastOesPath = serializedObject.FindProperty(optionsVarName + ".useFastOesPath");
			if (propUseFastOesPath != null)
			{
				EditorGUILayout.PropertyField(propUseFastOesPath, new GUIContent("Use OES Rendering", "Enables a faster rendering path using OES textures.  This requires that all rendering in Unity uses special GLSL shaders."));
				if (propUseFastOesPath.boolValue)
				{
					EditorHelper.IMGUI.NoticeBox(MessageType.Info, "OES can require special shaders.  Make sure you assign an AVPro Video OES shader to your meshes/materials that need to display video.");

					// PlayerSettings.virtualRealitySupported is deprecated from 2019.3
#if !UNITY_2019_3_OR_NEWER
					if (PlayerSettings.virtualRealitySupported)
#endif
					{
						if (PlayerSettings.stereoRenderingPath != StereoRenderingPath.MultiPass)
						{
							EditorHelper.IMGUI.NoticeBox(MessageType.Error, "OES only supports multi-pass stereo rendering path, please change in Player Settings.");
						}
					}

					EditorHelper.IMGUI.NoticeBox(MessageType.Warning, "OES is not supported in the trial version.  If your Android plugin is not trial then you can ignore this warning.");
				}
			}
			EditorGUILayout.EndVertical();

			SerializedProperty httpHeadersProp = serializedObject.FindProperty(optionsVarName + ".httpHeaders.httpHeaders");
			if (httpHeadersProp != null)
			{
				OnInspectorGUI_HttpHeaders(httpHeadersProp);
			}

			// MediaPlayer API options
			{
				EditorGUILayout.BeginVertical(GUI.skin.box);
				GUILayout.Label("MediaPlayer API Options", EditorStyles.boldLabel);

				SerializedProperty propShowPosterFrame = serializedObject.FindProperty(optionsVarName + ".showPosterFrame");
				if (propShowPosterFrame != null)
				{
					EditorGUILayout.PropertyField(propShowPosterFrame, new GUIContent("Show Poster Frame", "Allows a paused loaded video to display the initial frame. This uses up decoder resources."));
				}

				EditorGUILayout.EndVertical();
			}

			// ExoPlayer API options
			{
				EditorGUILayout.BeginVertical(GUI.skin.box);
				GUILayout.Label("ExoPlayer API Options", EditorStyles.boldLabel);

				SerializedProperty propPreferSoftwareDecoder = serializedObject.FindProperty(optionsVarName + ".preferSoftwareDecoder");
				if(propPreferSoftwareDecoder != null)
				{
					EditorGUILayout.PropertyField(propPreferSoftwareDecoder);
				}

				// Audio
				{
					SerializedProperty propAudioOutput = serializedObject.FindProperty(optionsVarName + ".audioOutput");
					propAudioOutput.enumValueIndex = EditorGUILayout.Popup(new GUIContent("Audio Output"), propAudioOutput.enumValueIndex, _audioModesAndroid);
					if ((Android.AudioOutput)propAudioOutput.enumValueIndex == Android.AudioOutput.FacebookAudio360)
					{
						EditorGUILayout.Space();
						EditorGUILayout.LabelField("Facebook Audio 360", EditorStyles.boldLabel);

						SerializedProperty prop360AudioChannelMode = serializedObject.FindProperty(optionsVarName + ".audio360ChannelMode");
						if (prop360AudioChannelMode != null)
						{
							GUIContent label = new GUIContent("Channel Mode", "Specifies what channel mode Facebook Audio 360 needs to be initialised with");
							prop360AudioChannelMode.enumValueIndex = EditorGUILayout.Popup(label, prop360AudioChannelMode.enumValueIndex, _audio360ChannelMapGuiNames);
						}
					}
				}

				GUILayout.Space(8f);

//				EditorGUILayout.BeginVertical();
				EditorGUILayout.LabelField("Adaptive Stream", EditorStyles.boldLabel);

				SerializedProperty propStartWithHighestBitrate = serializedObject.FindProperty(optionsVarName + ".startWithHighestBitrate");
				if (propStartWithHighestBitrate != null)
				{
					EditorGUILayout.PropertyField(propStartWithHighestBitrate, new GUIContent("Start Max Bitrate"));
				}

				SerializedProperty preferredMaximumResolutionProp = serializedObject.FindProperty(optionsVarName + ".preferredMaximumResolution");
				if (preferredMaximumResolutionProp != null)
				{
					EditorGUILayout.PropertyField(preferredMaximumResolutionProp, new GUIContent("Preferred Maximum Resolution", "The desired maximum resolution of the video."));
					if ((MediaPlayer.OptionsAndroid.Resolution)preferredMaximumResolutionProp.intValue == MediaPlayer.OptionsAndroid.Resolution.Custom)
					{
						SerializedProperty customPreferredMaximumResolutionProp = serializedObject.FindProperty(optionsVarName + ".customPreferredMaximumResolution");
						if (customPreferredMaximumResolutionProp != null)
						{
							EditorGUILayout.PropertyField(customPreferredMaximumResolutionProp, new GUIContent(" "));
						}
					}
				}

				SerializedProperty propMinBufferMs = serializedObject.FindProperty(optionsVarName + ".minBufferMs");
				if (propMinBufferMs != null)
				{
					EditorGUILayout.PropertyField(propMinBufferMs, new GUIContent("Minimum Buffer Ms"));
				}

				SerializedProperty propMaxBufferMs = serializedObject.FindProperty(optionsVarName + ".maxBufferMs");
				if (propMaxBufferMs != null)
				{
					EditorGUILayout.PropertyField(propMaxBufferMs, new GUIContent("Maximum Buffer Ms"));
				}
				SerializedProperty propBufferForPlaybackMs = serializedObject.FindProperty(optionsVarName + ".bufferForPlaybackMs");
				if (propBufferForPlaybackMs != null)
				{
					EditorGUILayout.PropertyField(propBufferForPlaybackMs, new GUIContent("Buffer For Playback Ms "));
				}
				SerializedProperty propBufferForPlaybackAfterRebufferMs = serializedObject.FindProperty(optionsVarName + ".bufferForPlaybackAfterRebufferMs");
				if (propBufferForPlaybackAfterRebufferMs != null)
				{
					EditorGUILayout.PropertyField(propBufferForPlaybackAfterRebufferMs, new GUIContent("Buffer For Playback After Rebuffer Ms"));
				}

				EditorGUILayout.EndVertical();
			}
			GUI.enabled = true;

			/*
			SerializedProperty propFileOffsetLow = serializedObject.FindProperty(optionsVarName + ".fileOffsetLow");
			SerializedProperty propFileOffsetHigh = serializedObject.FindProperty(optionsVarName + ".fileOffsetHigh");
			if (propFileOffsetLow != null && propFileOffsetHigh != null)
			{
				propFileOffsetLow.intValue = ;

				EditorGUILayout.PropertyField(propFileOFfset);
			}*/
		}
	}
}