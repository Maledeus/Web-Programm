using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WifiSD.Core.Application.Commands
{
    public abstract class BaseCommandHandler
    {
        protected void MapEntityProperties<TSource, TTarget>(TSource source, TTarget target, IList<string> excludeProperties = default)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();

            if (sourceType.BaseType.FullName != targetType.BaseType.FullName)
            {
                throw new InvalidOperationException("Base types are not matching.");
            }

            List<PropertyInfo> targetPropertyInfos = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            targetPropertyInfos.ForEach(p =>
            {
                /* Ist das Ziel-Property schreibbar und nicht in der Liste der Ausnahmen */
                if (p.CanWrite && !(excludeProperties ?? new List<string>()).Contains(p.Name))
                {
                    var sourceProperty = sourceType.GetProperty(p.Name, BindingFlags.Instance | BindingFlags.Public);
                    if (sourceProperty != null)
                    {
                        /* Wert aus Quelle lesen */
                        var sourcePropertyValue = sourceProperty.GetValue(source, null);

                        /* Wert in Ziel schreiben */
                        p.SetValue(target, sourcePropertyValue);

                    }
                }
            });


        }
    }
}
