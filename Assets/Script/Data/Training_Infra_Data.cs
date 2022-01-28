using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training_Infra_Data : MonoBehaviour
{
    public List<Dictionary<string, object>> training_infra_table;
    public Dictionary<int, Training_Infra>training_infra_data = new Dictionary<int, Training_Infra>();

    void Start()
    {
        training_infra_table = CSVReader.Read("DataBase/CSV/Training_Infra_Table");
        for (int i = 0; i < training_infra_table.Count; i++)
        {
            Training_Infra new_training_Infra = new Training_Infra();

            new_training_Infra.code = int.Parse(training_infra_table[i]["code"].ToString());
            new_training_Infra.name = training_infra_table[i]["name"].ToString();
            new_training_Infra.cost = int.Parse(training_infra_table[i]["cost"].ToString());
            new_training_Infra.change_STR = float.Parse(training_infra_table[i]["change_STR"].ToString());
            new_training_Infra.change_DEX = float.Parse(training_infra_table[i]["change_DEX"].ToString());
            new_training_Infra.change_CON = float.Parse(training_infra_table[i]["change_CON"].ToString());
            new_training_Infra.change_INT = float.Parse(training_infra_table[i]["change_INT"].ToString());
            new_training_Infra.change_WIS = float.Parse(training_infra_table[i]["change_WIS"].ToString());
            new_training_Infra.change_WIL = float.Parse(training_infra_table[i]["change_WIL"].ToString());

            training_infra_data.Add(new_training_Infra.code, new_training_Infra);
        }
    }
}
