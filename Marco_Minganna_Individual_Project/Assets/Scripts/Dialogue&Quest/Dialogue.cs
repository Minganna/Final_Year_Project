using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Namespace used for all the dialogue scripts
/// </summary>
namespace Dialogue
{
    /// <summary>
    /// Used to allow the creation of an object in unity, file name is the name given in creation, menuname is the the name where it will be found
    /// </summary>
    [CreateAssetMenu(fileName = "EmptyDialogue", menuName = "Dialogue", order = 0)]
    /// class that is used to create the dialogue scriptable objects
    public class Dialogue : ScriptableObject,ISerializationCallbackReceiver
    {
        [SerializeField]
        ///List of dialogue nodes
        List<DialogueNode> nodes=new List<DialogueNode>(); 
        [SerializeField]
        /// used to create the new node in a position that differs from the origin
        Vector2 newNodeOffset = new Vector2(250, 0);

        Dictionary<string, DialogueNode> nodeloockup = new Dictionary<string, DialogueNode>();

        /// <summary>
        /// This function is used to validate the connection between the nodes and their childrens 
        /// </summary>
        public void OnValidate()
        {
            nodeloockup.Clear();
            foreach(DialogueNode node in GetAllNodes())
            {
                nodeloockup[node.name] = node;
            }
        }

        /// <summary>
        /// This function is used to collect all the dialogue nodes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        /// <summary>
        /// This function is used to get the main node
        /// </summary>
        /// <returns></returns>
        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }
        /// <summary>
        /// This function is used to collect all the children of a particular node
        /// </summary>
        /// <param name="Parentnode"></param>
        /// <returns></returns>
        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode Parentnode)
        {
            foreach(string childId in Parentnode.GetChildren())
            {
                if(nodeloockup.ContainsKey(childId))
                {
                     yield return nodeloockup[childId];
                } 
            }

        }
        /// <summary>
        /// This function is used to get all the nodes linked to the player speech
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode currentNode)
        {
            foreach(DialogueNode node in GetAllChildren(currentNode))
            {
                if(node.isPlayerSpeaking())
                {
                    yield return node;
                }
            }
        }

        /// <summary>
        /// this function collect all the nodes of the AI speech
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        public IEnumerable<DialogueNode> GetAIChildren(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                if (!node.isPlayerSpeaking())
                {
                    yield return node;
                }
            }
        }

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                DialogueNode newNode= MakeNode(null);
                AddNode(newNode);
            }
            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach (DialogueNode node in GetAllNodes())
                {
                    if (AssetDatabase.GetAssetPath(node) == ""&&node!=null)
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }

#endif
        }

        public void OnAfterDeserialize()
        {
        }


#if UNITY_EDITOR
       
        /// <summary>
        /// Used to create a new node, this function is divided in two (Make node is part of this logic)
        /// </summary>
        /// <param name="creatingNode"></param>
        public void CreateNode(DialogueNode creatingNode)
        {
            DialogueNode newNode = MakeNode(creatingNode);
            Undo.RegisterCompleteObjectUndo(newNode, "Created Dialogue node");
            Undo.RecordObject(this, "Added Dialogue Node");
            AddNode(newNode);
        }

        /// <summary>
        /// Used to add all the component in the new node
        /// </summary>
        /// <param name="creatingNode"></param>
        /// <returns></returns>
        private DialogueNode MakeNode(DialogueNode creatingNode)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();
            if (nodes.Count == 0)
            {
                newNode.name = "MainNode";
            }
            else
            {
                newNode.name = Guid.NewGuid().ToString();
            }
            if (creatingNode != null)
            {
                creatingNode.AddChild(newNode.name);
                newNode.SetSpeaker(!creatingNode.isPlayerSpeaking());
                newNode.SetPosition(creatingNode.GetRect().position + newNodeOffset);
            }
            

            return newNode;
        }


        /// <summary>
        /// function used to add a new node
        /// </summary>
        /// <param name="newNode"></param>
        private void AddNode(DialogueNode newNode)
        {
            nodes.Add(newNode);
            OnValidate();
        }

        /// <summary>
        /// function used to delete a node 
        /// </summary>
        /// <param name="deletinggNode"></param>
        public void DeleteNode(DialogueNode deletinggNode)
        {
            Undo.RecordObject(this, "Deleted Dialogue Node");
            nodes.Remove(deletinggNode);
            OnValidate();
            CleanDanglingChildren(deletinggNode);
            Undo.DestroyObjectImmediate(deletinggNode);

        }
        /// <summary>
        /// function used to clean each nodes from deleted children nodes
        /// </summary>
        /// <param name="NodeToDelete"></param>
        private void CleanDanglingChildren(DialogueNode NodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(NodeToDelete.name);
            }
        }

#endif
    }
}


