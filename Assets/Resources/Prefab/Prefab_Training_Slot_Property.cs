using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prefab_Training_Slot_Property : MonoBehaviour
{
    public Text name;
    public Text change_STR, change_DEX, change_CON;
    public Text change_INT, change_WIS, change_WIL;

    public Training_Infra training_infra = new Training_Infra();
}
