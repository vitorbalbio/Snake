using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Item : MonoBehaviour
{
    // Nota Para Avaliadores Técnicos: 
    // Sou defensor do uso de variáveis públicas e quebra de encapsulamento em meus projetos
    // para melhorar a legibilidade e evitar o uso de um imenso numero de declarações Get/Set
    // em projetos internos. Como esse é um tema polêmico, em projetos para outras empresas
    // e/ou que outras pessoas irão interagir (como na gazeus) eu mantenho o encapsulamento.

    private Vector2 position;
    public Vector2 Position { get => position; set => position = value; }

    public void SetPosition(Vector2 _position) {
        Position = _position;
        transform.position = new Vector3(Position[0], 0, Position[1]);
    }
}
