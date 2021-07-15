using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    [SerializeField] private int numberOfTiles;
    [SerializeField] private int areaOfInterest;

    public List<Color> colors = new List<Color>();

    public GameObject gridPanel;
    public GameObject tilePrefab;

    void Start()
    {
        for(int i = 1; i <= numberOfTiles; i++)
        {
            GameObject go = Instantiate(tilePrefab);

            Color color = Random.ColorHSV(0f, 1f);
            colors.Add(color);

            go.transform.SetParent(gridPanel.transform);
            go.GetComponent<Button>().onClick.AddListener(()=> PopulateData(go));
        }
    }

    public void PopulateData(GameObject go)
    {
        int childnumber = go.transform.GetSiblingIndex();
        go.GetComponentInChildren<Text>().text = (childnumber + 1).ToString();
        go.GetComponent<Image>().color = colors[childnumber];
    }
}
