using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
/*
 * [Rico,Alex]
 * 5/10/24
 * 
 */
public class CharacterSelect : MonoBehaviour
{
    public List<PlayerController> _playerTypes = new List<PlayerController>();
    public int _playerCount = 0;
    public int _charactersChosen = 0;
    private bool playerAmtSet = false;
    [SerializeField] private GameObject playerAmtScreen;
    [SerializeField] private GameObject characterSelectScreen;
    [SerializeField] private GameObject readyButton;
    [SerializeField] private GameObject confirmButton;
    [SerializeField] private GameObject backButton;

    [SerializeField] private GameObject firstCharacterSelect;
    [SerializeField] private GameObject firstPlayerCountSelect;

    private void Awake()
    {
        //check if level one first
        playerAmtScreen.SetActive(true);
        confirmButton.SetActive(false);
        characterSelectScreen.SetActive(false);
        readyButton.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstPlayerCountSelect);

    }

    public void OnePlayer()
    {
        _playerCount = 1;
        confirmButton.SetActive(true);
        confirmButton.SetActive(true);
    }
    public void TwoPlayer()
    {
        _playerCount = 2;
        confirmButton.SetActive(true);

    }
    public void ThreePlayer()
    {
        _playerCount = 3;
        confirmButton.SetActive(true);

    }
    public void FourPlayer()
    {
        _playerCount = 4;
        confirmButton.SetActive(true);

    }
    public void ConfirmPlayers()
    {
        playerAmtScreen.SetActive(false);
        characterSelectScreen.SetActive(true);
        confirmButton.SetActive(false);
        playerAmtSet = true;
        EventSystem.current.SetSelectedGameObject(firstCharacterSelect);
    }
    public void Back()
    {
        playerAmtScreen.SetActive(true);
        characterSelectScreen.SetActive(false);
        readyButton.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstPlayerCountSelect);
    }
    public void WarriorButton()
    {
        _charactersChosen += 1;
        Debug.Log("spawn warrior");
        PlayersChosen();
    }
    public void ValkyrieButton()
    {
        _charactersChosen += 1;
        Debug.Log("spawn valk");
        PlayersChosen();
    }
    public void WizardButton()
    {
        _charactersChosen += 1;
        Debug.Log("spawn wizard");
        PlayersChosen();
    }
    public void ElfButton()
    {
        _charactersChosen += 1;
        PlayersChosen();

        Debug.Log("spawn elf");
    }
    public void PlayersChosen()
    {
        if ((_playerCount >= 1)
            && (_playerCount == _charactersChosen) 
            && (playerAmtSet == true))
                readyButton.SetActive(true);
    }
    public void ReadyButton()
    {
        playerAmtScreen.SetActive(false);
        characterSelectScreen.SetActive(false);
        readyButton.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);

    }
}