using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AphroditeFightCode
{
    public class FireManager : MonoBehaviour
    {


        [SerializeField]
        private Tilemap map;

        //[SerializeField]
        //private TileBase ashTile;


        [SerializeField]
        private MapManager mapManager;

        [SerializeField]
        private Fire firePrefab;


        private List<Vector3Int> activeFires = new List<Vector3Int>();

        private float timer = 0f;

        public float instantiateInterval = 10f; // Time interval between instantiations


        public List<Vector3Int> tilestarters;

        public bool isBossFight;

        public bool notrandomspread;

        


        public void FinishedBurning(Vector3Int position)
        {
            //map.SetTile(position, ashTile);
            activeFires.Remove(position);
        }

        public void TryToSpread(Vector3Int position, float spreadChance)
        {
            for (int x = position.x - 1; x < position.x + 2; x++)
            {
                for (int y = position.y - 1; y < position.y + 2; y++)
                {
                    TryToBurnTile(new Vector3Int(x, y, 0));
                }
            }


            void TryToBurnTile(Vector3Int tilePosition)
            {
                if (activeFires.Contains(tilePosition)) return;

                TileData data = mapManager.GetTileData(tilePosition);

                if (data != null && data.canBurn)
                {
                if (notrandomspread)
                {
                    SetTileOnFire(tilePosition, data);
                }
                else
                {
                    if (UnityEngine.Random.Range(0f, 100f) <= data.spreadChance)
                        SetTileOnFire(tilePosition, data);
                }
                }

            }

        }

        private void SetTileOnFire(Vector3Int tilePosition, TileData data)
        {
            Fire newFire = Instantiate(firePrefab);
            newFire.transform.position = map.GetCellCenterWorld(tilePosition);
            newFire.StartBurning(tilePosition, data, this);

            activeFires.Add(tilePosition);


        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.name == "Player" && GameData.inQuest)
            {
                Vector2 player_pos = collision.gameObject.transform.position;
                Vector3Int gridPosition = map.WorldToCell(player_pos);
                Debug.Log("player:" + player_pos);
                Debug.Log("grid:" + gridPosition);
                TileData data = mapManager.GetTileData(gridPosition);
                SetTileOnFire(gridPosition, data);
            }
        }

    private void Start()
    {
        if (isBossFight)
        {
            SetLongBurn();
        }
    }

    private void Update()
    {
        //if (isBossFight)
        //{
        //    timer += Time.deltaTime;

        //    // Check if it's time to instantiate the GameObject
        //    if (timer >= instantiateInterval)
        //    {
        //        SetQuickBurn();
        //        timer = 0f;
        //    }
            

        //}
    }


    private void SetLongBurn()
        {
        
            foreach (Vector3Int tilestarter in tilestarters)
            {
                if (activeFires.Contains(tilestarter)) continue;
                TileData data = mapManager.GetTileData(tilestarter);
                SetTileOnFire(tilestarter, data);
            }
  
    }

    }
}