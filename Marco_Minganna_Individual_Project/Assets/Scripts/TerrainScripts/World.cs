﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class that keep track of the entire terrain created in chunks
/// </summary>
public class World : MonoBehaviour {

	public Material textureAtlas;
    public Material fluidTexture;
	public static int columnHeight = 16;
	public static int chunkSize = 16;
	public static int worldSize = 5;
	public static Dictionary<string, Chunk> chunks;

	public static string BuildChunkName(Vector3 v)
	{
		return (int)v.x + "_" + 
			         (int)v.y + "_" + 
			         (int)v.z;
	}

	IEnumerator BuildChunkColumn()
	{
		for(int i = 0; i < columnHeight; i++)
		{
			Vector3 chunkPosition = new Vector3(this.transform.position.x, 
												i*chunkSize, 
												this.transform.position.z);
			Chunk c = new Chunk(chunkPosition, textureAtlas,fluidTexture);
			c.chunk.transform.parent = this.transform;
			chunks.Add(c.chunk.name, c);
		}

		foreach(KeyValuePair<string, Chunk> c in chunks)
		{
			c.Value.DrawChunk();
			yield return null;
		}
		
	}

	IEnumerator BuildWorld()
	{
        for (int z = 0; z < worldSize; z++)
            for (int x = 0; x < worldSize; x++)
                for (int y = 0; y < columnHeight; y++)
                {
                    Vector3 chunkPosition = new Vector3(x * chunkSize, y * chunkSize, z * chunkSize);
                    Chunk c = new Chunk(chunkPosition, textureAtlas,fluidTexture);
                    c.chunk.transform.parent = this.transform;
                    chunks.Add(c.chunk.name, c);

                }

        foreach (KeyValuePair<string, Chunk> c in chunks)
        {
            c.Value.DrawChunk();
            yield return null;
        }


    }

	// Use this for initialization
	void Start () {
       
		chunks = new Dictionary<string, Chunk>();
		this.transform.position = Vector3.zero;
		this.transform.rotation = Quaternion.identity;
		StartCoroutine(BuildWorld());
	}
}
