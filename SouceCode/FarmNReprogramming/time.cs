using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Threading;
namespace ConsoleApplication1
{
    /// <summary>
    /// This class is for mesuring times on different functions
    /// </summary>
    public sealed class  time
    {
        // store information on all function
        List<data> list = new List<data>();
        // we dont would not allow other functions to create a new instance of this class so the constructor is private
        private time() { }

        /// <summary>
        /// Accessing the instance from outside
        /// </summary>
        /// <returns>the only instance of time-class</returns>
        public static time GetInstance()
        {
            return NestedSingleton.singleton;
        }
        
        // Generate a thread safe instance of this class
        class NestedSingleton
        {
            internal static readonly time singleton = new time();

            static NestedSingleton() { }
        }

        //a data class that store information about each funktion
        private class data
        {
            private data(){}
            // name of function
            public string name;
            // number of time that function has been called
            public int timeCalled;
            // time spend on this function
            public TimeSpan ts;
            // adding more time that this function has used
            public void addTS(TimeSpan input) { ts = ts + input; }
            public data(string input)
            {
            
                name = input;
                timeCalled = 1;
                ts= new TimeSpan();

            }
            



        }
        /// <summary>
        /// Adding time for a function
        /// </summary>
        /// <param name="name">The functions name</param>
        /// <param name="ts">Time spend</param>
        public void add(String name, TimeSpan ts)
        {
            bool found=false;
            int where = -1;
            // se if we already have information about that function
            for(int i=0;i<list.Count;i++)
            {
                if (list[i].name == name)
                {
                    found = true;
                    where = i;
                }

            }
            // if we already know about the function we start measuring the time
            if (found == true)
            {
               list[where].ts = ts;
               list[where].timeCalled++;
            }
            // if we dont know it we create the needed information and start measuring the time
            else
            {
                data tmp = new data(name);
                list.Add(tmp);
            }
        }
        /// <summary>
        /// Prints out times used on a function, number of time that function has been used and average time 
        /// </summary>
        public void printAll()
        {
            foreach (data tmp in list)
            {
                Console.WriteLine(tmp.name + " used " + tmp.ts + " ms");
                Console.WriteLine(tmp.name + " was called " + tmp.timeCalled + " time");
                TimeSpan tid = tmp.ts;
                //adding hours in ms
                int totalTime = tmp.ts.Hours * 60 * 60 * 1000;
                //adding min in ms
                totalTime = totalTime + tmp.ts.Minutes * 60 * 1000;
                //adding s in ms
                totalTime = totalTime + tmp.ts.Seconds * 1000;
                //adding ms
                totalTime = totalTime + tmp.ts.Milliseconds;
                Console.WriteLine("on avarge " + tmp.name + " did use: " + totalTime / tmp.timeCalled + "ms");

            }
        }
    }
}
