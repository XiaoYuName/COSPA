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
		private void OnInspectorGUI_Override_WebGL()
		{
			GUILayout.Space(8f);

			string optionsVarName = MediaPlayer.GetPlatformOptionsVariable(Platform.WebGL);

			EditorGUILayout.BeginVertical(GUI.skin.box);
			SerializedProperty propExternalLibrary = serializedObject.FindProperty(optionsVarName + ".externalLibrary");
			if (propExternalLibrary != null)
			{
				EditorGUILayout.PropertyField(propExternalLibrary);
			}

			{
				SerializedProperty propUseTextureMips = serializedObject.FindProperty(optionsVarName + ".useTextureMips");
				if (propUseTextureMips != null)
				{
					EditorGUILayout.PropertyField(propUseTextureMips, new GUIContent("Generate Mipmap", "Automatically create mip-maps for the texture to reducing aliasing when texture is scaled down"));
					if (propUseTextureMips.boolValue && ((FilterMode)_propFilter.enumValueIndex) != FilterMode.Trilinear)
					{
						EditorHelper.IMGUI.NoticeBox(MessageType.Info, "Recommend changing the texture filtering mode to Trilinear when using mip-maps.");
					}
				}
			}
			EditorGUILayout.EndVertical();
		}
	}
}