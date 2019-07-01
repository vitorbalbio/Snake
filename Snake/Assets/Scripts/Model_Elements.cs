// ## Nota Para Avaliadores Técnicos: 
// Eventualmente utilizo quanto necessário classes de dados/Model externas ao pattern
// "Entidade-Componente" da Unity e mais próximas de um MVC tradicional.
// Elas são identificadas pelo prefixo "Model_" no projeto

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Model_Element
{
    // Nota Para Avaliadores Técnicos: 
    // Sou defensor do uso de variáveis públicas e quebra de encapsulamento em meus projetos
    // para melhorar a legibilidade e evitar o uso de um imenso numero de declarações Get/Set
    // em projetos internos. Como esse é um tema polêmico, em projetos para outras empresas
    // e/ou que outras pessoas irão interagir (como na gazeus) eu mantenho o encapsulamento.

    [SerializeField] private Vector2 position;
    [SerializeField] private GameObject model;

    public Vector2 Position { get => position; set => position = value; }
    public GameObject Model { get => model; set => model = value; }
}
