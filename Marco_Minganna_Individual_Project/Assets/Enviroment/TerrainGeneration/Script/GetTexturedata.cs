using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GetTexturedata : MonoBehaviour {

    public Sprite GrassTop;

    public string StringName;
    private string GrassTopVectors ;

    int index = 0;


    void CreateFile(string result)
    {
        string path = Application.dataPath + "/UVLocations/"+StringName+".txt";
        if(!File.Exists(path))
        {
            File.WriteAllText(path,result);
        }
    }



    string GetUVSizes()
    {
        string[] NameSplitted = GrassTop.name.Split(new[] { ',', '|' });

        Vector2[] sizes = new Vector2[NameSplitted.Length/2];

        string result = "";

        for (int i = 0; i < NameSplitted.Length; i ++)
        {

            if (NameSplitted[i].Contains(".")) 
            {
                NameSplitted[i] += "f";
            }
            else
            {
                NameSplitted[i] += ".0f";
            }



        }


        for (int i = 0; i < NameSplitted.Length; i+=2)
        {

            if(i==0)
            {
                result += "{new Vector2(" + NameSplitted[i] + " , " + NameSplitted[i + 1] + "),";
            }
            else if(i< NameSplitted.Length-2)
            {
                result += "new Vector2(" + NameSplitted[i] + " , " + NameSplitted[i + 1] + "),";
            }
            else
            {
                result += "new Vector2(" + NameSplitted[i] + " , " + NameSplitted[i + 1] + ")},";
            }

           
        }




        return result;
    }
	// Use this for initialization
	void Start () {


        GrassTopVectors = GetUVSizes();
         CreateFile(GrassTopVectors);

       // Debug.Log("New Vector2(" +GrassTopVectors[0]+"), "+ "New Vector2(" + GrassTopVectors[1] + "), "+ "New Vector2(" + GrassTopVectors[2] + "), "+ "New Vector2(" + GrassTopVectors[3] + "); ");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
