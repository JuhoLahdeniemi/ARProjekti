using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public int points;
    public TextMeshProUGUI gameOverText;
    public GameObject tunnel;
    public float randomY;
    public float randomX;
    public float randomZ;
    public float randomScale;
    // Start is called before the first frame update

    private void Awake()
    {
        // singleton
        // check if manager already exist
        if (manager == null)
        {
            // if not, this is it and it won't be destroyed when changing the scene
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

        void Start()
    {
        spawnTunnels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPoints(int points)
    {
        this.points += points;
    }

    public void resetPoints()
    {
        this.points = 0;

    }

    public int getPoints()
    {
        return this.points;
    }

    public void spawnTunnels()
    {
        StartCoroutine(tunnelsCoroutine());
    }

    IEnumerator tunnelsCoroutine()
    {
        yield return new WaitForSeconds(2);
        this.points = 0;
        this.gameOverText.text = "";

        for(int i = 0; i < 10; i++)
        {
            randomY = Random.Range(-10, 10);
            randomX = Random.Range(-10, 10);
            randomZ = Random.Range(30, 50);
            randomScale = Random.Range(-1, 5);
            GameObject tunnelObject = Instantiate(tunnel, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
            tunnelObject.transform.localScale *= randomScale;
            yield return new WaitForSeconds(3);
        }

        yield return new WaitForSeconds(4);
        this.gameOverText.text = "Game over." + "\n" + "Points: " + this.points.ToString();

    }
}
