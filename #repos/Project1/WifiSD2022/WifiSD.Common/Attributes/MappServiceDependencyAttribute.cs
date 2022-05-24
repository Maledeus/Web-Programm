using System;
using System.Collections.Generic;
using System.Text;

namespace WifiSD.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MappServiceDependencyAttribute : Attribute
    {

        protected string name;

        public string Name => this.name;

        public MappServiceDependencyAttribute(string name) 
        {
            this.name = name; 
        }

    }
}
