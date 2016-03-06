namespace _01.CollectResources
{
    using System;
    using System.Linq;

    public static class CollectResources
    {
        private const char ItemSeparator = '_';
        private static string[] validItems = { "stone", "gold", "wood", "food" };
        private static int bestPathSum = 0;

        public static void Main()
        {
            var items = Console.ReadLine().Split(' ').ToArray();
            var numberOfItems = items.Length;
            var numberOdPaths = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOdPaths; i++)
            {
                var path = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                var step = path[0];
                var currentPathSum = 0;
                var visited = new bool[numberOfItems];
                while (!visited[step])
                {
                    var indexOfSeparator = items[step].IndexOf(ItemSeparator);
                    var currentItem = string.Empty;
                    var currentQuantity = 0;

                    if (indexOfSeparator < 0)
                    {
                        currentItem = items[step];
                        currentQuantity = 1;
                    }
                    else
                    {
                        currentItem = items[step].Substring(0, indexOfSeparator);
                        currentQuantity = int.Parse(
                            items[step].Substring(
                                indexOfSeparator + 1, 
                                items[step].Length - indexOfSeparator - 1));
                    }

                    if (validItems.Contains(currentItem))
                    {
                        currentPathSum += currentQuantity;
                        visited[step] = true;
                    }

                    step = (step + path[1]) % numberOfItems;
                }

                if (currentPathSum > bestPathSum)
                {
                    bestPathSum = currentPathSum;
                }
            }

            Console.WriteLine(bestPathSum);
        }
    }
}
