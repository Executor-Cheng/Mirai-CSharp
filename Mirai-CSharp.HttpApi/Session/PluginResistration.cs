using System.Collections.Generic;
using Mirai.CSharp.Framework.Invoking;

namespace Mirai.CSharp.HttpApi.Session
{
    public struct PluginResistration
    {
        internal LinkedList<DynamicHandlerRegistration>? _registrations;

        public void Dispose()
        {
            if (_registrations is LinkedList<DynamicHandlerRegistration> registrations)
            {
                foreach (var registration in registrations)
                {
                    registration.Dispose();
                }
            }
        }
    }
}
