using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;

public class TextureManager : MonoBehaviour
{
    private Texture2D[] textureArray = new Texture2D[10];
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        for (int i = 0; i < 10; i++)
        {
            string fileName = i.ToString("D2") + ".png"; 
            string path = "./Assets/textures/" + fileName;

            if (File.Exists(path))
            {
                byte[] bytes = File.ReadAllBytes(path);
                Texture2D texture = new Texture2D(100, 100);
                texture.filterMode = FilterMode.Trilinear;
                texture.LoadImage(bytes);
                textureArray[i] = texture;
            }
        }
        if (textureArray[0] != null) ApplyTexture(0);
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "IterativeCube")
                {
                    Debug.Log("New Input System: You hit the cube!");
                    int randomIndex = Random.Range(0, 10);
                    ApplyTexture(randomIndex);
                }
            }
        }
    }

    void ApplyTexture(int index)
    {
        if (textureArray[index] == null) return;
        Material mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        mat.mainTexture = textureArray[index];
        meshRenderer.material = mat;
    }
}