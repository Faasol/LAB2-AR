using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class CubeGenerator : MonoBehaviour
{
    // Variables to store config data
    private int N;
    private float minX, maxX, minY, maxY, minZ, maxZ, minV, maxV;

    // Lists to track the cubes and their specific rotation data
    private List<GameObject> cubeList = new List<GameObject>();
    private List<Vector3> rotationAxes = new List<Vector3>();
    private List<float> rotationSpeeds = new List<float>();

    void Start()
    {
        ReadConfigFile();
        GenerateCubes();
    }

    void ReadConfigFile()
    {
        string path = "./Assets/config.txt";

        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("#") || string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split(' ');
                    string key = parts[0];
                    float value = float.Parse(parts[1]);

                    // Assign values based on the key in the text file
                    switch (key)
                    {
                        case "N": N = (int)value; break;
                        case "minX": minX = value; break;
                        case "maxX": maxX = value; break;
                        case "minY": minY = value; break;
                        case "maxY": maxY = value; break;
                        case "minZ": minZ = value; break;
                        case "maxZ": maxZ = value; break;
                        case "minV": minV = value; break;
                        case "maxV": maxV = value; break;
                    }
                }
            }
        }
    }

    void GenerateCubes()
    {
        for (int i = 0; i < N; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            cube.transform.position = new Vector3(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY),
                Random.Range(minZ, maxZ)
            );

            float randomSize = Random.Range(0.2f, 1.5f);
            cube.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

            Renderer rend = cube.GetComponent<Renderer>();
            rend.material.color = new Color(Random.value, Random.value, Random.value);

            cubeList.Add(cube);
            rotationAxes.Add(new Vector3(Random.value, Random.value, Random.value));
            rotationSpeeds.Add(Random.Range(minV, maxV));
        }
    }

    void Update()
    {
        for (int i = 0; i < cubeList.Count; i++)
        {
            cubeList[i].transform.Rotate(rotationAxes[i], rotationSpeeds[i] * 10f * Time.deltaTime);
        }
    }
}