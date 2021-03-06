using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk {

	public Material cubeMaterial;
    public Material fluidMaterial;
	public Block[,,] chunkData;
	public GameObject chunk;
    public GameObject fluid;

	void BuildChunk()
	{
		chunkData = new Block[World.chunkSize,World.chunkSize,World.chunkSize];

		for(int z = 0; z < World.chunkSize; z++)
			for(int y = 0; y < World.chunkSize; y++)
				for(int x = 0; x < World.chunkSize; x++)
				{
					Vector3 pos = new Vector3(x,y,z);
					int worldX = (int)(x + chunk.transform.position.x);
					int worldY = (int)(y + chunk.transform.position.y);
					int worldZ = (int)(z + chunk.transform.position.z);

                    if(Utils.fBM3D(worldX,worldY,worldZ,0.1f,3)<0.45f)
                    {
                        chunkData[x, y, z] = new Block(Block.BlockType.AIR, pos,
                                                       chunk.gameObject, this);
                    }
					if(worldY <= Utils.GenerateStoneHeight(worldX,worldZ))
						chunkData[x,y,z] = new Block(Block.BlockType.STONE, pos, 
						                chunk.gameObject, this);
					else if(worldY == Utils.GenerateHeight(worldX,worldZ))
						chunkData[x,y,z] = new Block(Block.BlockType.GRASS, pos, 
						                chunk.gameObject, this);
					else if(worldY < Utils.GenerateHeight(worldX,worldZ))
						chunkData[x,y,z] = new Block(Block.BlockType.DIRT, pos, 
						                chunk.gameObject, this);
                    else if (worldY < 10)
                        chunkData[x, y, z] = new Block(Block.BlockType.WATER, pos,
                                        fluid.gameObject, this);
                    else
						chunkData[x,y,z] = new Block(Block.BlockType.AIR, pos, 
						                chunk.gameObject, this);

                   
				}
	}

	public void DrawChunk()
	{
		for(int z = 0; z < World.chunkSize; z++)
			for(int y = 0; y < World.chunkSize; y++)
				for(int x = 0; x < World.chunkSize; x++)
				{
					chunkData[x,y,z].Draw();	
				}
		CombineQuads(chunk.gameObject,cubeMaterial);
        MeshCollider collider = chunk.gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        collider.sharedMesh = chunk.transform.GetComponent<MeshFilter>().mesh;

        CombineQuads(fluid.gameObject, fluidMaterial);
        

	}

	// Use this for initialization
	public Chunk (Vector3 position, Material c,Material t) {
		
		chunk = new GameObject(World.BuildChunkName(position));
		chunk.transform.position = position;
        fluid = new GameObject(World.BuildChunkName(position) + "_F");
        fluid.transform.position = position;

		cubeMaterial = c;
        fluidMaterial = t;
		BuildChunk();
	}
	
	void CombineQuads(GameObject o,Material m)
	{
		//1. Combine all children meshes
		MeshFilter[] meshFilters = o.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length) {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            i++;
        }

        //2. Create a new mesh on the parent object
        MeshFilter mf = (MeshFilter) o.gameObject.AddComponent(typeof(MeshFilter));
        mf.mesh = new Mesh();

        //3. Add combined meshes on children as the parent's mesh
        mf.mesh.CombineMeshes(combine);

        //4. Create a renderer for the parent
		MeshRenderer renderer = o.gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		renderer.material = m;

		//5. Delete all uncombined children
		foreach (Transform quad in chunk.transform) {
     		GameObject.Destroy(quad.gameObject);
 		}

	}

}
