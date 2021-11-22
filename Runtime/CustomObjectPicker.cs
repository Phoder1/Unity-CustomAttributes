using UnityEngine;

namespace CustomAttributes
{
    public enum ResultObjectType
    {
        Scene,
        Asset,
        SceneOrAsset
    };
    [HelpURL("https://forum.unity.com/threads/favourite-way-to-serialize-interfaces.513874/")]
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class CustomObjectPickerAttribute : PropertyAttribute
    {
        public System.Type[] typeRestrictions;
        public ResultObjectType resultObjectType;

        private CustomObjectPickerAttribute() { }

        public CustomObjectPickerAttribute(params System.Type[] typeRestrictions)
        {
            this.typeRestrictions = typeRestrictions;
            this.resultObjectType = ResultObjectType.SceneOrAsset;
        }
        public CustomObjectPickerAttribute(ResultObjectType resultObjectType, params System.Type[] typeRestrictions)
        {
            this.typeRestrictions = typeRestrictions;
            this.resultObjectType = resultObjectType;
        }
    }
}