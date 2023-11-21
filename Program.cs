using System;
using System.Collections.Generic;
using System.Linq;

namespace Islands
{
    class Islands {

        public List<char> Map = new List<char>();

        public int numColumns;

        public void printMap()
        {
            int index = 0;
            foreach (var item in Map)
            {
                Console.Write(item);
                index++;
                if (index==numColumns)
                {
                    index = 0;
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
        char getMapValue(int x, int y)
        {
            int offset = (y * numColumns) + x;
            return Map[offset];
        }

        void setMapValue(int x, int y, char islandId)
        {
            int offset = (y * numColumns) + x;
            var tmpMap = Map.ToArray();
            tmpMap[offset] = islandId;
            Map = tmpMap.ToList();
        }

        Dictionary<int, int> getXYCoordsByAbsPos(int absPos)
        {
            var pos = new Dictionary<int, int>();
            pos.Add((absPos % numColumns), (absPos / numColumns));
            return pos;
        }

        public void addToMapFromString(string rawLine)
        {
            Map = (String.Join("", Map) + rawLine).ToList();
        }

        void travelIsland(int startX, int startY, char islandId)
        {
            if ((startX < 0) || (startY < 0))
                return;
            if ((startX > numColumns - 1) || (startY > (Map.ToString().Length / numColumns)))
                return;
            if (getMapValue(startX, startY) != '1')
                return;
           
            setMapValue(startX, startY, islandId);

            travelIsland(startX - 1, startY - 1, islandId);
            travelIsland(startX - 0, startY - 1, islandId);
            travelIsland(startX + 1, startY - 1, islandId);

            travelIsland(startX - 1, startY - 0, islandId);
            travelIsland(startX + 1, startY - 0, islandId);

            travelIsland(startX - 1, startY + 1, islandId);
            travelIsland(startX - 0, startY + 1, islandId);
            travelIsland(startX + 1, startY + 1, islandId);

        }
        public void recurseMap()
        {

            string linearMap = String.Join("", Map);
            int currentIslandId = 65;

            do
            {
                var absPos = linearMap.IndexOf("1");

                if (linearMap.IndexOf("1") >= 0)
                {
                    var xyPos = getXYCoordsByAbsPos(absPos);

                    var x = xyPos.First().Key;
                    var y = xyPos.First().Value;

                    travelIsland(x,y, (char)currentIslandId);
                }

                linearMap = String.Join("", Map);

                currentIslandId++;

            } while (linearMap.IndexOf("1") >= 0);

        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            var island = new Islands();
            island.numColumns= 14;

            island.addToMapFromString("11100010000001");
            island.addToMapFromString("00100100000000");
            island.addToMapFromString("00011000100000");
            island.addToMapFromString("10001100010000");

            island.recurseMap();

            island.printMap();

            Console.WriteLine(island.Map.Distinct().ToList().Count-1);

            Console.ReadKey();

        }
    }
}
