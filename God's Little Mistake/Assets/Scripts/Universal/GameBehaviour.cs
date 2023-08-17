using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : LC.Behaviour //inherits from
{
    //unquie to this project
    protected static GameManager _GM { get { return GameManager.INSTANCE; } }
    protected static UIManager _UI { get { return UIManager.INSTANCE; } }
    protected static PlayerController _PC { get { return PlayerController.INSTANCE; } }
    protected static ItemDataBase _ItemD { get { return ItemDataBase.INSTANCE; } }
    protected static InSceneItemDataBase _ISitemD { get { return InSceneItemDataBase.INSTANCE; } }
    protected static ItemGeneration _IG { get { return ItemGeneration.INSTANCE; } }
    protected static ItemsOnPlayer _AVTAR { get { return ItemsOnPlayer.INSTANCE; } }

    protected static MeleeAttack _MA { get { return MeleeAttack.INSTANCE; } }

   


    //protected static SceneController _SC { get { return SceneController.INSTANCE; } }


}
//
// Instanced GameBehaviour
//
public class GameBehaviour<T> : GameBehaviour where T : GameBehaviour
{
    static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameBehaviour<" + typeof(T).ToString() + "> not instantiated!\nNeed to call Instantiate() from " + typeof(T).ToString() + "Awake().");
            return _instance;
        }
    }
    //
    // Instantiate singleton
    // Must be called first thing on Awake()
    protected bool Instantiate()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Instance of GameBehaviour<" + typeof(T).ToString() + "> already exists! Destroying myself.\nIf you see this when a scene is LOADED from another one, ignore it.");
            DestroyImmediate(gameObject);
            return false;
        }
        _instance = this as T;
        return true;
    }

}
