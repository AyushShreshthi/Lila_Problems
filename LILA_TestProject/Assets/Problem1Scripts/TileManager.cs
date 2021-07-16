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


        PopulateAdjacentUp(go);
        PopulateAdjacentDown(go);
        PopulateAdjacentRight(go);
        PopulateAdjacentLeft(go);
        PopulateAdjacentUpRight(go);
        PopulateAdjacentUpLeft(go);
        PopulateAdjacentDownRight(go);
        PopulateAdjacentDownLeft(go);
    }

    public void PopulateAdjacentUp(GameObject go)
    {
        for (int i = 1; i <= areaOfInterest; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(go.transform.position + (Vector3.up * 100f), Vector2.up, 300f);
            Debug.DrawRay(go.transform.position, Vector2.up * 20f);
            GameObject goup = new GameObject();
            
            if (hit.collider != null && hit.collider.transform.gameObject != go)
            {
                goup = hit.transform.gameObject;
                int childnumberup = goup.transform.GetSiblingIndex();
                goup.GetComponentInChildren<Text>().text = (childnumberup + 1).ToString();
                goup.GetComponent<Image>().color = colors[childnumberup];
            }
            go = goup;
        }
        
    }
    public void PopulateAdjacentDown(GameObject go)
    {
        for (int i = 1; i <= areaOfInterest; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(go.transform.position + (-Vector3.up * 100f), -Vector2.up, 300f);
            Debug.DrawRay(go.transform.position, -Vector2.up * 20f);
            GameObject goup = new GameObject();

            if (hit.collider != null && hit.collider.transform.gameObject != go)
            {
                goup = hit.transform.gameObject;
                int childnumberup = goup.transform.GetSiblingIndex();
                goup.GetComponentInChildren<Text>().text = (childnumberup + 1).ToString();
                goup.GetComponent<Image>().color = colors[childnumberup];
            }
            go = goup;
        }

    }
    public void PopulateAdjacentRight(GameObject go)
    {
        for (int i = 1; i <= areaOfInterest; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(go.transform.position + (Vector3.right * 100f), Vector2.right, 300f);
            Debug.DrawRay(go.transform.position, Vector2.right * 20f);
            GameObject goup = new GameObject();

            if (hit.collider != null && hit.collider.transform.gameObject != go)
            {
                goup = hit.transform.gameObject;
                int childnumberup = goup.transform.GetSiblingIndex();
                goup.GetComponentInChildren<Text>().text = (childnumberup + 1).ToString();
                goup.GetComponent<Image>().color = colors[childnumberup];
            }
            go = goup;
        }

    }
    public void PopulateAdjacentLeft(GameObject go)
    {
        for (int i = 1; i <= areaOfInterest; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(go.transform.position + (-Vector3.right * 100f), -Vector2.right, 300f);
            Debug.DrawRay(go.transform.position, -Vector2.right * 20f);
            GameObject goup = new GameObject();

            if (hit.collider != null && hit.collider.transform.gameObject != go)
            {
                goup = hit.transform.gameObject;
                int childnumberup = goup.transform.GetSiblingIndex();
                goup.GetComponentInChildren<Text>().text = (childnumberup + 1).ToString();
                goup.GetComponent<Image>().color = colors[childnumberup];
            }
            go = goup;
        }

    }

    public void PopulateAdjacentUpRight(GameObject go)
    {
        for (int i = 1; i <= areaOfInterest; i++)
        {
            Vector3 direction =  go.transform.GetChild(1).transform.position-go.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(go.transform.GetChild(1).transform.position, direction, 100f);
            Debug.DrawRay(go.transform.GetChild(1).transform.position, direction * 100f, Color.yellow);
            GameObject goup = new GameObject();

            if (hit.collider != null && hit.collider.transform.gameObject != go)
            {
                
                goup = hit.transform.gameObject;
                int childnumberup = goup.transform.GetSiblingIndex();
                goup.GetComponentInChildren<Text>().text = (childnumberup + 1).ToString();
                goup.GetComponent<Image>().color = colors[childnumberup];
            }
            else
            {
                break;
            }
            go = goup;
        }
    }
    public void PopulateAdjacentUpLeft(GameObject go)
    {
        for (int i = 1; i <= areaOfInterest; i++)
        {
            Vector3 direction = go.transform.GetChild(2).transform.position - go.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(go.transform.GetChild(2).transform.position, direction, 100f);
            Debug.DrawRay(go.transform.GetChild(2).transform.position, direction * 100f, Color.yellow);
            GameObject goup = new GameObject();

            if (hit.collider != null && hit.collider.transform.gameObject != go)
            {

                goup = hit.transform.gameObject;
                int childnumberup = goup.transform.GetSiblingIndex();
                goup.GetComponentInChildren<Text>().text = (childnumberup + 1).ToString();
                goup.GetComponent<Image>().color = colors[childnumberup];
            }
            else
            {
                break;
            }
            go = goup;
        }
    }

    public void PopulateAdjacentDownRight(GameObject go)
    {
        for (int i = 1; i <= areaOfInterest; i++)
        {
            Vector3 direction = go.transform.GetChild(3).transform.position - go.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(go.transform.GetChild(3).transform.position, direction, 100f);
            Debug.DrawRay(go.transform.GetChild(3).transform.position, direction * 100f, Color.yellow);
            GameObject goup = new GameObject();

            if (hit.collider != null && hit.collider.transform.gameObject != go)
            {

                goup = hit.transform.gameObject;
                int childnumberup = goup.transform.GetSiblingIndex();
                goup.GetComponentInChildren<Text>().text = (childnumberup + 1).ToString();
                goup.GetComponent<Image>().color = colors[childnumberup];
            }
            else
            {
                break;
            }
            go = goup;
        }
    }

    public void PopulateAdjacentDownLeft(GameObject go)
    {
        for (int i = 1; i <= areaOfInterest; i++)
        {
            Vector3 direction = go.transform.GetChild(4).transform.position - go.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(go.transform.GetChild(4).transform.position, direction, 100f);
            Debug.DrawRay(go.transform.GetChild(4).transform.position, direction * 100f, Color.yellow);
            GameObject goup = new GameObject();

            if (hit.collider != null && hit.collider.transform.gameObject != go)
            {

                goup = hit.transform.gameObject;
                int childnumberup = goup.transform.GetSiblingIndex();
                goup.GetComponentInChildren<Text>().text = (childnumberup + 1).ToString();
                goup.GetComponent<Image>().color = colors[childnumberup];
            }
            else
            {
                break;
            }
            go = goup;
        }
    }
}
