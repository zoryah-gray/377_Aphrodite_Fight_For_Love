using AphroditeFightCode;
using UnityEngine;

public class FireVariant : MonoBehaviour
{

    private Vector3Int position;

    private TileData data;

    private FireManager fireManager;

    public float FireAttack;

    private float burnTimeCounter, spreadIntervallCounter;




    public void StartBurning(Vector3Int position, TileData data, FireManager fm)
    {
        this.position = position;
        this.data = data;
        fireManager = fm;

        burnTimeCounter = data.burnTime;
        spreadIntervallCounter = data.spreadIntervall;
        Debug.Log("tile data:" + data);
        Debug.Log("fire manager:" + fm);
    }



    private void Update()
    {
        burnTimeCounter -= Time.deltaTime;
        if (burnTimeCounter <= 0)
        {
            fireManager.FinishedBurning(position);
            Destroy(gameObject);
        }

        spreadIntervallCounter -= Time.deltaTime;
        if (spreadIntervallCounter <= 0)
        {
            spreadIntervallCounter = data.spreadIntervall;
            fireManager.TryToSpread(position, data.spreadChance);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Fire collision: " + collision.gameObject.name);
        if (collision.gameObject.name == "Player")
        {
            GameData.CheckPlayerHealth(FireAttack);
        }
    }
}