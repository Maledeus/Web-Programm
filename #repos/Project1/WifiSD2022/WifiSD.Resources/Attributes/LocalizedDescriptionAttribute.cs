using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Resources;
using System.Text;

namespace WifiSD.Resources.Attributes
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly string _resourceKey;
        private static ResourceManager _resourceManager { get; set; }

        public LocalizedDescriptionAttribute(string resourceKey)
        {
            this._resourceKey = resourceKey;
        }

        public override string Description
        {
            get =>this._resourceKey != null ? _resourceManager.GetString(this._resourceKey) : null;
        }

        public static void Setup(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

    }
}
