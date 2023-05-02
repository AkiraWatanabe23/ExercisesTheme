using Constants;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _currentTurn = 1;

    public int CurrentTurn => _currentTurn;

    private void Start()
    {
        _currentTurn = Consts.WHITE;
    }

    private void Update()
    {
        
    }

    private int ChangeTurn()
    {
        return _currentTurn *= -1;
    }
}
