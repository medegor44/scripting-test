using System;
using System.Collections.Generic;

namespace ScriptingLibrary
{
    public class Container // Maybe find more elegant ways to pass globals to script?
    {
        public void Register<TObject>(TObject obj, string name)
        {
            Instances[typeof(TObject)] = obj;
            Names[typeof(TObject)] = name;
        }

        public TObject Resolve<TObject>()
        {
            return (TObject)Resolve(typeof(TObject));
        }

        public object Resolve(Type type)
        {
            if (Instances.ContainsKey(type))
                return Instances[type];

            throw new ArgumentException("unregistered dependency");
        }

        public string GetDependencyName(Type type)
        {
            if (Names.ContainsKey(type))
                return Names[type];

            throw new ArgumentException("unregistered dependency");
        }

        public void RegisterFunction(object func, string alias)
        {
            Functions[alias] = func;
        }

        public object ResolveFunction(string alias)
        {
            return Functions[alias];
        }

        public Dictionary<string, object> Functions { get; } = new();
        public Dictionary<Type, object> Instances { get; } = new();
        public Dictionary<Type, string> Names { get; } = new();
    }
}
