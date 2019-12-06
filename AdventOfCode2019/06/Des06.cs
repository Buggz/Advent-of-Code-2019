using System.Collections.Generic;
using System.Linq;
using System.Xml;
using AdventOfCode2019._04;

namespace AdventOfCode2019._06
{
    public static class Des06
    {
        public static int First(List<string> list)
        {
            var spaceObjects = Parse(list);
            SetDepths(spaceObjects);

            return spaceObjects.Values.Sum(x => x.Depth);
        }

        public static int Second(List<string> list)
        {
            var spaceObjects = Parse(list);
            SetDepths(spaceObjects);
            return CalculateTransfers(spaceObjects, "YOU", "SAN");
        }

        private static int CalculateTransfers(Dictionary<string, SpaceObject> dict, string from, string to)
        {
            var you = dict[from];
            var san = dict[to];
            var commonDepth = FindCommonParent(you, san).Depth + 1;
            return (you.Depth - commonDepth) + (san.Depth - commonDepth);
        }

        private static SpaceObject FindCommonParent(SpaceObject you, SpaceObject san)
        {
            var list1 = GetAllParents(you);
            var list2 = GetAllParents(san);

            return list1.Intersect(list2).OrderBy(x => x.Depth).Last();
        }

        private static List<SpaceObject> GetAllParents(SpaceObject you)
        {
            var result = new List<SpaceObject>();
            while (you.Parent != null)
            {
                result.Add(you.Parent);
                you = you.Parent;
            }

            return result;
        }

        private static void SetDepths(Dictionary<string, SpaceObject> spaceObjects)
        {
            var root = spaceObjects.Values.Single(x => x.Parent == null);
            root.Depth = 0;
            var children = root.Children;
            RecursiveDepths(children, 1);
        }

        private static void RecursiveDepths(List<SpaceObject> spaceObjects, int depth)
        {
            var children = new List<SpaceObject>();
            foreach (var spaceObject in spaceObjects)
            {
                spaceObject.Depth = depth;
                children.AddRange(spaceObject.Children);
            }
            
            if (children.Any())
                RecursiveDepths(children, depth + 1);
        }

        public static Dictionary<string, SpaceObject> Parse(List<string> list)
        {
            var spaceObjects = new Dictionary<string, SpaceObject>();

            foreach (var line in list)
            {
                var split = line.Split(')');
                var parentName = split[0];
                var childName = split[1];

                SpaceObject parent, child;

                if (spaceObjects.ContainsKey(parentName))
                    parent = spaceObjects[parentName];
                else
                {
                    parent = new SpaceObject(parentName);
                    spaceObjects.Add(parentName, parent);    
                }
                
                if (spaceObjects.ContainsKey(childName))
                    child = spaceObjects[childName];
                else
                {
                    child = new SpaceObject(childName);
                    spaceObjects.Add(childName, child);
                }
                
                child.Parent = parent;
                parent.Children.Add(child);
            }

            return spaceObjects;
        }

        public class SpaceObject
        {
            public SpaceObject(string name)
            {
                Name = name;
            }
            public string Name { get; set; }
            public int Depth { get; set; }

            public SpaceObject Parent { get; set; }

            public List<SpaceObject> Children = new List<SpaceObject>();
            private SpaceObject _parent;
        }
    }
}