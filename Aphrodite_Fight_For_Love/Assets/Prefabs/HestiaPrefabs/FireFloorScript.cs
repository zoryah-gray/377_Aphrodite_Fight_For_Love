using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AphroditeFightCode
{
    public class FireFloorScript : MonoBehaviour
    {
        public enum State
        {
            empty,
            ff1,
            ff2,
            ffdamage
        }

        public State currentState = State.ff1;

        public Sprite ff_empty;
        public Sprite ff1_Sprite;
        public Sprite ff2_Sprite;
        public Sprite ffdamage_Sprite;
        private SpriteRenderer tileSpriteRenderer;

        private float loopEnd;
        private float loopInterval = 10f;


        // Start is called before the first frame update
        void Start()
        {
            tileSpriteRenderer = GetComponent<SpriteRenderer>();
            loopEnd = -loopInterval;

        }
        // Update is called once per frame
        void Update()
        {
            if (Time.time - loopEnd > 2f) { ChangeState(State.ff1); }
            if (Time.time - loopEnd >= 4f) { ChangeState(State.ff2); }
            if (Time.time - loopEnd >= 6f) { ChangeState(State.ffdamage); }
            if (Time.time - loopEnd > loopInterval)
            {
                Debug.Log("DO DAMAGE TO PLAYER");
                ChangeState(State.empty);
                loopEnd = Time.time;
            }
            UpdateSprite();
        }

        void ChangeState(State newTileState)
        {
            if (currentState != newTileState) { currentState = newTileState; }
        }

        void UpdateSprite()
        {
            switch (currentState)
            {
                case State.empty:
                    tileSpriteRenderer.sprite = ff_empty;
                    break;
                case State.ff1:
                    tileSpriteRenderer.sprite = ff1_Sprite;
                    break;
                case State.ff2:
                    tileSpriteRenderer.sprite = ff2_Sprite;
                    break;
                case State.ffdamage:
                    tileSpriteRenderer.sprite = ffdamage_Sprite;
                    break;
            }
        }
    }
}
