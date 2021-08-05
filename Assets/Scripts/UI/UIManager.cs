using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SpawnShipUI spawnShipUI;
    [SerializeField] private DisplayStateUI displayStateUI;
    [SerializeField] private Player player;

    #region Singleton

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public void changePlayerState()
    {
        player.state++;
    }

    public void addShipCountOnScene()
    {
        player.shipsOnScene++;
    }

    public void setSelectedShip(GameObject obj)
    {
        player.selectedShip = obj;
    }

    public int checkShipCountOnScene()
    {
        return player.shipsOnScene;
    }

    public void changeDisplayText(string turntxt, string stagetxt)
    {
        displayStateUI.gameObject.SetActive(true);
        displayStateUI.changeTexts(turntxt, stagetxt);
    }

    public void animateSpawnShipUI(bool value, float delay)
    {       
        StartCoroutine(setDelay(value,delay,1));
    }

    public void animatedisplayUI(bool value, float delay)
    {        
        StartCoroutine(setDelay(value,delay,2));
    }

    private IEnumerator setDelay(bool value,float time,int swap)
    {
        yield return new WaitForSeconds(time);
        switch (swap)
        {
            case 1:
                spawnShipUI.animator.SetBool("isOpen", value);
                break;
            case 2:
                displayStateUI.animator.SetBool("isOpen", value);
                break;

        }             
    }

}
