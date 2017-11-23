using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Threading;
namespace ConsoleApplication3
{
    /// <summary>
    /// This class is for mesuring times on different functions
    /// </summary>
    public sealed class timeV2
    {
        // store information from all function
        List<data> list = new List<data>();
        // since we are usign singleton design pattens the constructor is private
        private timeV2()
        {
            for (int i = 0; i < 20; i++)
            {
                data tmp = new data("unused");
                list.Add(tmp);
            }
        }

        /// <summary>
        /// Accessing the instance from outside
        /// </summary>
        /// <returns>the only instance of the time-class</returns>
        public static timeV2 GetInstance()
        {
            return NestedSingleton.singleton;
        }

        // Generate a thread safe instance of this class
        class NestedSingleton
        {
            internal static readonly timeV2 singleton = new timeV2();

            static NestedSingleton() { }
        }

        //a data class that store information about each funktion
        private class data
        {
            private data() { }
            // name of function
            public string name;
            // number of time that function has been called
            public int timeCalled;
            // time spend on this function
            public TimeSpan ts;

            public data(string input)
            {

                name = input;
                timeCalled = 0;
                ts = new TimeSpan();

            }




        }
        /// <summary>
        /// Adding time for a function
        /// </summary>
        /// <param name="name">The functions name</param>
        /// <param name="ts">Time spend</param>
        /// <param name="place">The exact place in internal data structur</param>
        public void add(String name, TimeSpan ts, int place)
        {


            list[place].ts = ts;
            list[place].name = name;
            list[place].timeCalled++;

        }
        /// <summary>
        /// Prints out total times used on a function, number of time that function has been used and average time 
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
                if (tmp.timeCalled != 0)
                    Console.WriteLine("on avarge " + tmp.name + " did use: " + totalTime / tmp.timeCalled + "ms");

            }
        }
    }
}