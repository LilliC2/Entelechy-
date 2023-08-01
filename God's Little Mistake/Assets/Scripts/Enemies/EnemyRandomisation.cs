using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomisation : GameBehaviour
{
    float rotation;
    public GameObject[] slots;

    string[] categoryHead = new string[] { "Eyes", "Horns" };
    string[] categoryTorso = new string[] { "Slappies", "Launchers" }; //ADD PUNCHIES WHEN AN ITEM IS ADDED
    string[] categoryLegs = new string[] { "Legs", "Crawlies"};

    public List<string> categories;

    public Sprite[] enemySpritesArray;
    public SpriteRenderer enemySprite;

    public Color[] hueArray;
    Color colour;


    // Update is called once per frame
    void Start()
    {
        //
        colour = hueArray[Random.Range(0, hueArray.Length)];

        enemySprite.sprite = enemySpritesArray[Random.Range(0, enemySpritesArray.Length)];


        enemySprite.color = colour;


        //each enemy needs atleast legs

        //Top slot
        //HEAD
        if (Random.Range(0,2) == 1)
        {
            EquipItem(0, GenerateItem(categoryHead[Random.Range(0, categoryHead.Length )]));

        }
        
        if(Random.Range(0, 2) == 1)
        {
            EquipItem(1, GenerateItem(categoryHead[Random.Range(0, categoryHead.Length)]));

        }
        if(Random.Range(0, 2) == 1)
        {
            EquipItem(2, GenerateItem(categoryHead[Random.Range(0, categoryHead.Length)]));

        }
        
        //MOUTH
        if(Random.Range(0, 2) == 1)
        {
            EquipItem(3, GenerateItem("Mouth"));


        }

        //TORSO
        if (Random.Range(0, 2) == 1)
        {
            EquipItem(4, GenerateItem(categoryTorso[Random.Range(0, categoryTorso.Length)]));

        }
        if (Random.Range(0, 2) == 1)
        {
            EquipItem(5, GenerateItem(categoryTorso[Random.Range(0, categoryTorso.Length)]));

        }
        //var gencat = categoryTorso[Random.Range(0, categoryTorso.Length)];
        //print("generating cat = " + gencat);
        //GameObject go = GenerateItem(gencat);
        //print(go.name);
        //EquipItem(4, go);

        //LEGS
        EquipItem(6, GenerateItem(categoryLegs[Random.Range(0,categoryLegs.Length)]));



    }

    void EquipItem(int _slot, GameObject _prefab)
    {
        bool flip = false;
        switch (_slot)
        {

            case 2:
                flip = true;
                break;
            case 5:
                flip = true;
                break;
            default:
                rotation = 0;
                break;


        }
        var item = Instantiate(_prefab, slots[_slot].transform);
        item.transform.localEulerAngles = new Vector3(item.transform.rotation.x, item.transform.rotation.y, rotation);
        if(flip) item.transform.localScale = new Vector3(-1, 0, -1);

        SpriteRenderer sprite = item.GetComponentInChildren<SpriteRenderer>();
        sprite.color = colour;

    }

    public GameObject GenerateItem(string _category)
    {
        GameObject prefab = null;

        categories.Add(_category);

        for (int i = 0; i < _ItemD.itemDataBase.Length; i++)
        {
            if (_ItemD.itemDataBase[i].category.ToString() == _category)
            {
                //possibleDrops.Add(_ItemD.itemDataBase[i]);
                prefab = _ItemD.itemDataBase[i].avtarPrefab;

            }
        }
        return prefab;
    }
}
