using System;
using System.Text;

namespace ScriptingLibrary
{
    class ResolvingCodeGenerator
    {
        public ResolvingCodeGenerator(Container container)
        {
            UsingLines = GenerateUsingLines(container);
            ResolveLines = GenerateResolveLines(container);
        }

        private string GenerateUsingLines(Container container)
        {
            var sb = new StringBuilder();

            foreach (var entry in container.Names)
            {
                var type = entry.Key;

                var resolvingCode = $"using {type.Namespace};" + Environment.NewLine;


                sb.Append(resolvingCode);
            }

            return sb.ToString();
        }

        private string GenerateResolveLines(Container container)
        {
            var sb = new StringBuilder();

            foreach (var entry in container.Names)
            {
                var type = entry.Key;
                var name = entry.Value;

                var resolvingCode = $"var {name} = Container.Resolve<{type.Name}>();" + Environment.NewLine;


                sb.Append(resolvingCode);
            }

            return sb.ToString();
        }

        public string UsingLines { get; }
        public string ResolveLines { get; }
    }
}
