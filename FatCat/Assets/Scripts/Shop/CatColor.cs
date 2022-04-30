using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CatColor : MonoBehaviour
{
    public int Index;
    public Texture2D CatTexture;
    public Texture2D StomachTexture;
    private MeshRenderer _rendererOfStomach;
    private SkinnedMeshRenderer _rendererOfCat;
    private void Start()
    {
        _rendererOfCat = GameObject.FindGameObjectWithTag("Body").GetComponent<SkinnedMeshRenderer>();
        _rendererOfStomach = GameObject.FindObjectOfType<Stomach>().GetComponent<MeshRenderer>();
        this.GetComponent<Button>().onClick.AddListener(ChangeCatColor);
    }
    private void ChangeCatColor()
    {
        // color will be lay on texture
        // white - more neutral
        ProcessingPlayerData.ChangeCurrentColorCatIndex(Index);

        _rendererOfCat.material.color = Color.white;
        _rendererOfCat.material.mainTexture = CatTexture;
        _rendererOfStomach.material.mainTexture = StomachTexture;
    }
}
