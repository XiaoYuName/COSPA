using UnityEngine;
using UnityEditor;

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
		private SerializedProperty _propSourceAudioSampleRate;
		private SerializedProperty _propSourceAudioChannels;
		private SerializedProperty _propManualSetAudioProps;

		private readonly static GUIContent[] _audioModesWindows =
		{
			new GUIContent("System Direct"),
			new GUIContent("Unity", "Allows the AudioOutput component to grab audio from the video and play it through Unity to the AudioListener"),
			new GUIContent("Facebook Audio 360", "Initialises player with Facebook Audio 360 support"),
			new GUIContent("None", "No audio"),
		};

		private readonly static GUIContent[] _audioModesUWP =
		{
			new GUIContent("System Direct"),
			new GUIContent("Unity", "Allows the AudioOutput component to grab audio from the video and play it through Unity to the AudioListener"),
			new GUIContent("Facebook Audio 360", "Initialises player with Facebook Audio 360 support"),
			new GUIContent("None", "No audio"),
		};

		private void OnInspectorGUI_Override_Windows()
		{
			//MediaPlayer media = (this.target) as MediaPlayer;
			//MediaPlayer.OptionsWindows options = media._optionsWindows;

			GUILayout.Space(8f);

			EditorGUILayout.BeginVertical(GUI.skin.box);
			string optionsVarName = MediaPlayer.GetPlatformOptionsVariable(Platform.Windows);
			SerializedProperty propVideoApi = serializedObject.FindProperty(optionsVarName + ".videoApi");
			if (propVideoApi != null)
			{
				EditorGUILayout.PropertyField(propVideoApi, new GUIContent("Video API", "The preferred video API to use"));
			}
			{
				SerializedProperty propUseTextureMips = serializedObject.FindProperty(optionsVarName + ".useTextureMips");
				if (propUseTextureMips != null)
				{
					EditorGUILayout.PropertyField(propUseTextureMips, new GUIContent("Generate Mipmaps", "Automatically create mip-maps for the texture to reducing aliasing when texture is scaled down"));
					if (propUseTextureMips.boolValue && ((FilterMode)_propFilter.enumValueIndex) != FilterMode.Trilinear)
					{
						EditorHelper.IMGUI.NoticeBox(MessageType.Info, "Recommend changing the texture filtering mode to Trilinear when using mip-maps.");
					}
				}
			}
			{
				SerializedProperty propUseHardwareDecoding = serializedObject.FindProperty(optionsVarName + ".useHardwareDecoding");
				EditorGUI.BeginDisabledGroup(!propUseHardwareDecoding.boolValue && propVideoApi.enumValueIndex == (int)Windows.VideoApi.MediaFoundation);
				{
					SerializedProperty propUse10BitTextures = serializedObject.FindProperty(optionsVarName + ".use10BitTextures");
					if (propUse10BitTextures != null)
					{
						EditorGUILayout.PropertyField(propUse10BitTextures, new GUIContent("Use 10-Bit Textures", "Provides a hint to the decoder to use 10-bit textures - allowing more quality for videos encoded with a 10-bit profile"));
					}
				}
				EditorGUI.EndDisabledGroup();
			}
			EditorGUILayout.EndVertical();

			// Media Foundation Options
			{
				EditorGUILayout.BeginVertical(GUI.skin.box);
				GUILayout.Label("Media Foundation API Options", EditorStyles.boldLabel);
				{
					SerializedProperty propUseHardwareDecoding = serializedObject.FindProperty(optionsVarName + ".useHardwareDecoding");
					if (propUseHardwareDecoding != null)
					{
						EditorGUILayout.PropertyField(propUseHardwareDecoding, new GUIContent("Hardware Decoding"));
					}
				}
				{
					SerializedProperty propUseLowLatency = serializedObject.FindProperty(optionsVarName + ".useLowLatency");
					if (propUseLowLatency != null)
					{
						EditorGUILayout.PropertyField(propUseLowLatency, new GUIContent("Use Low Latency", "Provides a hint to the decoder to use less buffering - may degrade performance and quality"));
					}
					{
						GUILayout.Label("Hap/NotchLC");
						EditorGUI.indentLevel++;
						SerializedProperty propUseCustomMovParser = serializedObject.FindProperty(optionsVarName + ".useCustomMovParser");
						if (propUseLowLatency != null)
						{
							EditorGUILayout.PropertyField(propUseCustomMovParser, new GUIContent("Use Custom MOV Parser", "For playback of Hap and NotchLC media to handle high bit-rates"));
						}
						SerializedProperty propParallelFrameCount = serializedObject.FindProperty(optionsVarName + ".parallelFrameCount");
						if (propParallelFrameCount != null)
						{
							EditorGUILayout.PropertyField(propParallelFrameCount, new GUIContent("Parallel Frame Count", "Number of frames to decode in parallel via multi-threading.  Higher values increase latency but can improve performance for demanding videos."));
						}
						SerializedProperty propPrerollFrameCount = serializedObject.FindProperty(optionsVarName + ".prerollFrameCount");
						if (propPrerollFrameCount != null)
						{
							EditorGUILayout.PropertyField(propPrerollFrameCount, new GUIContent("Preroll Frame Count", "Number of frames to pre-decode before playback starts.  Higher values increase latency but can improve performance for demanding videos."));
						}
						EditorGUI.indentLevel--;
					}
				}
				// Audio Output
				{
					SerializedProperty propAudioOutput = serializedObject.FindProperty(optionsVarName + ".audioOutput");
					propAudioOutput.enumValueIndex = EditorGUILayout.Popup(new GUIContent("Audio Output"), propAudioOutput.enumValueIndex, _audioModesWindows);
					if ((Windows.AudioOutput)propAudioOutput.enumValueIndex == Windows.AudioOutput.FacebookAudio360)
					{
						EditorGUILayout.Space();
						EditorGUILayout.LabelField("Facebook Audio 360", EditorStyles.boldLabel);

						SerializedProperty prop360AudioChannelMode = serializedObject.FindProperty(optionsVarName + ".audio360ChannelMode");
						if (prop360AudioChannelMode != null)
						{
							GUIContent label = new GUIContent("Channel Mode", "Specifies what channel mode Facebook Audio 360 needs to be initialised with");
							prop360AudioChannelMode.enumValueIndex = EditorGUILayout.Popup(label, prop360AudioChannelMode.enumValueIndex, _audio360ChannelMapGuiNames);
						}

						SerializedProperty propForceAudioOutputDeviceName = serializedObject.FindProperty(optionsVarName + ".forceAudioOutputDeviceName");
						if (propForceAudioOutputDeviceName != null)
						{
							string[] deviceNames = { "Default", Windows.AudioDeviceOutputName_Rift, Windows.AudioDeviceOutputName_Vive, "Custom" };
							int index = 0;
							if (!string.IsNullOrEmpty(propForceAudioOutputDeviceName.stringValue))
							{
								switch (propForceAudioOutputDeviceName.stringValue)
								{
									case Windows.AudioDeviceOutputName_Rift:
										index = 1;
										break;
									case Windows.AudioDeviceOutputName_Vive:
										index = 2;
										break;
									default:
										index = 3;
										break;
								}
							}
							int newIndex = EditorGUILayout.Popup("Audio Device Name", index, deviceNames);
							if (newIndex == 0)
							{
								propForceAudioOutputDeviceName.stringValue = string.Empty;
							}
							else if (newIndex == 3)
							{
								if (index != newIndex)
								{
									if (string.IsNullOrEmpty(propForceAudioOutputDeviceName.stringValue) ||
											propForceAudioOutputDeviceName.stringValue == Windows.AudioDeviceOutputName_Rift ||
											propForceAudioOutputDeviceName.stringValue == Windows.AudioDeviceOutputName_Vive)
									{
										propForceAudioOutputDeviceName.stringValue = "?";
									}
								}
								EditorGUILayout.PropertyField(propForceAudioOutputDeviceName, new GUIContent("Audio Device Name", "Useful for VR when you need to output to the VR audio device"));
							}
							else
							{
								propForceAudioOutputDeviceName.stringValue = deviceNames[newIndex];
							}
						}
					}
				}
				EditorGUILayout.EndVertical();
			}

			// WinRT Options
			{
				EditorGUILayout.BeginVertical(GUI.skin.box);
				GUILayout.Label("WinRT API Options", EditorStyles.boldLabel);
				{
					SerializedProperty propStartWithHighestBitrate = serializedObject.FindProperty(optionsVarName + ".startWithHighestBitrate");
					if (propStartWithHighestBitrate != null)
					{
						EditorGUILayout.PropertyField(propStartWithHighestBitrate, new GUIContent("Start Max Bitrate"));
					}
				}
				SerializedProperty httpHeadersProp = serializedObject.FindProperty(optionsVarName + ".httpHeaders.httpHeaders");
				if (httpHeadersProp != null)
				{
					OnInspectorGUI_HttpHeaders(httpHeadersProp);
				}
				GUILayout.Space(8f);
				SerializedProperty keyAuthProp = serializedObject.FindProperty(optionsVarName + ".keyAuth");
				if (keyAuthProp != null)
				{
					OnInspectorGUI_HlsDecryption(keyAuthProp);
				}
				EditorGUILayout.EndVertical();
			}

			// DirectShow Options
			{
				EditorGUILayout.BeginVertical(GUI.skin.box);
				GUILayout.Label("DirectShow API Options", EditorStyles.boldLabel);
				{
					SerializedProperty propHintAlphaChannel = serializedObject.FindProperty(optionsVarName + ".hintAlphaChannel");
					if (propHintAlphaChannel != null)
					{
						EditorGUILayout.PropertyField(propHintAlphaChannel, new GUIContent("Alpha Channel Hint", "If a video is detected as 32-bit, use or ignore the alpha channel"));
					}
				}
				{
					SerializedProperty propForceAudioOutputDeviceName = serializedObject.FindProperty(optionsVarName + ".forceAudioOutputDeviceName");
					if (propForceAudioOutputDeviceName != null)
					{
						EditorGUILayout.PropertyField(propForceAudioOutputDeviceName, new GUIContent("Force Audio Output Device Name", "Useful for VR when you need to output to the VR audio device"));
					}
				}
				{
					int prevIndentLevel = EditorGUI.indentLevel;
					EditorGUI.indentLevel = 1;
					SerializedProperty propPreferredFilter = serializedObject.FindProperty(optionsVarName + ".preferredFilters");
					if (propPreferredFilter != null)
					{
						EditorGUILayout.PropertyField(propPreferredFilter, new GUIContent("Preferred Filters", "Priority list for preferred filters to be used instead of default"), true);
						if (propPreferredFilter.arraySize > 0)
						{
							EditorHelper.IMGUI.NoticeBox(MessageType.Info, "Command filter names are:\n1) \"Microsoft DTV-DVD Video Decoder\" (best for compatibility when playing H.264 videos)\n2) \"LAV Video Decoder\"\n3) \"LAV Audio Decoder\"");
						}
					}
					EditorGUI.indentLevel = prevIndentLevel;
				}
				EditorGUILayout.EndVertical();
			}
		}


		private void OnInspectorGUI_Override_WindowsUWP()
		{
			//MediaPlayer media = (this.target) as MediaPlayer;
			//MediaPlayer.OptionsWindowsUWP options = media._optionsWindowsUWP;

			GUILayout.Space(8f);

			string optionsVarName = MediaPlayer.GetPlatformOptionsVariable(Platform.WindowsUWP);

			EditorGUILayout.BeginVertical(GUI.skin.box);
			SerializedProperty propVideoApi = serializedObject.FindProperty(optionsVarName + ".videoApi");
			if (propVideoApi != null)
			{
				EditorGUILayout.PropertyField(propVideoApi, new GUIContent("Video API", "The preferred video API to use"));
			}
			{
				SerializedProperty propUseHardwareDecoding = serializedObject.FindProperty(optionsVarName + ".useHardwareDecoding");
				EditorGUI.BeginDisabledGroup(!propUseHardwareDecoding.boolValue && propVideoApi.enumValueIndex == (int)WindowsUWP.VideoApi.MediaFoundation);
				{
					SerializedProperty propUse10BitTextures = serializedObject.FindProperty(optionsVarName + ".use10BitTextures");
					if (propUse10BitTextures != null)
					{
						EditorGUILayout.PropertyField(propUse10BitTextures, new GUIContent("Use 10-Bit Textures", "Provides a hint to the decoder to use 10-bit textures - allowing more quality for videos encoded with a 10-bit profile"));
					}
				}
				EditorGUI.EndDisabledGroup();
			}
			EditorGUILayout.EndVertical();

			// Media Foundation Options
			{
				EditorGUILayout.BeginVertical(GUI.skin.box);
				GUILayout.Label("Media Foundation API Options", EditorStyles.boldLabel);
				{
					SerializedProperty propUseHardwareDecoding = serializedObject.FindProperty(optionsVarName + ".useHardwareDecoding");
					if (propUseHardwareDecoding != null)
					{
						EditorGUILayout.PropertyField(propUseHardwareDecoding, new GUIContent("Hardware Decoding"));
					}
				}
				{
					SerializedProperty propUseTextureMips = serializedObject.FindProperty(optionsVarName + ".useTextureMips");
					if (propUseTextureMips != null)
					{
						EditorGUILayout.PropertyField(propUseTextureMips, new GUIContent("Generate Mipmaps", "Automatically create mip-maps for the texture to reducing aliasing when texture is scaled down"));
						if (propUseTextureMips.boolValue && ((FilterMode)_propFilter.enumValueIndex) != FilterMode.Trilinear)
						{
							EditorHelper.IMGUI.NoticeBox(MessageType.Info, "Recommend changing the texture filtering mode to Trilinear when using mip-maps.");
						}
					}
				}
				{
					SerializedProperty propUseLowLatency = serializedObject.FindProperty(optionsVarName + ".useLowLatency");
					if (propUseLowLatency != null)
					{
						EditorGUILayout.PropertyField(propUseLowLatency, new GUIContent("Use Low Latency", "Provides a hint to the decoder to use less buffering - may degrade performance and quality"));
					}
				}
				
				// Audio Output
				{
					SerializedProperty propAudioOutput = serializedObject.FindProperty(optionsVarName + ".audioOutput");
					propAudioOutput.enumValueIndex = EditorGUILayout.Popup(new GUIContent("Audio Output"), propAudioOutput.enumValueIndex, _audioModesUWP);
				}

				EditorGUILayout.EndVertical();
			}

			// WinRT Options
			{
				EditorGUILayout.BeginVertical(GUI.skin.box);
				GUILayout.Label("WinRT API Options", EditorStyles.boldLabel);
				{
					SerializedProperty propStartWithHighestBitrate = serializedObject.FindProperty(optionsVarName + ".startWithHighestBitrate");
					if (propStartWithHighestBitrate != null)
					{
						EditorGUILayout.PropertyField(propStartWithHighestBitrate, new GUIContent("Start Max Bitrate"));
					}
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
				EditorGUILayout.EndVertical();
			}

			GUI.enabled = true;
		}
	}
}