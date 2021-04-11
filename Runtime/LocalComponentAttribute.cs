using UnityEngine;
namespace CustomAttributes
{
    public class LocalComponentAttribute : PropertyAttribute
    {
        public readonly bool getComponentFromChildrens;
        public readonly bool hideProperty;
        public readonly string parentObject;

        public LocalComponentAttribute(bool hideProperty = false, bool getComponentFromChildrens = false, string parentObject = "")
        {
            this.getComponentFromChildrens = getComponentFromChildrens;
            this.hideProperty = hideProperty;
            this.parentObject = parentObject;
        }
    }
}