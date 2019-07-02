// ## Nota Para Avaliadores Técnicos: 
// Utilizo quanto necessário classes de dados/Model externas ao pattern
// "Entidade-Componente" da Unity e mais próximas de um MVC tradicional.
// Elas são identificadas pelo prefixo "Model_" no projeto

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Model_Element
{

    [SerializeField] private Vector2 position;
    [SerializeField] private GameObject model;

    public Vector2 Position { get => position; set => position = value; }
    public GameObject Model { get => model; set => model = value; }
}
