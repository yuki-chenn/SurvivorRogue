using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Survivor.Utils
{
    class GameObjectUtil
    {
        public static void SetAllComponentEnable(Transform transform, bool enable)
        {
            // 获取当前 Transform 对象上的所有组件
            Component[] components = transform.GetComponents<Component>();

            // 禁用每个组件
            foreach (Component component in components)
            {
                // 排除 Transform 组件本身
                if (!(component is Transform))
                {
                    // 如果组件类型有 enabled 属性，则将其设置为 false
                    var type = component.GetType();
                    var enabledProperty = type.GetProperty("enabled");
                    if (enabledProperty != null)
                    {
                        enabledProperty.SetValue(component, enable);
                    }
                }
            }
        }

        public static void SetAllChildGameObjectEnable(Transform transform, bool enable)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(enable);
            }
        }

    }
}
