using UnityEngine;

namespace Libs.Bootstrap
{
    [RequireComponent(typeof(RunnableContext))]
    public abstract class Installer : MonoBehaviour
    {
        public abstract void Install(RunnableContext context);
    }
}