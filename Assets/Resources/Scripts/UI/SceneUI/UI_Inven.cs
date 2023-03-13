using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanal,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanal = Get<GameObject>((int)GameObjects.GridPanal);

        foreach (Transform child in gridPanal.transform)
            Managers.Resources.Destroy(child.gameObject);

        for (int i = 0; i < 8; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Inven>(gridPanal.transform).gameObject;

            UI_Inven_Item inven_item = Util.GetOrAddComponent<UI_Inven_Item>(item);
            inven_item.SetInfo($"집행검{i}번째");
        }
    }
}
