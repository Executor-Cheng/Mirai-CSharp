using System;
using System.Collections.Generic;
using Mirai.CSharp.Framework.Invoking;

namespace Mirai.CSharp.HttpApi.Session
{
    public struct PluginResistration : IDisposable
    {
        internal LinkedList<DynamicHandlerRegistration>? _registrations;

        public PluginResistration(LinkedList<DynamicHandlerRegistration> registrations)
        {
            _registrations = registrations;
        }

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
