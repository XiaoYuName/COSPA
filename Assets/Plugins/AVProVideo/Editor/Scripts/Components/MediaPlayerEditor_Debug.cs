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
		private static bool _allowDeveloperMode = false;

		private void OnInspectorGUI_Debug()
		{
			MediaPlayer mediaPlayer = (this.target) as MediaPlayer;
			IMediaInfo info = mediaPlayer.Info;
			if (info != null)
			{
				EditorGUI.BeginDisabledGroup(true);
				GUILayout.BeginHorizontal();
				GUILayout.Toggle(mediaPlayer.Control.HasMetaData(), "MetaData", GUI.skin.button);
				GUILayout.Toggle(mediaPlayer.Control.IsPaused(), "Paused", GUI.skin.button);
				GUILayout.Toggle(mediaPlayer.Control.IsPlaying(), "Playing", GUI.skin.button);
				GUILayout.Toggle(mediaPlayer.Control.IsSeeking(), "Seeking", GUI.skin.button);
				GUILayout.Toggle(mediaPlayer.Control.IsBuffering(), "Buffering", GUI.skin.button);
				GUILayout.Toggle(mediaPlayer.Info.IsPlaybackStalled(), "Stalled", GUI.skin.button);
				GUILayout.Toggle(mediaPlayer.Control.IsFinished(), "Finished", GUI.skin.button);
				GUILayout.EndHorizontal();
				EditorGUI.EndDisabledGroup();

				GUILayout.BeginHorizontal();
				GUILayout.Label("Time: " + mediaPlayer.Control.GetCurrentTime());
				GUILayout.FlexibleSpace();
				GUILayout.Label("Frame: " + mediaPlayer.Control.GetCurrentTimeFrames());
				EditorGUI.BeginDisabledGroup(mediaPlayer.Info.GetVideoFrameRate() <= 0f);
				if (GUILayout.Button("<"))
				{
					mediaPlayer.Control.SeekToFrameRelative(-1);
				}
				if (GUILayout.Button(">"))
				{
					mediaPlayer.Control.SeekToFrameRelative(1);
				}
				EditorGUI.EndDisabledGroup();
				GUILayout.EndHorizontal();

				if (mediaPlayer.TextureProducer != null)
				{
					GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
					GUILayout.FlexibleSpace();
					for (int i = 0; i < mediaPlayer.TextureProducer.GetTextureCount(); i++)
					{
						Texture texture = mediaPlayer.TextureProducer.GetTexture(i);
						if (texture != null)
						{
							GUILayout.BeginVertical();
							Rect textureRect = GUILayoutUtility.GetRect(128f, 128f);
							if (Event.current.type == EventType.Repaint)
							{				
								GUI.color = Color.gray;
								EditorGUI.DrawTextureTransparent(textureRect, Texture2D.blackTexture, ScaleMode.StretchToFill);
								GUI.color = Color.white;
							}
							GUI.DrawTexture(textureRect, texture, ScaleMode.ScaleToFit, false);
							GUILayout.Label(texture.width + "x" + texture.height + " ");
							if (GUILayout.Button("Select Texture", GUILayout.ExpandWidth(false)))
							{
								Selection.activeObject = texture;
							}
							GUILayout.EndVertical();
						}
					}
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();

					GUILayout.Label("Updates: " + mediaPlayer.TextureProducer.GetTextureFrameCount());
					GUILayout.Label("TimeStamp: " + mediaPlayer.TextureProducer.GetTextureTimeStamp());

					GUILayout.BeginHorizontal();
					if (GUILayout.Button("Save Frame PNG", GUILayout.ExpandWidth(true)))
					{
						mediaPlayer.SaveFrameToPng();
					}
					if (GUILayout.Button("Save Frame EXR", GUILayout.ExpandWidth(true)))
					{
						mediaPlayer.SaveFrameToExr();
					}
					GUILayout.EndHorizontal();
				}

				int activeDecodeThreadCount = 0;
				int decodedFrameCount = 0;
				int droppedFrameCount = 0;
				if (info.GetDecoderPerformance(ref activeDecodeThreadCount, ref decodedFrameCount, ref droppedFrameCount))
				{
					GUILayout.Label("Decode Stats");
					EditorGUI.indentLevel++;
					EditorGUILayout.Slider("Parallel Frames", activeDecodeThreadCount, 0f, mediaPlayer.PlatformOptionsWindows.parallelFrameCount);
					EditorGUILayout.Slider("Decoded Frames", decodedFrameCount, 0f, mediaPlayer.PlatformOptionsWindows.prerollFrameCount*2);
					EditorGUILayout.IntField("Dropped Frames", droppedFrameCount);
					EditorGUI.indentLevel--;
				}
			}
			else
			{
				GUILayout.Label("No media loaded");
			}
		}
	}
}