using UnityEngine;

public class GameState : MonoBehaviour
{
    public int Money = 0;
    public bool HasDoughMachine = false;
    public bool HasDeliverator = false;
    public bool HasSupplyBot = false;
    public int NumToppersReplaced = 0;
    public GameManager.GameStage CurrentStage;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
