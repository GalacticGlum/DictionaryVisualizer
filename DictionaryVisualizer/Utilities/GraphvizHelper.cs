using System.Diagnostics;
using System.IO;

namespace DictionaryVisualizer.Utilities
{
    public static class GraphvizHelper
    {
        private const string GraphvizBinaryLocation = "C:\\Program Files (x86)\\Graphviz2.38\\bin\\dot.exe";

        public static void DrawTree<T>(TreeNode<T> treeRoot, string filepath)
        {
            string graphvizCode = "digraph tree\n{\n";

            treeRoot.Traverse(node =>
            {
                // ReSharper disable once AccessToModifiedClosure
                graphvizCode += $"{{node_{node.Parent.Id}[label=\"{node.Parent.Value.ToString()}\"]}} -> {{node_{node.Id}[label=\"{node.Value.ToString()}\"]}};\n";
            }, false);
            graphvizCode += "}";

            string dotFilepath = Path.GetFileNameWithoutExtension(filepath) + ".dot";
            File.WriteAllText(dotFilepath, graphvizCode);
            Process.Start(GraphvizBinaryLocation, $"-Tpng -o{filepath} {dotFilepath}");
        }
    }
}
