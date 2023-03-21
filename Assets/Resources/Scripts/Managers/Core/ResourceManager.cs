using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        // 프리팹 폴더에서 경로를 통해 오브젝트를 찾아, 로드를 통해 original이라는 원본을 메모리에 로드함.
        GameObject original = Load<GameObject>($"Prefabs/{path}");    // 여기에서는 메모리에만 객체가 있고 씬에서는 표현이 안됨.
        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        // 풀링된 애인지 확인
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;


        GameObject go = Object.Instantiate(original, parent);     // 여기에서 original이라는 원본을 대상으로 복사본을 만들어, 씬에 배치해준다.

        // original 복사본의 이름 바꾸기
        // int index = go.name.IndexOf("(Clone)");
        // if (index > 0)
        //     go.name = go.name.Substring(0, index);
        go.name = original.name;  // 이렇게 이름을 바꿔도 된다.

        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        // 풀링이 필요한 애라면 풀링 매니저한테 위탈
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }
        
        Object.Destroy(go);
    }
}
