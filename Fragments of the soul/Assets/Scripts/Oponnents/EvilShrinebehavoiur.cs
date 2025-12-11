using System.Collections.Generic;
using UnityEngine;

public class EvilShrinebehavoiur : MonoBehaviour
{


    public GameObject shadowCreature;

    GameObject clone;

    bool hasSpawned;
    // for init
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float canSeeDistance = 10f;  //Todo: dodaæ widocznoœæ, sprawdzanie, ¿eby przez œciany nie by³o widaæ
    [SerializeField] protected float maxHp = 50;
    public PlaytimeKeyBind pausedGameManager;
    public Map gridManager;
    public GameObject playerObject;
    public Player player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasSpawned = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerObject.transform.position) <= 4f && !hasSpawned)
            {
            if (player.getCorruprionLevel() > 50) 
            {
                hasSpawned = true;

                clone = Instantiate(shadowCreature, transform.position + new Vector3(0f, -1f, 0), Quaternion.identity);
                clone.GetComponent<ShadowBehaviour>().Init(speed, canSeeDistance, maxHp, pausedGameManager,
                    gridManager, playerObject, player);
            }

            }
    }
}



