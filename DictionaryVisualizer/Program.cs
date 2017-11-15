using System;
using DictionaryVisualizer.Utilities;

namespace DictionaryVisualizer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TreeNode<string> root = new TreeNode<string>("Yoni");
            {
                root.AddChild("Short");
                root.AddChild("Dumb");
                TreeNode<string> node2 = root.AddChild("Stupid");
                {
                    node2.AddChild("node20");
                    TreeNode<string> node21 = node2.AddChild("node21");
                    {
                        node21.AddChild("node210");
                        node21.AddChild("node211");
                    }
                }
                TreeNode<string> node3 = root.AddChild("node3");
                {
                    node3.AddChild("node30");
                }
            }

            GraphvizHelper.DrawTree(root, "output_graph.png");
            Console.WriteLine("Finished graph output.");
        }
    }
}
