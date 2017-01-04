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
            int r = Convert.ToInt32(section["rows"].InnerText);
            int c = Convert.ToInt32(section["cols"].InnerText);
            return new Config(r, c);
        }
    }

    public class Config
    {
        private int rows;
        private int cols;

        public Config(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
        }

        public int Rows
        {
            get { return rows; }
        }

        public int Cols
        {
            get { return cols; }
        }
    }
}
