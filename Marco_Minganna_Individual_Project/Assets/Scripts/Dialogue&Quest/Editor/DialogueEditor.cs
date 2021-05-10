using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Callbacks;
using UnityEditor;
using System;

namespace Dialogue.Editor
{
    /// <summary>
    /// the dialogue editor class, used to create the window and visualize the nodes
    /// </summary>
    public class DialogueEditor : EditorWindow
    {
        /// <summary>
        /// keep track of the selected dialogue in the unity editor
        /// </summary>
        Dialogue selectedDialogue=null;
        /// <summary>
        /// the current position in the window 
        /// </summary>
        Vector2 scrollPosition;
        [NonSerialized]
        ///the color and style of the node when NPc is talking
        GUIStyle nodeStyle;
        [NonSerialized]
        ///the color and style of the node when player is talking
        GUIStyle PlayerStyle;
        [NonSerialized]
        ///keep track of the node the player is draggin
        DialogueNode draggingNode = null;
        [NonSerialized]
        Vector2 draggingOffset;
        [NonSerialized]
        DialogueNode creatingNode=null;
        [NonSerialized]
        DialogueNode DelitingNode = null;
        [NonSerialized]
        DialogueNode LinkingParentNode = null;
        [NonSerialized]
        bool draggingCanvas = false;
        [NonSerialized]
        Vector2 draggingCanvansOffset;

        const float canvasSize = 4000;
        const float backgroundSize = 70;




        [MenuItem("Window/Dialogue Editor")]
        ///create the window editor
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        [OnOpenAsset(1)]
        ///open the window if a dialogue is double clicked
        public static bool OnOpenAsset(int instanceID,int line)
        {
            Dialogue dialogue= EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if(dialogue!=null)
            {
                ShowEditorWindow();
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// function used to change the node style and update them when the editor is enabled
        /// </summary>
        private void OnEnable()
        {
            Selection.selectionChanged += OneSelectionChanged;

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background=EditorGUIUtility.Load("node0")as Texture2D;
            nodeStyle.padding = new RectOffset(20,20,20,20);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);

            PlayerStyle = new GUIStyle();
            PlayerStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            PlayerStyle.padding = new RectOffset(20, 20, 20, 20);
            PlayerStyle.border = new RectOffset(12, 12, 12, 12);
        }

        /// <summary>
        /// used to updat the view for any change
        /// </summary>
        private void OneSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
           if (newDialogue!=null)
            {
                selectedDialogue = newDialogue;
                Repaint();
            }
        }
        /// <summary>
        /// function used to display in the window the informations needed
        /// </summary>
        private void OnGUI()
        {
            if(selectedDialogue==null)
            {
                EditorGUILayout.LabelField("no Dialogue Selected.");
            }
            else
            {
                processEvents();

                scrollPosition= EditorGUILayout.BeginScrollView(scrollPosition);

                Rect canvas=GUILayoutUtility.GetRect(canvasSize,canvasSize);
                Texture2D backtex=Resources.Load("background")as Texture2D;
                Rect texCoord = new Rect(0, 0, canvasSize/backgroundSize, canvasSize / backgroundSize);
                GUI.DrawTextureWithTexCoords(canvas, backtex,texCoord);

                foreach (DialogueNode item in selectedDialogue.GetAllNodes())
                {
                    DrawConnections(item);
                }
                foreach (DialogueNode item in selectedDialogue.GetAllNodes())
                {
                    
                    DrawNode(item);

                }
                EditorGUILayout.EndScrollView();

                if(creatingNode!=null)
                {

                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }
                if (DelitingNode != null)
                {
                   
                    selectedDialogue.DeleteNode(DelitingNode);
                    DelitingNode = null;
                }

            }
            
        }

        /// <summary>
        /// function used to drag nodes around or to change the position in the canvas
        /// </summary>
        private void processEvents()
        {
            if(Event.current.type==EventType.MouseDown&&draggingNode==null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition+scrollPosition);
                if(draggingNode!=null)
                {
                    draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;
                    Selection.activeObject = draggingNode;
                }
                else
                {
                    draggingCanvas = true;
                    draggingCanvansOffset = Event.current.mousePosition + scrollPosition;
                    Selection.activeObject = selectedDialogue;
                }
            }
            else if(Event.current.type==EventType.MouseDrag&& draggingNode != null)
            {
               
                draggingNode.SetPosition(Event.current.mousePosition + draggingOffset);
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && draggingCanvas)
            {
                scrollPosition = draggingCanvansOffset - Event.current.mousePosition;

                GUI.changed = true;
            }
            else if(Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
            else if (Event.current.type == EventType.MouseUp && draggingCanvas)
            {
               draggingCanvas=false;
            }
        }

        /// <summary>
        /// function used to update the node position to the mouse position
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <returns></returns>
        private DialogueNode GetNodeAtPoint(Vector2 mousePosition)
        {
            DialogueNode foundNode = null;
            foreach(DialogueNode node in selectedDialogue.GetAllNodes())
            {
                if(node.GetRect().Contains(mousePosition))
                {
                    foundNode=node;
                }
            }
            return foundNode;
        }



        /// <summary>
        /// function used to add or draw the nodes
        /// </summary>
        /// <param name="item"></param>
        private void DrawNode(DialogueNode item)
        {
            GUIStyle style = nodeStyle;
            if(item.isPlayerSpeaking())
            {
                style = PlayerStyle;
            }
            GUILayout.BeginArea(item.GetRect(), style);

            item.SetText(EditorGUILayout.TextField(item.GetText()));
            item.SetVoiceActing(EditorGUILayout.TextField(item.GetVoiceTrack()));

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Add"))
            {
                creatingNode = item;
            }

            LinkingNodes(item);

            if (item.name != "MainNode")
            {
                
                if (GUILayout.Button("Remove"))
                {
                    DelitingNode = item;
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Change Speaker"))
            {
                item.SetSpeaker(!item.isPlayerSpeaking());
            }
            if(!item.isPlayerSpeaking())
            {
                if (GUILayout.Button("Is Question?"))
                {
                    item.SetIsQuestion(!item.IsQuestion());
                }

            }
           
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        /// <summary>
        /// function used to link and unlink the nodes
        /// </summary>
        /// <param name="item"></param>
        private void LinkingNodes(DialogueNode item)
        {
            if (LinkingParentNode == null)
            {
                if (GUILayout.Button("Link"))
                {
                    LinkingParentNode = item;
                }
            }
            else if(LinkingParentNode==item)
            {
                if (GUILayout.Button("Cancel"))
                {
                    LinkingParentNode = null;
                }
            }
            else if(LinkingParentNode.GetChildren().Contains(item.name))
            {
                if (GUILayout.Button("Unlink"))
                {
                    
                    LinkingParentNode.RemoveChild(item.name);
                    LinkingParentNode = null;
                }
            }
            else
            {
                if (GUILayout.Button("Child"))
                {
                    
                    LinkingParentNode.AddChild(item.name);
                    LinkingParentNode = null;
                }
            }
        }

        /// <summary>
        /// function used to draw a line between the two linked nodes
        /// </summary>
        /// <param name="item"></param>
        private void DrawConnections(DialogueNode item)
        {
            Vector3 startPosition = new Vector2(item.GetRect().xMax, item.GetRect().center.y);
            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(item))
            {
                Vector3 endPosition = new Vector2(childNode.GetRect().xMin, childNode.GetRect().center.y);
                Vector3 controlPointOffset = endPosition-startPosition;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(startPosition, endPosition, 
                                   startPosition+controlPointOffset, endPosition-controlPointOffset,
                                   Color.white,null,4.0f);
            }
        }
    }
}