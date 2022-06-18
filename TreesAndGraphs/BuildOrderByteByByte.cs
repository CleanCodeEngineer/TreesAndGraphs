using System;
using System.Collections.Generic;

namespace TreesAndGraphs
{
    // Given a list of packages that need to be built
    // and the dependencies for each package, determine
    // a valid order in which to build the packages.
    // eg.
    // {{},{0},{0},{1,2},{3}}
    // 0, 1, 2, 3, 4
    //      - 1 <-
    //     /       \
    // 0 <-         <- 3 <- 4
    //     \       /
    //      - 2 <-
    public class BuildOrderByteByByte
    {
        public List<int> BuildOrder(int[,] processes)
        {
            HashSet<int> tempMarks = new HashSet<int>();
            HashSet<int> permMarks = new HashSet<int>();
            List<int> results = new List<int>();

            for (int i = 0; i < processes.GetLength(0); i++)
            {
                if(!permMarks.Contains(i))
                {
                    visit(i, processes, tempMarks, permMarks, results);
                }
            }

            return results;
        }

        public void visit(int process, int[,] processes, HashSet<int> tempMarks, HashSet<int> permMarks, List<int> results)
        {
            if (tempMarks.Contains(process)) throw new Exception();

            if (!permMarks.Contains(process))
            {
                tempMarks.Add(process);

                for (int i = 0; i < processes.GetLength(1); i++)
                {
                    if (processes[process, i] != -1)
                        visit(processes[process, i], processes, tempMarks, permMarks, results);
                }

                permMarks.Add(process); 
                tempMarks.Remove(process);

                results.Add(process);
            }
        }
    }
}
