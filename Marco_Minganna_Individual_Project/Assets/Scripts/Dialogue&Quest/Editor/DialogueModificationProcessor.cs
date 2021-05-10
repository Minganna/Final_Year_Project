using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

/// <summary>
/// namespace used for the dialogue editors classes
/// </summary>
namespace Dialogue.Editor
{
    /// <summary>
    /// used to keep the changes in the dialogue such as rename 
    /// </summary>
    public class DialogueModificationProcessor : UnityEditor.AssetModificationProcessor
    {
        private static AssetMoveResult OnWillMoveAsset(string sourcePath,string destinationPath)
        {
            Dialogue dialogue=AssetDatabase.LoadMainAssetAtPath(sourcePath) as Dialogue;

            if(dialogue==null)
            {
                return AssetMoveResult.DidNotMove;
            }    
            if(Path.GetDirectoryName(sourcePath)!=Path.GetDirectoryName(destinationPath))
            {
                return AssetMoveResult.DidNotMove;
            }

            dialogue.name = Path.GetFileNameWithoutExtension(destinationPath);
            return AssetMoveResult.DidNotMove;
        }
       
    }
}

