using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[ExecuteAlways]
public class Sprite3D : MonoBehaviour
{
    [SerializeField] private bool locked = true;
    private MeshRenderer mr;

    public bool flipX;
    public Texture sprite;
    
    // Start is called before the first frame update
    void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localScale = transform.localScale;
        transform.localScale = new Vector3(Mathf.Abs(localScale.x) * (flipX ? -1 : 1), localScale.y, localScale.z);
        if (Application.IsPlaying(gameObject))
        {
            mr.material.mainTexture = sprite;
        }else if (!locked)
        {
            
        }else
        {
            sprite = mr.sharedMaterial.mainTexture;
        }
    }
}
