// Nota:
// Por conta da simplicidade do projeto essa classe ficou subutilizada
// Mas em projetos reais sempre que posso utilizo um ou mais UI Managers como este para centralizar
// chamadas da UI (Eventualmente um por tela) e ativar eventos em outras entidades.
// Isso me permite separar melhor o VIEW do CONTROLLER


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller_UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Text score;

    public static Controller_UIManager uiManager;

    

    public void Awake() {
        uiManager = this;
    }

    public void ShowMainMenu(int _score) {
        mainMenu.SetActive(true);
        UpdateScore(_score);
    }

    public void UpdateScore(int _score) {
        // Atualiza o placar
        score.text = _score.ToString();
    }

    public void HideMainMenu() {
        mainMenu.SetActive(false);
    }

    // Chamado pelo Evento do Botão
    public void StartGame_Click() {
        HideMainMenu();
        Controller_GameManager.main.StartGame();
    }
    
}
