using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace Config
{
    public class SectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object context, XmlNode section)
        {
            List<int> values = new List<int>();

            for (int i = 0; i < section.ChildNodes.Count; i++)
            {
                values.Add(Convert.ToInt32(section.ChildNodes[i].InnerText));
            }

            return new IntArray(values);
        }
    }
    public struct Array
    {
        public int[] array;
        public int start;
        public int end;

        public Array(int[] array, int start, int end)
        {
            this.array = array;
            this.start = start;
            this.end = end;
        }
    }

    public class IntArray
    {
        private List<int> values;

        public IntArray(List<int> values)
        {
            this.values = values;
        }

        public Array ThreadedArray()
        {
            return new Array(values.ToArray(), 0, values.Count - 1);
        }

        public List<int> Values
        {
            get { return this.values; }
        }

    }
}
